using GenericGenetics;
using GenericGenetics.Implementations;
using System;
using System.Windows.Forms;

namespace WinFormGraphics
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new OrigForm.MainOrigForm());

            IEvolution<Point> evolution = new CircleEvolution();

            evolution.SetParameters(
                new Parameters()
                {
                    TargetFitness = 2.05f,
                    PopulationSize = 100,
                    DnaSize = 300,
                    DnaMinValue = 0,
                    DnaMaxValue = 50,
                    MutationRate = 0.01
                });

            Application.Run(new CircleEvolutionForm(evolution));
        }
    }
}