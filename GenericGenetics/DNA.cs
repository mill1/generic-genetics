using System;
using System.Linq;

namespace GenericGenetics
{
    public class DNA<T>
    {
        public T[] Genes { get;  set; }
        public float Fitness { get; private set; }
        public bool IsMale { get; private set; }

        private Random random;
        private Func<T> getRandomGene;
        private Func<DNA<T>, float> determineFitness;

        public DNA(int size, Random random, Func<T> getRandomGene, Func<DNA<T>, float> determineFitness, bool InitializeGenes = true)
        {
            Genes = new T[size];
            IsMale = random.NextDouble() < 0.5 ? true : false;
            this.random = random;
            this.getRandomGene = getRandomGene;
            this.determineFitness = determineFitness;

            if (InitializeGenes)
                Genes = Genes.Select(g => getRandomGene()).ToArray();
        }

        public void CalculateFitness(DNA<T> dna)
        {
            Fitness = determineFitness(dna);
        }

        public DNA<T> Crossover(DNA<T> otherParent)
        {
            DNA<T> child = new DNA<T>(Genes.Length, random, getRandomGene, determineFitness, InitializeGenes: false);

            child.Genes = Genes.Select((g, i) => random.NextDouble() < 0.5 ? g : otherParent.Genes[i]).ToArray();

            return child;
        }

        public void Mutate(float MutationRate)
        {
            Genes = Genes.Select(g => {
                return random.NextDouble() < MutationRate ? getRandomGene() : g;            
            }).ToArray();
        }
    }
}