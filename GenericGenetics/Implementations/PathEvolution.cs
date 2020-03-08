﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericGenetics.Implementations
{
    public class PathEvolution : Evolution<Point>, IEvolution<Point>
    {
        internal override double DetermineFitness(DNA<Point> genotype)
        {
            return new PointsCalculator().Roundness(genotype.Genes);
        }

        internal override Point GetRandomGene(Random random)
        {
            return new Point(random.Next(DnaMinValue, DnaMaxValue), random.Next(DnaMinValue, DnaMaxValue));
        }
    }
}