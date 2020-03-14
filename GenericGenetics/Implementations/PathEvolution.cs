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
            int minDistanceToTarget;
            int minTotalDistance;
            int length;

            EvaluatePath(genotype, out minDistanceToTarget, out minTotalDistance, out length);

            double fitness = minDistanceToTarget + (minTotalDistance / 1000d);

            return fitness;
        }

        public void EvaluatePath(DNA<Point> genotype, out int minDistanceToTarget, out int minTotalDistance, out int length)
        {
            Point currentPoint = StartingPoint;

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
                    length = i + 1;
                }
            }
        }

        internal override Point GetRandomGene(Random random)
        {
            return new Point(random.Next(DnaMinValue, DnaMaxValue), random.Next(DnaMinValue, DnaMaxValue));
        }
    }
}