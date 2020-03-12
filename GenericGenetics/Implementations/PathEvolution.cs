using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericGenetics.Implementations
{
    public class PathEvolution : Evolution<Point>, IEvolution<Point>
    {
        public Point TargetPoint { get; set; }

        internal override double DetermineFitness(DNA<Point> genotype)
        {
            Point currentPoint = new Point(25, 0); // TODO

            int minDistanceToTarget = (TargetPoint - (currentPoint + genotype.Genes[0])).Value + 1;
            int totalDistance = 0;
            int minTotalDistance = 0;

            foreach (Point point in genotype.Genes)
            {
                totalDistance += point.Value;

                currentPoint += point;
                point.DistanceToTarget = (TargetPoint - currentPoint).Value;

                if (point.DistanceToTarget < minDistanceToTarget)
                {
                    minDistanceToTarget = point.DistanceToTarget;
                    minTotalDistance = totalDistance;
                }
            }

            double fitness = minDistanceToTarget + (minTotalDistance / 1000d);

            return fitness;
        }

        internal override Point GetRandomGene(Random random)
        {
            return new Point(random.Next(DnaMinValue, DnaMaxValue), random.Next(DnaMinValue, DnaMaxValue));
        }
    }
}