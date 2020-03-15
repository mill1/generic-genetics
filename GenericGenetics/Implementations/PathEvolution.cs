using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericGenetics.Implementations
{
    public class PathEvolution : Evolution<Point>, IEvolution<Point>
    {
        public Point TargetPoint { get; set; }
        public Point StartingPoint { get; set; }

        internal override double DetermineFitness(DNA<Point> genotype)
        {
            EvaluatePath(genotype, out int minDistanceToTarget, out int minTotalDistance, out int length);

            double fitness = minDistanceToTarget + (minTotalDistance / 1000d);

            return fitness;
        }

        public void EvaluatePath(DNA<Point> genotype, out int minDistanceToTarget, out int minTotalDistance, out int length)
        {
            Point currentPoint = StartingPoint;
            bool obstacle = false;

            minDistanceToTarget = (TargetPoint - (currentPoint + genotype.Genes[0])).Value + 1;
            minTotalDistance = 0;
            length = 0;
            int totalDistance = 0;

            for (int i = 0; i < genotype.Genes.Length; i++)
            {
                totalDistance += genotype.Genes[i].Value;
                currentPoint += genotype.Genes[i];

                if ((TargetPoint - currentPoint).Value < minDistanceToTarget)
                {
                    minDistanceToTarget = (TargetPoint - currentPoint).Value;
                    minTotalDistance = totalDistance;

                    if (currentPoint.Y > 5 && currentPoint.X < 45)
                        obstacle = true;

                    //if (currentPoint.X > 5 && currentPoint.X < 18 && currentPoint.Y < 31)
                    //    obstacle = true;

                    length = i + 1;
                }  
            }

            if (obstacle)
                minDistanceToTarget += 1000;
        }

        internal override Point GetRandomGene(Random random)
        {
            return new Point(random.Next(DnaMinValue, DnaMaxValue), random.Next(DnaMinValue, DnaMaxValue));
        }
    }
}