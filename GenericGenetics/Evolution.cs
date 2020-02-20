using System;
using System.Linq;

namespace GenericGenetics
{
    public abstract class Evolution<T>
    {
        Random random;
        public abstract void GetInput();

        public abstract void DisplayResult(DNA<T> dna, int generation);

        public abstract double TargetFitness { get; }
        public abstract int PopulationSize { get; }
        public abstract int DnaSize { get; set; }
        public abstract double MutationRate { get; }

        // Delegates
        public abstract T GetRandomGene(Random random);
        public abstract double DetermineFitness(DNA<T> dna);

        public void Run()
        {
            int generation = 1;
            DNA<T> bestDNA;

            random = new Random();

            GetInput();

            GeneticAlgorithm<T> ga = new GeneticAlgorithm<T>(PopulationSize, DnaSize, random, GetRandomGene, DetermineFitness, MutationRate);

            double bestFitness = TargetFitness - 1;

            while (bestFitness < TargetFitness)
            {
                ga.SpawnNewGeneration();
                bestDNA = ga.NewPopulation.OrderByDescending(e => e.Fitness).First();
                bestFitness = bestDNA.Fitness;

                DisplayResult(bestDNA, generation++);
            }

            //Worst DNA in population:
            bestDNA = ga.NewPopulation.OrderByDescending(e => e.Fitness).Last();
            DisplayResult(bestDNA, --generation);
        }
    }
}
