using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System;
using GenericGenetics;
using Point = GenericGenetics.Implementations.Point;

namespace WinFormGraphics
{
    public partial class CircleEvolutionForm: EvolutionForm<Point>
    {
        private IEnumerable<System.Drawing.Point> points = new List<System.Drawing.Point>();
        private double fitness;

        public CircleEvolutionForm(IEvolution<Point> evolution) : base(evolution)
        {
            InitializeComponentCircle();
        }

        internal override void DisplayPhenotype(DNA<Point> genotype, int generation)
        {
            lblGenerationCount.Visible = true;
            lblGenerationCount.Text = $"Generation {generation}, Fitness: {fitness.ToString("0.00000")}";

            points = genotype.Genes.Select(g => new System.Drawing.Point(Adjust(g.X, false), Adjust(g.Y, true)));
            fitness = genotype.Fitness;

            Refresh();
        }

        private int Adjust(int i, bool isY)
        {
            return i * 12 + (isY ? 95 : 50);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
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

        private void DrawEllipseRectangle(PaintEventArgs e, Pen pen, System.Drawing.Point point)
        {
            Rectangle rect = new Rectangle(point, new Size(4, 4));
            e.Graphics.DrawEllipse(pen, rect);
        }
    }
}
