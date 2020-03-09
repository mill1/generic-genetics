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
                Point p2 = new Point(-2, -2);

                //Console.WriteLine(p2.Equals(p1));
                //Console.WriteLine((p1 + p2).ToString());
                Console.WriteLine((p1 - p2).Value);
                //Console.WriteLine(p1.Value);

                bool quit = false;

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

                // This weird structure is a result of the fact that a specific end state is sought.
                evolution.TargetPoint = new Point(21, 34);

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
            Console.WriteLine($"{generation,5:#####} {genotype.Fitness,6:0.0000}");
        }
    }
}
