using sys=System;
using System.Collections.Generic;
using System.Linq;
using GenericGenetics;
using System;
using GenericGenetics.Implementations;

namespace ConsoleUI
{
    public class Matrix
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Matrix()
        {
        }

        public Matrix(int width, int heigth)
        {
            Height = heigth;
            Width = width;
        }

        public void PrintPath(Point[] points, int generation, double fitness, Point startingPoint)
        {
            Point[] path = new Point[points.Length+1];
            path[0] = startingPoint;
            Point currentPoint = startingPoint;

            for (int i = 0; i < points.Length; i++)
            {
                currentPoint += points[i];
                path[i + 1] = new Point(currentPoint.X, currentPoint.Y);
            }

            int minWidth = path.Min(p => p.X);
            int minHeight = path.Min(p => p.Y);

            Width = path.Max(p => p.X) - minWidth + 1;
            Height = path.Max(p => p.Y) - minHeight + 1;
            
            PrintHeader(generation, points.Length, fitness);
            PrintPoints(path, Height, Width, minHeight, minWidth, true);
        }

        public void Print(DNA<Point> genotype, int generation)
        {
            int geneCount = genotype.Genes.Length;

            PrintHeader(generation, geneCount, genotype.Fitness);
            PrintPoints(genotype.Genes, geneCount, geneCount);
        }

        private void PrintPoints(Point[] points, int height, int width, int minHeight = 0, int minWidth = 0, bool path = false)
        {
            char[,] chars = new char[height, width * 2];

            points.ToList().ForEach(p => chars[p.Y-minHeight, (p.X-minWidth) * 2] = '\u25A0');

            if (path)
            {
                chars[points.First().Y - minHeight, (points.First().X - minWidth) * 2] = 'S';
                chars[points.Last().Y - minHeight, (points.Last().X - minWidth) * 2] = 'F';
            }

            for (int i = 0; i < Height; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write((i + 1).ToString("d3") + " ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(GetRow(chars, i));
            }
        }

        private void PrintHeader(int generation, int geneCount, double fitness)
        {
            string ruler = "0 _ _ _ _ _ _ _ 9 1 _ _ _ _ _ _ _ _ 9 2 _ _ _ _ _ _ _ _ 9 3 _ _ _ _ _ _ _ _ 9 5 _ _ _ _ _ _ _ _ 9 40";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\r\n\r\n    Generation: {generation,4:####} Gene count: {geneCount,3:###}  fitness: {fitness,6:0.0000}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("    ");
            Console.WriteLine(ruler.Substring(0, Math.Min(ruler.Length, Width * 2)));
            Console.WriteLine();
        }

        private T[] GetRow<T>(T[,] matrix, int row)
        {
            var rowVector = new T[Width * 2];

            for (var i = 0; i < Width * 2; i++)
                rowVector[i] = matrix[row, i];

            return rowVector;
        }
    }
}
