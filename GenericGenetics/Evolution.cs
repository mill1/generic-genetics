using System;

namespace GenericGenetics
{
    public abstract class Evolution<T>
    {
        Random random;
        public abstract void GetInput();

        public abstract void DisplayResult(T[] bestGenes, double bestFitness, int generation);

        public abstract double TargetFitness { get; }
        public abstract int PopulationSize { get; }
        public abstract int DnaSize { get; set; }
        public abstract double MutationRate { get; }

        // Delegates
        public abstract T GetRandomGene(Random random);
        public abstract double DetermineFitness(DNA<T> dna);

        public void Run()
        {
            random = new Random();

            GetInput();

            GeneticAlgorithm<T> ga = new GeneticAlgorithm<T>(PopulationSize, DnaSize, random, GetRandomGene, DetermineFitness, MutationRate);

            while (ga.BestFitness < TargetFitness)
            {
                ga.SpawnNewGeneration();
                DisplayResult(ga.BestGenes, ga.BestFitness, ga.Generation);
            }
        }
    }
}
