namespace WinFormGraphics
{
    partial class EvolutionForm<T>
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public System.Windows.Forms.Label lblGenerationCount;
        private System.Windows.Forms.Button cmdRun;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support
        /// </summary>
        private void InitializeComponent()
        {
            // command Run
            this.cmdRun = new System.Windows.Forms.Button();
            this.cmdRun.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRun.Location = new System.Drawing.Point(20, 20);
            this.cmdRun.Name = "cmdRun";
            this.cmdRun.Size = new System.Drawing.Size(46, 22);
            this.cmdRun.Text = "Run!";
            //Raises the click event of the button
            this.cmdRun.Click += cmdRun_Click;
            this.Controls.Add(this.cmdRun);

            // label GenerationCount
            this.lblGenerationCount = new System.Windows.Forms.Label();
            this.lblGenerationCount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenerationCount.Location = new System.Drawing.Point(80, 23);
            this.lblGenerationCount.Name = "lblGenerationCount";
            this.lblGenerationCount.Size = new System.Drawing.Size(500, 23);
            this.lblGenerationCount.TabIndex = 0;
            this.lblGenerationCount.Text = "Generation..";
            this.lblGenerationCount.Visible = false;
            this.Controls.Add(this.lblGenerationCount);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "MainForm";
            this.Text = "Generic genetics";
            this.ResumeLayout(false);
        }
    }
}
