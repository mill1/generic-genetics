using Jint;
using System;
using System.Collections.Generic;
using System.IO;

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

        public void TestRunJavaScript()
        {
            var engine = new Jint.Engine();

            engine.Execute(File.ReadAllText("circlefit.js"));

            
            engine.Execute("CIRCLEFIT.resetPoints()");

            engine.Execute("CIRCLEFIT.addPoint(11, 24)");
            engine.Execute("CIRCLEFIT.addPoint(28, 32)");
            engine.Execute("CIRCLEFIT.addPoint(62, 1)");
            engine.Execute("CIRCLEFIT.addPoint(41, 15)");

            // success(Boolean) : status of the computation
            // points(Array) : all points given by the user
            // projections(Array) : projections of each points onto the circle
            // distances(Array) : distance of each points to the circle
            // center(Object) : center of the circle
            // radius(Number) : radius of the circle
            // residue(Number) : residue of the least squares method, can be use to define the quality of the circle
            // computationTime(Number) : time spent in computation(in milliseconds)


            // var result = engine.Execute("CIRCLEFIT.compute().radius").GetCompletionValue();
            // var result = engine.Execute("CIRCLEFIT.compute().success").GetCompletionValue();
            // result = engine.Execute("CIRCLEFIT.compute().center.x").GetCompletionValue();

            // residue of the least squares method, can be use to define the quality of the circle
            // var result = engine.Execute("CIRCLEFIT.compute().residue").GetCompletionValue();

            dynamic result = engine.Execute("CIRCLEFIT.compute()").GetCompletionValue().ToObject();

            var kvp = result.success;

            if (result.success)
                Console.WriteLine($"Quality of circle: {result.residue}");
            else
                throw new Exception("Could not compute");
        }


        public override void GetInput()
        {
            TestRunJavaScript();

            Console.WriteLine("TODO: GetInput");

            DnaSize = 100;
        }

        public override void DisplayResult(Shape[] bestGenes, float bestFitness, int generation)
        {
            Console.WriteLine("TODO: DisplayResult");
        }

        public override Shape GetRandomGene()
        {
            return new Shape();
        }
    }
}
