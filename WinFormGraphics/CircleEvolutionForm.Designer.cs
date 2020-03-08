namespace WinFormGraphics
{
    partial class CircleEvolutionForm
    {
        private System.Windows.Forms.Label lblDnaSize;
        private System.Windows.Forms.TextBox txtDnaSize;
        private System.Windows.Forms.Label label3;

        private void InitializeComponent()
        {
            this.lblDnaSize = new System.Windows.Forms.Label();
            this.lblDnaSize.Location = new System.Drawing.Point(380, 28);
            this.lblDnaSize.Size = new System.Drawing.Size(110, 16);
            this.lblDnaSize.Text = "Number of points:";
            this.Controls.Add(this.lblDnaSize);

            this.txtDnaSize = new System.Windows.Forms.TextBox();
            this.txtDnaSize.Location = new System.Drawing.Point(500, 25);
            this.txtDnaSize.Size = new System.Drawing.Size(40, 16);
            this.txtDnaSize.Text = "250";
            this.txtDnaSize.KeyPress += txtDnaSize_KeyPress;
            this.Controls.Add(this.txtDnaSize);

            this.label3 = new System.Windows.Forms.Label();
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Demonstrate generating a circle from random points.";
            this.Controls.Add(this.label3);

            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Top = 20;
            this.Left = 100;
            this.ClientSize = new System.Drawing.Size(700, 730);
            this.Text = "Generic genetics: generate a circle";
        }
    }
}
