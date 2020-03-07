namespace WinFormGraphics
{
    partial class CircleEvolutionForm
    {
        private System.Windows.Forms.Label label2;

        private void InitializeComponentCircle()
        {           
            this.label2 = new System.Windows.Forms.Label();

            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Demonstrate generating a circle from random points.";
            this.Controls.Add(this.label2);

            this.ClientSize = new System.Drawing.Size(700, 730);
            this.Text = "Generic genetics: generate a circle";
        }
    }
}
