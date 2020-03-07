using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System;
using GenericGenetics;

namespace WinFormGraphics
{
    public abstract partial class EvolutionForm<T> : Form
    {
        private readonly IEvolution<T> evolution;

        public EvolutionForm(IEvolution<T> evolution)
        {
            this.evolution = evolution;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
        }

        //Perform any action on click action method
        private void cmdRun_Click(object sender, EventArgs e)
        {
            evolution.Run(DisplayPhenotype);
        }

        internal abstract void DisplayPhenotype(DNA<T> genotype, int generation);

        public override void Refresh()
        {
            base.Refresh();
        }
    }
}
