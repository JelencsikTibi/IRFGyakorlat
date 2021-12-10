
namespace Evolucios
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1stgeneration = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1stgeneration
            // 
            this.label1stgeneration.AutoSize = true;
            this.label1stgeneration.Location = new System.Drawing.Point(13, 13);
            this.label1stgeneration.Name = "label1stgeneration";
            this.label1stgeneration.Size = new System.Drawing.Size(66, 13);
            this.label1stgeneration.TabIndex = 0;
            this.label1stgeneration.Text = "1. generáció";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1stgeneration);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1stgeneration;
    }
}

