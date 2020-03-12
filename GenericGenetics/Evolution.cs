using System;
using System.Linq;

namespace GenericGenetics
{
    public abstract class Evolution<T>
    {
        Random random;

        private double targetFitness;
        private int populationSize;
        internal int DnaSize { get; private set; }
        internal int DnaMinValue { get; set; }
        internal int DnaMaxValue { get; set; }
        private double mutationRate;

        // Delegates
        internal abstract T GetRandomGene(Random random);

        internal abstract double DetermineFitness(DNA<T> genotype);

        public void SetParameters(Parameters parameters)
        {
            targetFitness = parameters.TargetFitness;
            populationSize = parameters.PopulationSize;
            DnaMinValue = parameters.DnaMinValue;
            DnaMaxValue = parameters.DnaMaxValue;
            mutationRate = parameters.MutationRate;
        }

        public void Run(int dnaSize, Action<DNA<T>, int> displayPhenotype) 
        {
            DnaSize = dnaSize;
            DNA<T> genotype;
            int generation = 1;
            random = new Random();

            GeneticAlgorithm<T> ga = new GeneticAlgorithm<T>(populationSize, dnaSize, random, GetRandomGene, DetermineFitness, mutationRate);

            double bestFitness = targetFitness + 1;

            while (bestFitness > targetFitness)
            {
                ga.SpawnNewGeneration();
                genotype = ga.newPopulation.OrderBy(e => e.Fitness).First();
                bestFitness = genotype.Fitness;
                displayPhenotype(genotype, generation++);
            }
        }
    }
}
