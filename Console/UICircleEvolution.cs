using GenericGenetics;
using GenericGenetics.Implementations;
using GenericGenetics.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    public class UICircleEvolution: IUICircleEvolution
    {
        private Matrix matrix;
        private int populationSize;
        private int dnaSize;
        private int dnaMaxValue;

        public void Run(IUICircleEvolution ui)
        {
            // Evolution<char> evolution = new TextEvolution();
            try
            {
                GetParameters();
                Evolution<Point> evolution = new CircleEvolution(8.55f, populationSize, dnaSize, 0, dnaMaxValue, 0.02f);

                evolution.Run(DisplayPhenotype);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.Read();
            }
        }

        private void GetParameters()
        {
            Console.WriteLine("Population size:");
            populationSize = int.Parse(Console.ReadLine());

            Console.WriteLine($"number of points:");
            dnaSize = int.Parse(Console.ReadLine());

            Console.WriteLine("Output: column size:");
            dnaMaxValue = int.Parse(Console.ReadLine());

            matrix = new Matrix(dnaMaxValue, dnaMaxValue);
        }

        private void DisplayPhenotype(DNA<Point> genotype, int generation)
        {
            matrix.Print(genotype, generation);
        }
    }
}
