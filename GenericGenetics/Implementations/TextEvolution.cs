using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericGenetics
{
    public class TextEvolution
    {
        string validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ,.|!#$%&/()=? ";
        int populationSize = 100; // 200;
        float mutationRate = 0.01f; // 0.01f;

        public string targetText;
        public string populationTextParent;
        public string textPrefab;

        private GeneticAlgorithm<char> ga;
        private System.Random random;

        public void Run()
        {
            Console.WriteLine("Target text:");
            targetText = Console.ReadLine();;

            if (string.IsNullOrEmpty(targetText))
                throw new Exception("Target string is null or empty");

            random = new System.Random();
            ga = new GeneticAlgorithm<char>(populationSize, targetText.Length, random, GetRandomCharacter, FitnessFunction, mutationRate);
            
            while (ga.BestFitness < 1)
                Update();
        }

        private void Update()
        {
            ga.NewGeneration();
            UpdateText(ga.BestGenes, ga.BestFitness, ga.Generation);
        }

        private char GetRandomCharacter()
        {
            int i = random.Next(validCharacters.Length);
            return validCharacters[i];
        }

        private float FitnessFunction(int index)
        {
            float score = 0;
            DNA<char> dna = ga.Population[index];

            dna.Genes.Select((g, i) => {
                Console.Write(i);
                return i;
            }); //.ToList() forces immediate query evaluation and returns a List<T> that contains the query results.

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
