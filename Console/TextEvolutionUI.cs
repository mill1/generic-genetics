using GenericGenetics;
using GenericGenetics.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    public class TextEvolutionUI
    {
        public void Run(double targetFitness, double mutationRate)
        {
            try
            {
                Console.WriteLine("Target text:");
                string targetText = Console.ReadLine();

                Console.WriteLine("Population size:");
                int populationSize = int.Parse(Console.ReadLine());

                TextEvolution evolution = new TextEvolution();

                evolution.SetParameters(
                    new Parameters()
                    {
                        TargetFitness = targetFitness,
                        PopulationSize = populationSize,
                        DnaSize = targetText.Length,
                        DnaMinValue = 0,
                        DnaMaxValue = -1, // = nr of ValidCharacters
                        MutationRate = mutationRate
                    });

                // This weird structure is a result of the fact that a specific end state is sought.
                evolution.TargetText = targetText;

                evolution.Run(DisplayPhenotype);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.Read();
            }
        }

        private void DisplayPhenotype(DNA<char> genotype, int generation)
        {
            Console.WriteLine("{0,5:#####} {1,6:0.0000} {2}", generation, genotype.Fitness, new string(genotype.Genes));
        }
    }
}
