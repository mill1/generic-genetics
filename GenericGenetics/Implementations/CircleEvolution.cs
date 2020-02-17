using Jint;
using System;
using System.Collections.Generic;
using System.IO;

namespace GenericGenetics.Implementations
{
    public class CircleEvolution : Evolution<Point>
    {
        public override double TargetFitness { get; } = 0.9999999f;
        public override int PopulationSize { get; } = 40;
        public override int DnaSize { get; set; }
        public override double MutationRate { get; } = 0.01f;

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

            Console.WriteLine($"Nr of points (< {width * height}):");
            DnaSize = int.Parse(Console.ReadLine());

        }

        public override Point GetRandomGene(Random random)
        {
            return new Point(random.Next(matrix.Width), random.Next(matrix.Heigth));
        }

        public override double DetermineFitness(DNA<Point> dna)
        {
            return circleFit.CalculateFitness(dna.Genes);
        }

        public override void DisplayResult(Point[] bestGenes, double bestFitness, int generation)
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

        public void Print(Point[] bestGenes, double bestFitness, int generation)
        {
            Console.WriteLine("{0,5:#####} {1} {2}", generation, bestFitness, bestGenes.Length);

            foreach (Point point in bestGenes)
            {
                Console.WriteLine($"{point.X} {point.Y}");
            }

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
