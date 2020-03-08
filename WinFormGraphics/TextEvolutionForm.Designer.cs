namespace WinFormGraphics
{
    partial class TextEvolutionForm
    {
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private System.Windows.Forms.Label lblTargetText;
        private System.Windows.Forms.TextBox txtTargetText;
        private System.Windows.Forms.Label lblResult;
        private void InitializeComponent()
        {
            this.lblTargetText = new System.Windows.Forms.Label();
            this.lblTargetText.Location = new System.Drawing.Point(20, 60);
            this.lblTargetText.Size = new System.Drawing.Size(70, 16);
            this.lblTargetText.Text = "Target text: ";
            this.Controls.Add(this.lblTargetText);

            this.txtTargetText = new System.Windows.Forms.TextBox();
            this.txtTargetText.Location = new System.Drawing.Point(90, 55);
            this.txtTargetText.Size = new System.Drawing.Size(300, 16);
            this.Controls.Add(this.txtTargetText);

            this.lblResult = new System.Windows.Forms.Label();
            this.lblResult.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(20, 110);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(100, 16);
            this.lblResult.TabIndex = 1;
            this.Controls.Add(this.lblResult);

            this.ClientSize = new System.Drawing.Size(430, 230);
            this.Text = "Generic genetics: generate a text";
        }
    }
}