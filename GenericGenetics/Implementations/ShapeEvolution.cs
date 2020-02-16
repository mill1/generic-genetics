using System;
using System.Collections.Generic;
using System.Text;

namespace GenericGenetics.Implementations
{
    public class ShapeEvolution : Evolution<Shape>
    {
        public override double TargetFitness { get; } = 1;
        public override int PopulationSize { get; } = 100;
        public override int DnaSize { get; set; }
        public override float MutationRate { get; } = 0.01f;

        public override float DetermineFitness(DNA<Shape> dna)
        {
            return 1;
        }

        public override void DisplayResult(Shape[] bestGenes, float bestFitness, int generation)
        {
            Console.WriteLine("TODO: DisplayResult");
        }

        public override void GetInput()
        {
            Console.WriteLine("TODO: GetInput");

            DnaSize = 100;
        }

        public override Shape GetRandomGene()
        {
            return new Shape();
        }
    }
}
