﻿using GenericGenetics.Implementations;
using System;

namespace GenericGenetics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Program p = new Program();

            p.Run(args);
        }

        public void Run(string[] args)
        {
            // Evolution<char> evolution = new TextEvolution();

            Evolution<Shape> evolution = new ShapeEvolution();

            try
            {
                evolution.Run();
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
    }
}
