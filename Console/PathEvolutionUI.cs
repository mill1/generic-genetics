using GenericGenetics;
using GenericGenetics.Implementations;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ConsoleUI
{
    public class PathEvolutionUI
    {

        public void Run(double targetFitness, double mutationRate)
        {
            try
            {
                Point p1 = new Point(1, 2);
                Point p2 = new Point(3, 4);

                //Console.WriteLine(p2.Equals(p1));
                //Console.WriteLine((p1+p2).ToString());

                bool quit = true;

                if (quit)
                    return;

                Console.WriteLine("Population size:");
                int populationSize = int.Parse(Console.ReadLine());

                Console.WriteLine($"Number of vectors:");
                int dnaSize = 100; // int.Parse(Console.ReadLine());

                PathEvolution evolution = new PathEvolution();

                evolution.SetParameters(
                    new Parameters()
                    {
                        TargetFitness = targetFitness,
                        PopulationSize = populationSize,
                        DnaMinValue = -10,
                        DnaMaxValue = 10,
                        MutationRate = mutationRate
                    });

                evolution.Run(dnaSize, DisplayPhenotype);
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
            Console.WriteLine("// TODO");
            //Matrix matrix = new Matrix(dnaMaxValue, dnaMaxValue);
            //matrix.Print(genotype, generation);
        }
    }
}
