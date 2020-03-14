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
                bool quit = false;

                if (quit)
                    return;

                Console.WriteLine("Population size:");
                int populationSize = int.Parse(Console.ReadLine());

                //Number of vectors:
                int dnaSize = 40; 

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
                evolution.StartingPoint = new Point(25, 0);
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
            // err.: genotype.Genes.Min(p => p.DistanceToTarget);

            Console.WriteLine($"{generation,5:#####}  {genotype.Fitness,5:0.000}");

            if (genotype.Fitness <= targetFitness)
            { 
                int minDistanceToTarget;
                int minTotalDistance;
                int length;

                evolution.EvaluatePath(genotype, out minDistanceToTarget, out minTotalDistance, out length);

                Console.WriteLine($"Gap: {minDistanceToTarget,2:#0} length: {length,2:#0} distance: {minTotalDistance,3:##0}");

                Point[] path = new Point[length];
                Array.Copy(genotype.Genes, path, length);

                for (int i = 0; i < path.Length; i++)
                {
                    Console.WriteLine($"Step {i+1,3:##0}\t{genotype.Genes[i]}");
                }

                Matrix matrix = new Matrix(40, 40);
                matrix.Print(path, generation);

            }
        }
    }
}
