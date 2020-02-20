using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericGenetics
{
    public class GeneticAlgorithm<T>
    {
        public List<DNA<T>> Population { get; private set; }
        public int Generation { get; private set; }
        public double BestFitness { get; private set; }
        public T[] BestGenes { get; private set; }
        public double MutationRate { get; private set; }

        private List<DNA<T>> newPopulation;
        private readonly Random random;

        public GeneticAlgorithm(int populationSize, int dnaSize, Random random, Func<Random, T> getRandomGene,
                                Func<DNA<T>, double> determineFitness, double mutationRate)
        {
            Generation = 1;
            MutationRate = mutationRate;
            Population = new List<DNA<T>>(populationSize);
            newPopulation = new List<DNA<T>>(populationSize);
            this.random = random;

            BestGenes = new T[dnaSize];

            Population = Enumerable.Range(0, populationSize).Select(
                e => new DNA<T>(dnaSize, random, getRandomGene, determineFitness, InitializeGenes: true)).ToList();
        }

        public void SpawnNewGeneration()
        {
            // https://en.wikipedia.org/wiki/Percentile_rank
            double partnerFitnessPercentile = 0.6f;

            Population.ForEach(e => e.CalculateFitness(e));

            StoreBestGenes();

            newPopulation.Clear();

            // the number of members of the population beloning to the percentile. 
            int elite = (int)(Population.Count * (1 - partnerFitnessPercentile));

            double minimalParentFitness = Population.OrderByDescending(e => e.Fitness)
                                          .Select(e => e.Fitness).Take(elite).Last();

            Population.ForEach(e => newPopulation.Add(GetChild(minimalParentFitness)));

            // Save memory; switch between lists
            List<DNA<T>> tmpList = Population;
            Population = newPopulation;

            newPopulation = tmpList;

            Generation++;
        }

        private void StoreBestGenes()
        {
            DNA<T> best = Population.OrderByDescending(e => e.Fitness).First();

            BestFitness = best.Fitness;
            best.Genes.CopyTo(BestGenes, 0);
        }

        private DNA<T> GetChild(double minimalParentFitness)
        {
            DNA<T> parent1 = ChooseParent(isMale: true, minimalParentFitness);
            DNA<T> parent2 = ChooseParent(isMale: false, minimalParentFitness);

            DNA<T> child = parent1.Crossover(parent2);

            child.Mutate(MutationRate);
            return child;
        }

        private DNA<T> ChooseParent(bool isMale, double minimalParentFitness)
        {
            while (true)
            {
                int i = (int)(random.NextDouble() * Population.Count);

                if (Population[i].IsMale != isMale)
                    if (Population[i].Fitness >= minimalParentFitness)
                        return Population[i];
            }
        }
    }
}
