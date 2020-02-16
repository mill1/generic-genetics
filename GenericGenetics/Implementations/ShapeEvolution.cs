using System;
using System.Collections.Generic;
using System.Text;

namespace GenericGenetics.Implementations
{
    public class ShapeEvolution : Evolution<Shape>
    {
        public override double TargetFitness => throw new NotImplementedException();

        public override int PopulationSize => throw new NotImplementedException();

        public override int DnaSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override float MutationRate => throw new NotImplementedException();

        public override float DetermineFitness(DNA<Shape> dna)
        {
            throw new NotImplementedException();
        }

        public override void DisplayResult(Shape[] bestGenes, float bestFitness, int generation)
        {
            throw new NotImplementedException();
        }

        public override void GetInput()
        {
            throw new NotImplementedException();
        }

        public override Shape GetRandomGene()
        {
            throw new NotImplementedException();
        }
    }
}
