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

            double minimalMaleFitness = GetMinimalFitness(isMale: true);
            double minimalFemaleFitness = GetMinimalFitness(isMale: false);

            population.ForEach(e => NewPopulation.Add(GetChild(minimalMaleFitness, minimalFemaleFitness)));

            // Save memory; switch between lists
            List<DNA<T>> tmpList = population;
            population = NewPopulation;
            NewPopulation = tmpList;
        }

        private double GetMinimalFitness(bool isMale)
        {
            // https://en.wikipedia.org/wiki/Percentile_rank
            double partnerFitnessPercentile = 0.75f;

            // the number of members of the population beloning to the percentile. 
            int elite = (int)(population.Count * (1 - partnerFitnessPercentile));

            return population.OrderByDescending(e => e.Fitness)
                            .Where(e => e.IsMale == isMale)
                            .Select(e => e.Fitness).Take(elite).Last();
        }

        private DNA<T> GetChild(double minimalMaleFitness, double minimalFemaleFitness)
        {
            DNA<T> parent1 = ChooseParent(isMale: true, minimalFemaleFitness);
            DNA<T> parent2 = ChooseParent(isMale: false, minimalMaleFitness);

            DNA<T> child = parent1.Crossover(parent2);

            child.Mutate(MutationRate);
            return child;
        }

        private DNA<T> ChooseParent(bool isMale, double minimalParentFitness)
        {
            while (true)
            {
                int i = (int)(random.NextDouble() * population.Count);

                if (population[i].IsMale != isMale)
                    if (population[i].Fitness >= minimalParentFitness)
                        return population[i];
            }
        }
    }
}
