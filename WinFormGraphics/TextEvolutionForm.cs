using GenericGenetics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormGraphics
{
    public partial class TextEvolutionForm : EvolutionForm<char>
    {
        TextEvolution textEvolution;
        public TextEvolutionForm(IEvolution<char> evolution) : base(evolution)
        {
            textEvolution = (TextEvolution)evolution;
            EnableRunButton(false);

            InitializeComponent();
        }

        private void txtTargetText_KeyPress(object sender, KeyPressEventArgs e)
        {
            EnableRunButton(txtTargetText.Text.Length >= 3);
        }

        internal override int GetDnaSize()
        {
            return txtTargetText.Text.Length;
        }

        internal override void DisplayPhenotype(DNA<char> genotype, int generation)
        {
            textEvolution.TargetText = txtTargetText.Text;
            lblGenerationCount.Visible = true;
            lblGenerationCount.Text = $"Generation {generation}, Fitness: {genotype.Fitness.ToString("0.00000")}";
            lblResult.Text = new string(genotype.Genes);

            Refresh();
        }
    }
}
