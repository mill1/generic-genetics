using System;
using System.Linq;

namespace GenericGenetics
{
    public class TextEvolution : Evolution<char>
    {
        public override double TargetFitness { get; } = 0.8f; // 1;
        public override int PopulationSize { get; }
        public override int DnaSize { get; set; }
        public override double MutationRate { get; } = 0.01f;

        private readonly string validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ,.|!#$%&/()=? ";
        private string targetText;

        public TextEvolution()
        {
            PopulationSize = 100;
        }
        public override void GetInput()
        {
            Console.WriteLine("Target text:");
            targetText = Console.ReadLine(); ;

            if (string.IsNullOrEmpty(targetText))
                throw new Exception("Input is null or empty");

            DnaSize = targetText.Length;
        }

        public override char GetRandomGene(Random random)
        {
            int i = random.Next(validCharacters.Length);
            return validCharacters[i];
        }

        public override double DetermineFitness(DNA<char> genotype)
        {
            double score = 0;

            genotype.Genes.Select((g, i) =>
            {
                score += (g == targetText[i] ? 1 : 0);
                return i;
            }).ToList();

            score /= targetText.Length;

            // value proportional improvement by using exponent
            int exp = 2;
            score = (double)(Math.Pow(exp, score) - 1) / (exp - 1);

            return score;
        }

        public override void DisplayPhenotype(DNA<char> genotype, int generation)
        {
            Console.WriteLine("{0,5:#####} {1,6:0.0000} {2}", generation, genotype.Fitness, new string(genotype.Genes));
        }
    }
}
