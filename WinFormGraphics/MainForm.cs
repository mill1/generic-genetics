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

namespace WinFormGraphics
{
    public partial class MainForm : Form
    {
        private IEnumerable<Point> Points { get; set; }

        public MainForm()
        {
            Points = new List<Point>(){
                    new Point(40,250),
                    new Point(80,300),
                    new Point(120,200)};

            InitializeComponent();
        }

        //Perform any action on click action method
        private void Button1_Click(object sender, EventArgs e)
        {            
            MessageBox.Show("Button was clicked");
            Refresh();
        }

        public override void Refresh()
        {
            Points = new List<Point>(){
                    new Point(40,40),
                    new Point(80,80),
                    new Point(120,120)};

            base.Refresh();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {            
            DrawEllipseRectangles(e, Points);
        }

        private void DrawEllipseRectangles(PaintEventArgs e, IEnumerable<Point> points)
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



        private void ObsoleteMethod(PaintEventArgs e)
        {
            using (var p = new Pen(Color.Black))
            {
                // Specify a collection of points for the curve.
                var points = new Point[]{
                    new Point(40,250),
                    new Point(80,300),
                    new Point(120,200)};

                e.Graphics.DrawCurve(p, points);

                e.Graphics.DrawString(
                        "This is a horizontal text.",
                        this.Font, new SolidBrush(Color.Red), 450, 90, new StringFormat());

            }
        }
    }
}
