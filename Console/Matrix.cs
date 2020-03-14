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
        public int Heigth { get; private set; }
        public Matrix(int width, int heigth)
        {
            Heigth = heigth;
            Width = width;
        }

        public void Print(Point[] points, int generation)
        {
            PrintHeader(generation, points.Length);
        }

        public void Print(DNA<Point> genotype, int generation)
        {
            int geneCount = genotype.Genes.Length;

            PrintHeader(generation, geneCount, genotype.Fitness);

            char[,] chars = new char[geneCount, geneCount * 2];

            genotype.Genes.ToList().ForEach(p => chars[(int)p.Y, (int)p.X * 2] = '\u25A0');

            for (int i = 0; i < Heigth; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write((i+1).ToString("d3") + " ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(GetRow(chars, i));
            }
        }

        private void PrintHeader(int generation, int geneCount, double fitness = -1)
        {
            string ruler = "0 _ _ _ _ _ _ _ 9 1 _ _ _ _ _ _ _ _ 9 2 _ _ _ _ _ _ _ _ 9 3 _ _ _ _ _ _ _ _ 9 5 _ _ _ _ _ _ _ _ 9 40";

            Console.ForegroundColor = ConsoleColor.Green;

            Console.Write("\r\n\r\n    Generation: {0,4:####} Gene count: {1}", generation, geneCount);
            if (fitness == -1)
                Console.WriteLine();
            else
                Console.WriteLine($" fitness: {fitness}");

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
