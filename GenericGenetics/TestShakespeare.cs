using System;
using System.Collections.Generic;
using System.Text;

namespace GenericGenetics
{
    public class TestShakespeare
    {
        string validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ,.|!#$%&/()=? ";
        int populationSize = 100; // 200;
        float mutationRate = 0.01f; // 0.01f;
        int elitism = 5;

        public string targetText;
        public string populationTextParent;
        public string textPrefab;

        private GeneticAlgorithm<char> ga;
        private System.Random random;

        public void Run()
        {
            Console.WriteLine("Target string:");
            targetText = Console.ReadLine(); //  "To be, or not be. That is the question.";

            if (string.IsNullOrEmpty(targetText))
                throw new Exception("Target string is null or empty");

            random = new System.Random();
            ga = new GeneticAlgorithm<char>(populationSize, targetText.Length, random, GetRandomCharacter, FitnessFunction, elitism, mutationRate);
            
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

            for (int i = 0; i < dna.Genes.Length; i++)
            {
                if (dna.Genes[i] == targetText[i])
                {
                    score += 1;
                }
            }

            score /= targetText.Length;

            score = (float)(Math.Pow(2, score) - 1) / (2 - 1);

            return score;
        }


        private void UpdateText(char[] bestGenes, float bestFitness, int generation)
        {
            Console.WriteLine("{0,5:#####} {1,6:0.0000} {2}", generation, bestFitness, new string(bestGenes));
        }
    }
}
