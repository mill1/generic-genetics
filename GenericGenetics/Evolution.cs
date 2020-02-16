using System;
using System.Collections.Generic;
using System.Text;

namespace GenericGenetics
{
    public abstract class Evolution
    {
        private Random random;
        public abstract void GetInput();

        public abstract void DisplayResult(char[] bestGenes, float bestFitness, int generation);

        public abstract double TargetFitness { get; }
        public abstract int PopulationSize { get; }
        public abstract int DnaSize { get; set; }
        public abstract float MutationRate { get; }

        // Delegates
        public abstract char GetRandomGene();
        public abstract float DetermineFitness(DNA<char> dna);

        public void Run() 
        {
            GetInput();

            GeneticAlgorithm<char> ga = new GeneticAlgorithm<char>(PopulationSize, DnaSize, new Random(), GetRandomGene, DetermineFitness, MutationRate);

            while (ga.BestFitness < TargetFitness)
            {
                ga.SpawnNewGeneration();
                DisplayResult(ga.BestGenes, ga.BestFitness, ga.Generation);
            }
        }
    }
}
