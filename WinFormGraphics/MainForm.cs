/************************************* Module Header **************************************\
* Module Name:  MainForm.cs
* Project:      WinFormGraphics
* Copyright (c) Microsoft Corporation.
* 
* The Graphics sample demonstrates the fundamentals of GDI+ programming. 
* GDI+ allows you to create graphics, draw text, and manipulate graphical images as objects. 
* GDI+ is designed to offer performance as well as ease of use. 
* You can use GDI+ to render graphical images on Windows Forms and controls. 
* GDI+ has fully replaced GDI, and is now the only way to render graphics programmatically 
* in Windows Forms applications.
*
* In this sample, there're 5 examples:
* 
* 1. Draw A Line.
*    Demonstrates how to draw a solid/dash/dot line.
* 2. Draw A Curve.
*    Demonstrates how to draw a curve, and the difference between antialiasing rendering mode
*    and no antialiasing rendering mode.
* 3. Draw An Arrow.
*    Demonstrates how to draw an arrow.
* 4. Draw A Vertical String.
*    Demonstrates how to draw a vertical string.
* 5. Draw A Ellipse With Gradient Brush.
*    Demonstrates how to draw a shape with gradient effect.
* 
* THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
* EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
* WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
* ***************************************************************************/

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
    public partial class MainForm : Form
    {
        private IEnumerable<Point> points = new List<Point>();
        private int generation;
        private double fitness;

        public MainForm()
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
                    TargetFitness = 8.05f,
                    PopulationSize = 50, //100,
                    DnaSize = 550, //300,
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
