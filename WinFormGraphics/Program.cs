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
            int i = 2;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (i == 0)
                Application.Run(new OrigForm.MainOrigForm());

            if (i == 1)
            {
                // TEXT
                TextEvolution evolution = new TextEvolution();

                evolution.SetParameters(
                    new Parameters()
                    {
                        TargetFitness = 0.00,
                        PopulationSize = 100,
                        DnaMinValue = 0,
                        DnaMaxValue = -1, // = nr of ValidCharacters
                        MutationRate = 0.02
                    });

                Application.Run(new TextEvolutionForm(evolution));
            }

            if (i == 2)
            {
                //CIRCLE
                CircleEvolution evolution = new CircleEvolution();

                evolution.SetParameters(
                    new Parameters()
                    {
                        TargetFitness = 2.05f,
                        PopulationSize = 100,
                        DnaMinValue = 0,
                        DnaMaxValue = 50,
                        MutationRate = 0.01
                    });

                Application.Run(new CircleEvolutionForm(evolution));
            }
        }
    }
}