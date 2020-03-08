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

            int distance = (TargetPoint - currentPoint).Value;
            int delta = distance;

            foreach (Point point in genotype.Genes)
            {
                currentPoint += point;

                if (currentPoint == TargetPoint)
                    return 0;
            }

            return 1;
        }

        internal override Point GetRandomGene(Random random)
        {
            return new Point(random.Next(DnaMinValue, DnaMaxValue), random.Next(DnaMinValue, DnaMaxValue));
        }
    }
}
