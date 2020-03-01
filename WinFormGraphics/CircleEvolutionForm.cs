using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System;
using GenericGenetics;
using GenericGenetics.Implementations;
using Point = System.Drawing.Point;

namespace WinFormGraphics
{
    public partial class CircleEvolutionForm : Form
    {
        private IEnumerable<Point> points = new List<Point>();
        private int generation;
        private double fitness;

        public CircleEvolutionForm()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
        }

        //Perform any action on click action method
        private void Button1_Click(object sender, EventArgs e)
        {
            CircleEvolution evolution = new CircleEvolution(
                new Parameters()
                {
                    TargetFitness = 2.05f,
                    PopulationSize = 100,
                    DnaSize = 300,
                    DnaMinValue = 0,
                    DnaMaxValue = 50,
                    MutationRate = 0.01
                });

            evolution.Run(DisplayPhenotype);
        }

        private void DisplayPhenotype(DNA<GenericGenetics.Implementations.Point> genotype, int generation)
        {

            points = genotype.Genes.Select(g => new Point(Adjust(g.X, false), Adjust(g.Y, true)));
            this.generation = generation;
            fitness = genotype.Fitness;

            Refresh();
        }

        private int Adjust(int i, bool isY)
        {
            return i * 12 + ( isY ? 85 : 50);
        }

        public override void Refresh()
        {
            base.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e) 
        {
            label1.Text = $"Generation {generation}, Fitness: {fitness.ToString("0.00000")}";

            if (points.Count() == 0)
                return;

            DrawEllipseRectangles(e);
        }


        private void DrawEllipseRectangles(PaintEventArgs e)
        {
            using (var pen = new Pen(Color.Black, 1))
            {
                points.ToList().ForEach(p => DrawEllipseRectangle(e, pen, p));
            }
        }

        private void DrawEllipseRectangle(PaintEventArgs e, Pen pen, Point point)
        {

            // Create rectangle for ellipse.
            Rectangle rect = new Rectangle(point, new Size(4, 4));

            // Draw ellipse to screen.
            e.Graphics.DrawEllipse(pen, rect);
        }
    }
}
