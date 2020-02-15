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
        private Func<int, float> fitnessFunction;

        public GeneticAlgorithm(int populationSize, int dnaSize, Random random, Func<T> getRandomGene,
                                Func<int, float> fitnessFunction, float mutationRate = 0.01f)
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
            if (Population.Count <= 0)
                return;

            if (Population.Count > 0)
            {
                CalculateFitness();
            }
            newPopulation.Clear();

            for (int i = 0; i < Population.Count; i++)
            {
                DNA<T> parent1 = ChooseParent();
                DNA<T> parent2 = ChooseParent();

                DNA<T> child = parent1.Crossover(parent2);

                child.Mutate(MutationRate);

                newPopulation.Add(child);
            }

            List<DNA<T>> tmpList = Population;
            Population = newPopulation;
            newPopulation = tmpList;

            Generation++;
        }

        private void CalculateFitness()
        {
            fitnessSum = 0;
            DNA<T> best = Population[0];

            for (int i = 0; i < Population.Count; i++)
            {
                Population[i].CalculateFitness(i);

                fitnessSum += Population[i].Fitness;

                if (Population[i].Fitness > best.Fitness)
                    best = Population[i];
            }

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
