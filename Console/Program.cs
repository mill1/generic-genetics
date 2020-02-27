
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // new TextEvolutionUI().Run(0.80f, 0.01f);
            new CircleEvolutionUI().Run(8.55f, 0.02f);
        }
    }
}