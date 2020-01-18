using System;
using System.Collections.Generic;
using System.Text;

namespace GenericGenetics
{
    public class TestShakespeare
    {
        string targetString = "To be, or not to be, that is the question.";
        string validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ,.|!#$%&/()=? ";
        int populationSize = 200;
        float mutationRate = 0.01f;
        int elitism = 5;

        int numCharsPerText = 15000;
        public string targetText;
        public string bestText;
        public string bestFitnessText;
        public string numGenerationsText;
        public string populationTextParent;
        public string textPrefab;

        private GeneticAlgorithm<char> ga;
        private System.Random random;

        public void Start()
        {
            targetText = targetString;

            if (string.IsNullOrEmpty(targetString))
            {
                throw new Exception("Target string is null or empty");
            }

            random = new System.Random();
            ga = new GeneticAlgorithm<char>(populationSize, targetString.Length, random, GetRandomCharacter, FitnessFunction, elitism, mutationRate);
        }

        public void Update()
        {
            ga.NewGeneration();

            UpdateText(ga.BestGenes, ga.BestFitness, ga.Generation, ga.Population.Count, (j) => ga.Population[j].Genes);

            if (ga.BestFitness == 1)
            {
                throw new Exception("Done"); // this.enabled = false;
            }
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
                if (dna.Genes[i] == targetString[i])
                {
                    score += 1;
                }
            }

            score /= targetString.Length;

            score = (float)(Math.Pow(2, score) - 1) / (2 - 1);

            return score;
        }

        private int numCharsPerTextObj;
        private List<string> textList = new List<string>();

        public void Awake()
        {
            numCharsPerTextObj = numCharsPerText / validCharacters.Length;
            if (numCharsPerTextObj > populationSize) numCharsPerTextObj = populationSize;

            int numTextObjects = (int) Math.Ceiling((float)populationSize / numCharsPerTextObj);

            for (int i = 0; i < numTextObjects; i++)
            {
                Console.WriteLine($"i: {i} textPrefab: {textPrefab} pop. parent{populationTextParent}");
                textList.Add(textPrefab);
            }
        }

        private void UpdateText(char[] bestGenes, float bestFitness, int generation, int populationSize, Func<int, char[]> getGenes)
        {
            bestText = CharArrayToString(bestGenes);
            bestFitnessText = bestFitness.ToString();

            numGenerationsText = generation.ToString();

            for (int i = 0; i < textList.Count; i++)
            {
                var sb = new StringBuilder();
                int endIndex = i == textList.Count - 1 ? populationSize : (i + 1) * numCharsPerTextObj;
                for (int j = i * numCharsPerTextObj; j < endIndex; j++)
                {
                    foreach (var c in getGenes(j))
                    {
                        sb.Append(c);
                    }
                    if (j < endIndex - 1) sb.AppendLine();
                }

                textList[i] = sb.ToString();

                Console.WriteLine($"i: {i} textList[i]: {textList[i]}");
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\r\n{targetText}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(bestFitnessText);
            Console.WriteLine(bestText);
            Console.WriteLine(numGenerationsText);
        }

        private string CharArrayToString(char[] charArray)
        {
            var sb = new StringBuilder();
            foreach (var c in charArray)
            {
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
