using System;
using System.Linq;

namespace GenericGenetics
{
    public class DNA<T>
    {
        public T[] Genes { get;  set; }
        public float Fitness { get; private set; }

        private Random random;
        private Func<T> getRandomGene;
        private Func<int, float> fitnessFunction;

        public DNA(int size, Random random, Func<T> getRandomGene, Func<int, float> fitnessFunction, bool InitializeGenes = true)
        {
            Genes = new T[size];
            this.random = random;
            this.getRandomGene = getRandomGene;
            this.fitnessFunction = fitnessFunction;

            if (InitializeGenes)
                Genes = Genes.Select(g => getRandomGene()).ToArray();
        }

        public void CalculateFitness(int index)
        {
            Fitness = fitnessFunction(index);
        }

        public DNA<T> Crossover(DNA<T> otherParent)
        {
            DNA<T> child = new DNA<T>(Genes.Length, random, getRandomGene, fitnessFunction, InitializeGenes: false);

            child.Genes = Genes.Select((g, i) => random.NextDouble() < 0.5 ? g : otherParent.Genes[i]).ToArray();

            return child;
        }

        public void Mutate(float mutationRate)
        {
            Genes = Genes.Select(g => {
                return random.NextDouble() < mutationRate ? getRandomGene() : g;            
            }).ToArray();

        }
    }
}