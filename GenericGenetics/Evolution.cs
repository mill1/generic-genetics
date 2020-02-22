using System;
using System.Linq;

namespace GenericGenetics
{
    public abstract class Evolution<T>
    {
        Random random;

        private double TargetFitness { get; }
        public int PopulationSize { get; }
        private int DnaSize { get; }
        public int DnaMinValue { get; }
        public int DnaMaxValue { get; }
        private double MutationRate { get; }

        // Delegates
        public abstract T GetRandomGene(Random random);
        public abstract double DetermineFitness(DNA<T> genotype);

        public Evolution(double targetFitness,  int populationSize, int dnaSize, int dnaMinValue, int dnaMaxValue, double mutationRate)
        {
            TargetFitness = targetFitness;
            PopulationSize = populationSize;
            DnaSize = dnaSize;
            DnaMinValue = dnaMinValue;
            DnaMaxValue = dnaMaxValue;
            MutationRate = mutationRate;
        }

        // TODO targetFitness en MutationRate als arguments
        public void Run(Action<DNA<T>, int> displayPhenotype)
        {
            DNA<T> genotype;
            int generation = 1;

            random = new Random();

            GeneticAlgorithm<T> ga = new GeneticAlgorithm<T>(PopulationSize, DnaSize, random, GetRandomGene, DetermineFitness, MutationRate);

            double bestFitness = TargetFitness - 1;

            while (bestFitness < TargetFitness)
            {
                ga.SpawnNewGeneration();
                genotype = ga.NewPopulation.OrderByDescending(e => e.Fitness).First();
                bestFitness = genotype.Fitness;

                displayPhenotype(genotype, generation++);
            }
        }
    }
}
