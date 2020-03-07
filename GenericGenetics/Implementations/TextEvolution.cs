using System;
using System.Linq;

namespace GenericGenetics
{
    public class TextEvolution : Evolution<char>, IEvolution<char>
    {
        private readonly string validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ,.|!#$%&/()=? ";

        public string TargetText { get; set; }

        internal override double DetermineFitness(DNA<char> genotype)
        {
            double score = 0;

            genotype.Genes.Select((c, i) =>
            {
                score += (c == TargetText[i] ? 1 : 0);
                return i;
            }).ToList();

            score /= TargetText.Length;

            // value proportional improvement by using exponent
            int exp = 2;
            score = (double)(Math.Pow(exp, score) - 1) / (exp - 1);

            return 1 - score;
        }

        internal override char GetRandomGene(Random random)
        {
            int i = random.Next(DnaMinValue, validCharacters.Length);
            return validCharacters[i];
        }
    }
}
