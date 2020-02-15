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

        public void SpawnNewGeneration()
        {
            Population.ForEach(e => e.CalculateFitness(e));
            fitnessSum = Population.Select(e => e.Fitness).Sum();

            DetermineBestGenes();

            newPopulation.Clear();

            Population.ForEach(e => newPopulation.Add(GetChild()));

            // Save memory; switch between lists
            List<DNA<T>> tmpList = Population;
            Population = newPopulation;
            newPopulation = tmpList;

            Generation++;
        }

        private void DetermineBestGenes()
        {
            DNA<T> best = Population.OrderByDescending(e => e.Fitness).FirstOrDefault();

            BestFitness = best.Fitness;
            best.Genes.CopyTo(BestGenes, 0);
        }

        private DNA<T> GetChild()
        {
            DNA<T> parent1 = ChooseParent(isMale:true);
            DNA<T> parent2 = ChooseParent(isMale:false);

            DNA<T> child = parent1.Crossover(parent2);

            child.Mutate(MutationRate);
            return child;
        }

        private DNA<T> ChooseParent(bool isMale)
        {
            float average = Population.Select(e => e.Fitness).Average();

            while (true)
            {
                int i = (int)(Population.Count * random.NextDouble());

                if (Population[i].IsMale != isMale)
                    if (Population[i].Fitness > average)
                        return Population[i];
            }
        }

        private DNA<T> ChooseParentOrig()
        {
            Console.WriteLine("\r\nChoose parent");
            double randomNumber = random.NextDouble() * fitnessSum;

            for (int i = 0; i < Population.Count; i++)
            {
                Console.WriteLine($"i={i} randomNumber={randomNumber} e.Fitness={Population[i].Fitness}"); 

                if (randomNumber < Population[i].Fitness)
                {
                    Console.WriteLine($"i={i} randomNumber={randomNumber} e.Fitness={Population[i].Fitness} parent={i}");
                    Console.ReadLine();
                    return Population[i];
                }

                randomNumber -= Population[i].Fitness;
            }
            return null; // never happens
        }
    }
}
