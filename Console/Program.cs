
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //new TextEvolutionUI().Run(0.01f, 0.01f);
            //new CircleEvolutionUI().Run(1.55f, 0.02f);
            new PathEvolutionUI().Run(0.099f, 0.02f);
        }
    }
}