using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericGenetics
{
    public class GeneticAlgorithm<T>
    {
        public List<DNA<T>> Population { get; private set; }
        public int Generation { get; private set; }
        public float BestFitness { get; private set; }
        public T[] BestGenes { get; private set; }
        public float MutationRate { get; private set; }

        private List<DNA<T>> newPopulation;
        private Random random;
        private float fitnessSum;
        private Func<T> getRandomGene;
        private Func<DNA<T>, float> fitnessFunction;

        public GeneticAlgorithm(int populationSize, int dnaSize, Random random, Func<T> getRandomGene,
                                Func<DNA<T>, float> fitnessFunction, float mutationRate = 0.01f)
        {
            Generation = 1;
            MutationRate = mutationRate;
            Population = new List<DNA<T>>(populationSize);
            newPopulation = new List<DNA<T>>(populationSize);
            this.random = random;
            this.getRandomGene = getRandomGene;
            this.fitnessFunction = fitnessFunction;

            BestGenes = new T[dnaSize];

            Population = Enumerable.Range(0, populationSize).Select(x => new DNA<T>(dnaSize, random, getRandomGene, fitnessFunction, InitializeGenes: true)).ToList();
        }

        public void NewGeneration()
        {
            CalculatePopulationFitness();

            newPopulation.Clear();

            Population.ForEach(e => newPopulation.Add(GetChild()));

            // Save memory; switch between lists
            List<DNA<T>> tmpList = Population;
            Population = newPopulation;
            newPopulation = tmpList;

            Generation++;
        }

        private DNA<T> GetChild()
        {
            DNA<T> parent1 = ChooseParent();
            DNA<T> parent2 = ChooseParent();

            DNA<T> child = parent1.Crossover(parent2);

            child.Mutate(MutationRate);
            return child;
        }

        private void CalculatePopulationFitness()
        {
            fitnessSum = 0;
            DNA<T> best = Population[0];

            Population.ForEach(e => 
                {
                    e.CalculateFitness(e);
                    fitnessSum += e.Fitness;
                });

            best = Population.OrderByDescending(e => e.Fitness).FirstOrDefault();

            BestFitness = best.Fitness;
            best.Genes.CopyTo(BestGenes, 0);
        }

        private DNA<T> ChooseParent()
        {
            double randomNumber = random.NextDouble() * fitnessSum;

            for (int i = 0; i < Population.Count; i++)
            {
                if (randomNumber < Population[i].Fitness)
                    return Population[i];

                randomNumber -= Population[i].Fitness;
            }
            return null;
        }
    }
}
