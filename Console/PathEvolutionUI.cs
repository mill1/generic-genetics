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
        PathEvolution evolution = new PathEvolution();

        public void Run(double targetFitness, double mutationRate)
        {
            this.targetFitness = targetFitness;

            try
            {
                Console.WriteLine("Population size:");
                int populationSize = int.Parse(Console.ReadLine());

                evolution.StartingPoint = new Point(0, 0);
                evolution.TargetPoint = new Point(50, 30);

                //Number of vectors (=points). Based on DnaMinValue/DnaMaxValue = (-)2:
                int dnaSize = (int)((evolution.TargetPoint - evolution.StartingPoint).Value * 1.2f); 

                evolution.SetParameters(
                    new Parameters()
                    {
                        TargetFitness = targetFitness,
                        PopulationSize = populationSize,
                        DnaMinValue = -2, //-9,
                        DnaMaxValue = 2,  // 9,
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
            // err.: genotype.Genes.Min(p => p.DistanceToTarget);

            evolution.EvaluatePath(genotype, out int minDistanceToTarget, out int minTotalDistance, out int length);

            Console.Write($"\r\n    Gap: {minDistanceToTarget,2:##} distance: {minTotalDistance,3:##0}");

            Point[] points = new Point[length];
            Array.Copy(genotype.Genes, points, length);

            new Matrix().PrintPath(points, generation, genotype.Fitness, evolution.StartingPoint);

        }
    }
}
