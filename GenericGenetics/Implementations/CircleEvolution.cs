using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericGenetics.Implementations
{
    public class CircleEvolution : Evolution<Point>
    {
        public CircleEvolution(double targetFitness, int populationSize, int dnaSize, int dnaMinValue, int dnaMaxValue, double mutationRate)
                              : base( targetFitness, populationSize, dnaSize, dnaMinValue, dnaMaxValue, mutationRate)
        {
        }

        public override Point GetRandomGene(Random random)
        {
           return new Point(random.Next(DnaMinValue, DnaMaxValue), random.Next(DnaMinValue, DnaMaxValue));
        }

        public override double DetermineFitness(DNA<Point> genotype)
        {
            return 10 - new PointsCalculator().Roundness(genotype.Genes);
        }
    }
}
