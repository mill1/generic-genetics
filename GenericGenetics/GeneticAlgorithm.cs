using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericGenetics
{
    public class GeneticAlgorithm<T>
    {
        public List<DNA<T>> NewPopulation { get; private set; }
        public double MutationRate { get; private set; }

        private List<DNA<T>> population;
        private readonly Random random;

        public GeneticAlgorithm(int populationSize, int dnaSize, Random random, Func<Random, T> getRandomGene,
                                Func<DNA<T>, double> determineFitness, double mutationRate)
        {
            MutationRate = mutationRate;
            population = new List<DNA<T>>(populationSize);
            NewPopulation = new List<DNA<T>>(populationSize);
            this.random = random;

            population = Enumerable.Range(0, populationSize).Select(
                e => new DNA<T>(dnaSize, random, getRandomGene, determineFitness, InitializeGenes: true)).ToList();
        }

        public void SpawnNewGeneration()
        {
            population.ForEach(e => e.CalculateFitness(e));

            NewPopulation.Clear();

            double maximumMaleFitness = GetMaximumFitness(isMale: true);
            double maximumFemaleFitness = GetMaximumFitness(isMale: false);

            population.ForEach(e => NewPopulation.Add(GetChild(maximumMaleFitness, maximumFemaleFitness)));

            // Save memory; switch between lists
            List<DNA<T>> tmpList = population;
            population = NewPopulation;
            NewPopulation = tmpList;
        }

        private double GetMaximumFitness(bool isMale)
        {
            // https://en.wikipedia.org/wiki/Percentile_rank
            double partnerFitnessPercentile = 0.70f;

            // the number of members of the population beloning to the percentile. 
            int elite = (int)(population.Count * (1 - partnerFitnessPercentile));


            return population.OrderBy(e => e.Fitness)
                           .Where(e => e.IsMale == isMale)
                           .Select(e => e.Fitness).Take(elite).First();
        }

        private DNA<T> GetChild(double maximumMaleFitness, double maximumFemaleFitness)
        {
            DNA<T> parent1 = ChooseParent(isMale: true, maximumFemaleFitness);
            DNA<T> parent2 = ChooseParent(isMale: false, maximumMaleFitness);

            DNA<T> child = parent1.Crossover(parent2);

            child.Mutate(MutationRate);
            return child;
        }

        private DNA<T> ChooseParent(bool isMale, double maximumParentFitness)
        {
            while (true)
            {
                int i = (int)(random.NextDouble() * population.Count);

                if (population[i].IsMale != isMale)
                    if (population[i].Fitness <= maximumParentFitness)
                        return population[i];
            }
        }
    }
}
