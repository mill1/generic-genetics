using Jint;
using System;
using System.Collections.Generic;
using System.IO;

namespace GenericGenetics.Implementations
{
    public class CircleEvolution : Evolution<Point>
    {
        public override double TargetFitness { get; } = 0.001;
        public override int PopulationSize { get; } = 10;
        public override int DnaSize { get; set; }
        public override float MutationRate { get; } = 0.01f;

        private CircleFit circleFit;
        private Matrix matrix;

        public CircleEvolution()
        {
            circleFit = new CircleFit();
        }

        public override void GetInput()
        {

            Console.WriteLine("Matrix: nr of columns:");
            int width = int.Parse(Console.ReadLine());
            int height = width;
            // Ellipse later;
            //Console.WriteLine("Matrix: nr of rows:");
            //int height = int.Parse(Console.ReadLine());

            matrix = new Matrix(width, height);

            Console.WriteLine("Nr of points:");
            DnaSize = int.Parse(Console.ReadLine());
        }

        public override Point GetRandomGene(Random random)
        {
            return new Point(random.Next(matrix.Width), random.Next(matrix.Heigth));
        }

        public override float DetermineFitness(DNA<Point> dna)
        {
            return circleFit.CalculateFitness(dna.Genes);
        }

        public override void DisplayResult(Point[] bestGenes, float bestFitness, int generation)
        {
            matrix.Print(bestGenes, bestFitness, generation);
        }
    }
        
    public class Matrix
    {
        public int Width { get; private set; }
        public int Heigth { get; private set; }
        public Matrix(int width, int heigth)
        {
            Width = width;
            Heigth = heigth;
        }

        public void Print(Point[] bestGenes, float bestFitness, int generation)
        {
            Console.WriteLine("{0,5:#####} {1,6:0.0000} {2}", generation, bestFitness, bestGenes.Length);
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
