using GenericGenetics;
using GenericGenetics.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI
{
    public class PathEvolutionUI
    {
        double targetFitness;

        public void Run(double targetFitness, double mutationRate)
        {
            this.targetFitness = targetFitness;

            try
            {
                bool quit = false;

                if (quit)
                    return;

                Console.WriteLine("Population size:");
                int populationSize = int.Parse(Console.ReadLine());

                //Number of vectors:
                int dnaSize = 127; 

                PathEvolution evolution = new PathEvolution();

                evolution.SetParameters(
                    new Parameters()
                    {
                        TargetFitness = targetFitness,
                        PopulationSize = populationSize,
                        DnaMinValue = -9,
                        DnaMaxValue = 9,
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
            int minGap = genotype.Genes.Min(p => p.Value);

            Console.WriteLine($"{generation,5:#####}  {genotype.Fitness,7:0.00000}  (min. gap: {minGap})");

            if (genotype.Fitness <= targetFitness)
            { 
                Console.WriteLine($"Path:");

                for (int i = 0; i < genotype.Genes.Length; i++)
                {
                    Point p = genotype.Genes[i];
                    Console.WriteLine($"Step {i,5:####0}\t{p}");

                    if (p.Value == minGap)
                        break;
                }
            }
        }
    }
}
