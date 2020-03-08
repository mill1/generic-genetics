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

        public void Print(DNA<Point> genotype, int generation)
        {
            int geneCount = genotype.Genes.Length;

            sys.Console.ForegroundColor = sys.ConsoleColor.Green;
            sys.Console.WriteLine("\r\n\r\n    Generation: {0,4:####} Gene count: {1} score: {2}\r\n", generation, geneCount, genotype.Fitness);

            char[,] chars = new char[geneCount, geneCount * 2];

            genotype.Genes.ToList().ForEach(p => chars[(int)p.Y, (int)p.X * 2] = '\u25A0');

            string ruler = "0 _ _ _ _ _ _ _ 9 1 _ _ _ _ _ _ _ _ 9 2 _ _ _ _ _ _ _ _ 9 3 _ _ _ _ _ _ _ _ 9 5 _ _ _ _ _ _ _ _ 9 40";

            sys.Console.ForegroundColor = sys.ConsoleColor.Yellow;
            sys.Console.Write("    ");
            sys.Console.WriteLine(ruler.Substring(0, sys.Math.Min(ruler.Length, Width * 2)));
            sys.Console.WriteLine();

            for (int i = 0; i < Heigth; i++)
            {
                sys.Console.ForegroundColor = sys.ConsoleColor.Yellow;
                sys.Console.Write((i+1).ToString("d3") + " ");
                sys.Console.ForegroundColor = sys.ConsoleColor.White;
                sys.Console.WriteLine(GetRow(chars, i));
            }
        }

        //internal void Print<Point>(DNA<Point> genotype, int generation)
        //{
        //    throw new NotImplementedException();
        //}

        private T[] GetRow<T>(T[,] matrix, int row)
        {
            var rowVector = new T[Width * 2];

            for (var i = 0; i < Width * 2; i++)
                rowVector[i] = matrix[row, i];

            return rowVector;
        }
    }
}
