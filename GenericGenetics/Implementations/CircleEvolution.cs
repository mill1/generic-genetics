using Jint;
using System;
using System.Collections.Generic;
using System.IO;

namespace GenericGenetics.Implementations
{
    public class CircleEvolution : Evolution<Point>
    {
        public override double TargetFitness { get; } = 0.92f;
        public override int PopulationSize { get; } = 200;
        public override int DnaSize { get; set; }
        public override double MutationRate { get; } = 0.02f;

        private CircleFit circleFit;
        private Matrix matrix;

        public CircleEvolution()
        {
            circleFit = new CircleFit();
        }

        public override void GetInput()
        {
            // Matrix: nr of columns: 8
            // Nr of points(< 64): 22

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
            Console.WriteLine("\r\nGeneration: {0,4:####} Gene count: {1} score: {2}\r\n", generation, bestGenes.Length, bestFitness);

            char[,] chars = new char[bestGenes.Length, bestGenes.Length];

            foreach (Point point in bestGenes)
                chars[point.X, point.Y] = '\u25A0';

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
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
