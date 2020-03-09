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

            int minGap = (TargetPoint - currentPoint).Value;
            int gap;
            int step = 0;
            int minStep = step;

            foreach (Point point in genotype.Genes)
            {
                step++;
                currentPoint += point;

                gap = (TargetPoint - currentPoint).Value;

                if (gap < minGap)
                {
                    minGap = gap;
                    minStep = step;
                }
            }
            double fitness = (double)((minGap * 10) + minStep) / genotype.Genes.Length;

            Console.WriteLine($"{minGap} {minStep}");

            return fitness;
        }

        internal override Point GetRandomGene(Random random)
        {
            return new Point(random.Next(DnaMinValue, DnaMaxValue), random.Next(DnaMinValue, DnaMaxValue));
        }
    }
}
