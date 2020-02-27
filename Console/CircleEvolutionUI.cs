using GenericGenetics;
using GenericGenetics.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    public class CircleEvolutionUI
    {
        private int dnaMaxValue;

        public void Run(double targetFitness, double mutationRate)
        {
            try
            {
                Console.WriteLine("Population size:");
                int populationSize = int.Parse(Console.ReadLine());

                Console.WriteLine($"Number of points:");
                int dnaSize = int.Parse(Console.ReadLine());

                Console.WriteLine("Max. x/y value:");
                dnaMaxValue = int.Parse(Console.ReadLine());

                CircleEvolution evolution = new CircleEvolution(

                    new Parameters()
                    {
                        TargetFitness = targetFitness,
                        PopulationSize = populationSize,
                        DnaSize = dnaSize,
                        DnaMinValue = 0,
                        DnaMaxValue = dnaMaxValue,
                        MutationRate = mutationRate
                    });

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

        private void DisplayPhenotype(DNA<Point> genotype, int generation)
        {
            Matrix matrix = new Matrix(dnaMaxValue, dnaMaxValue);
            matrix.Print(genotype, generation);
        }
    }
}
