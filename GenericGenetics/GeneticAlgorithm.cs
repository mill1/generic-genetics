using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericGenetics
{
    public class GeneticAlgorithm<T>
    {
        public double MutationRate { get; private set; }

        public List<DNA<T>> Population { get; private set; }
        private List<DNA<T>> newPopulation;
        private readonly Random random;

        public GeneticAlgorithm(int populationSize, int dnaSize, Random random, Func<Random, T> getRandomGene,
                                Func<DNA<T>, double> determineFitness, double mutationRate)
        {
            MutationRate = mutationRate;
            Population = new List<DNA<T>>(populationSize);
            newPopulation = new List<DNA<T>>(populationSize);
            this.random = random;

            Population = Enumerable.Range(0, populationSize).Select(
                e => new DNA<T>(dnaSize, random, getRandomGene, determineFitness, InitializeGenes: true)).ToList();
        }

        public void DeterminePopulationFitness()
        {
            Population.ForEach(e => e.DetermineFitness());
        }

        public void SpawnNewGeneration()
        {
            double maximumMaleFitness = GetMaximumFitness(isMale: true);
            double maximumFemaleFitness = GetMaximumFitness(isMale: false);

            newPopulation.Clear();

            Population.ForEach(e => newPopulation.Add(GetChild(maximumMaleFitness, maximumFemaleFitness)));

            // Save memory; switch between lists
            List<DNA<T>> tmpList = Population;
            Population = newPopulation;
            newPopulation = tmpList;
        }

        private double GetMaximumFitness(bool isMale)
        {
            // https://en.wikipedia.org/wiki/Percentile_rank
            double partnerFitnessPercentile = 0.70f;

            // the number of members of the population beloning to the percentile. 
            int elite = (int)(Population.Count * (1 - partnerFitnessPercentile));


            return Population.OrderBy(e => e.Fitness)
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
                int i = (int)(random.NextDouble() * Population.Count);

                if (Population[i].IsMale != isMale)
                    if (Population[i].Fitness <= maximumParentFitness)
                        return Population[i];
            }
        }
    }
}
