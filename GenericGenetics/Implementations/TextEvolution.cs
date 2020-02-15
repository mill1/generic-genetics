using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericGenetics
{
    public class TextEvolution : IEvolution<char>
    {
        string validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ,.|!#$%&/()=? ";
        int populationSize = 150;
        float mutationRate = 0.02f; 

        public string targetText;
        public string populationTextParent;
        public string textPrefab;

        private GeneticAlgorithm<char> ga;
        private Random random;

        public void Run()
        {
            Console.WriteLine("Target text:");
            targetText = Console.ReadLine();;

            if (string.IsNullOrEmpty(targetText))
                throw new Exception("Target string is null or empty");

            random = new Random();
            ga = new GeneticAlgorithm<char>(populationSize, targetText.Length, random, GetRandomCharacter, FitnessFunction, mutationRate);
            
            while (ga.BestFitness < 1)
                Evolve();
        }

        private void Evolve()
        {
            ga.SpawnNewGeneration();
            UpdateText(ga.BestGenes, ga.BestFitness, ga.Generation);
        }

        private char GetRandomCharacter()
        {
            int i = random.Next(validCharacters.Length);
            return validCharacters[i];
        }

        private float FitnessFunction(DNA<char> dna)
        {
            float score = 0;

            dna.Genes.Select((g, i) =>
            {
                score += g == targetText[i] ? 1 : 0;
                return i;
            }).ToList();

            score /= targetText.Length;

            // value proportional improvement by using exponent
            int exp = 2;
            score = (float)(Math.Pow(exp, score) - 1) / (exp - 1);

            return score;
        }

        private void UpdateText(char[] bestGenes, float bestFitness, int generation)
        {
            Console.WriteLine("{0,5:#####} {1,6:0.0000} {2}", generation, bestFitness, new string(bestGenes));
        }
    }
}
