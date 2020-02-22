using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericGenetics.Implementations
{
    public class CircleEvolution : Evolution<Point>
    {
        private const double TARGET_CEILING = 10;
        public override double TargetFitness { get; } = TARGET_CEILING - 1.3f; // 200, 33, 99, percentile=75
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

        public override double DetermineFitness(DNA<Point> genotype)
        {
            return TARGET_CEILING - new PointsCalculator().Roundness(genotype.Genes);
        }

        public override void DisplayPhenotype(DNA<Point> genotype, int generation)
        {
            matrix.Print(genotype, generation);
        }
    }
        
    public class Matrix
    {
        public int Width { get; private set; }
        public int Heigth { get; private set; }
        public Matrix(int width, int heigth)
        {
            Heigth = heigth;
            Width = width;
        }

        public void Print(DNA<Point> genotype, int generation)
        {
            int geneCount = genotype.Genes.Length;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\r\n\r\n    Generation: {0,4:####} Gene count: {1} score: {2}\r\n", generation, geneCount, genotype.Fitness);

            char[,] chars = new char[geneCount, geneCount * 2];

            genotype.Genes.ToList().ForEach(p => chars[(int)p.Y, (int)p.X * 2] = '\u25A0');

            string ruler = "0________91________92________93________94________95________96________97________98________99_______99";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("    ");
            Console.WriteLine(ruler.Substring(0, Math.Min(ruler.Length, Width * 2)));
            Console.WriteLine();

            Point center = new PointsCalculator().Center(genotype.Genes);
            // chars[center.X, center.Y] = 'O';

            for (int i = 0; i < Heigth; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(i.ToString("d3") + " ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(GetRow(chars, i)); 
            }
        }

        private T[] GetRow<T>(T[,] matrix, int row)
        {
            var rowVector = new T[Width * 2];

            for (var i = 0; i < Width * 2; i++)
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
