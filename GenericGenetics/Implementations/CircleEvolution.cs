using System;
using System.Collections.Generic;
using System.IO;

namespace GenericGenetics.Implementations
{
    public class CircleEvolution : Evolution<Point>
    {
        private const double TARGET_CEILING = 10;
        public override double TargetFitness { get; } = TARGET_CEILING - 0.5f;
        public override int PopulationSize { get; }
        public override int DnaSize { get; set; }
        public override double MutationRate { get; } = 0.02f;

        private Matrix matrix;

        public CircleEvolution()
        {
            Console.WriteLine("Population size:");
            PopulationSize = int.Parse(Console.ReadLine());
        }

        public override void GetInput()
        {
            Console.WriteLine("Output: column size:");
            int width = int.Parse(Console.ReadLine());
            int height = width;

            matrix = new Matrix(width, height);

            Console.WriteLine($"number of points (< {width * height}):");
            DnaSize = int.Parse(Console.ReadLine());
        }

        public override Point GetRandomGene(Random random)
        {
            return new Point(random.Next(matrix.Width), random.Next(matrix.Heigth));
        }

        public override double DetermineFitness(DNA<Point> dna)
        {
            return TARGET_CEILING - new PointsCalculator().Roundness(dna.Genes);
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
            Console.WriteLine("\r\nGeneration: {0,4:####} Gene count: {1} score: {2}\r\n", generation, bestGenes.Length, bestFitness);

            char[,] chars = new char[bestGenes.Length, bestGenes.Length];

            foreach (Point point in bestGenes)
                chars[(int)point.X, (int)point.Y] = '\u25A0';

            Point center = new PointsCalculator().Center(bestGenes);
            chars[center.X, center.Y] = 'O';

            for (int i = 0; i < Width; i++)
            {
                Console.Write($"{i}\t");
                Console.WriteLine(GetRow(chars, i)); 
            }
        }

        private T[] GetRow<T>(T[,] matrix, int row)
        {
            var rowVector = new T[Width];

            for (var i = 0; i < Width; i++)
                rowVector[i] = matrix[row, i];

            return rowVector;
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double DistanceToCenter { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
