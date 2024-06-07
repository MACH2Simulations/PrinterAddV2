namespace PrinterAddV2
{

    partial class MainWindow
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
            this.MainProgressIndicator = new System.Windows.Forms.ProgressBar();
            this.StatusText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // MainProgressIndicator
            // 
            this.MainProgressIndicator.Location = new System.Drawing.Point(13, 12);
            this.MainProgressIndicator.Name = "MainProgressIndicator";
            this.MainProgressIndicator.Size = new System.Drawing.Size(459, 23);
            this.MainProgressIndicator.TabIndex = 0;
            this.MainProgressIndicator.Click += new System.EventHandler(this.MainProgressIndicator_Click);
            // 
            // StatusText
            // 
            this.StatusText.Enabled = false;
            this.StatusText.Location = new System.Drawing.Point(12, 49);
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(457, 20);
            this.StatusText.TabIndex = 1;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 81);
            this.ControlBox = false;
            this.Controls.Add(this.StatusText);
            this.Controls.Add(this.MainProgressIndicator);
            this.MaximumSize = new System.Drawing.Size(500, 120);
            this.MinimumSize = new System.Drawing.Size(500, 120);
            this.Name = "MainWindow";
            this.Text = "Printer Adder V2";
            this.Shown += new System.EventHandler(this.MainWindow_Shown_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar MainProgressIndicator;
        private System.Windows.Forms.TextBox StatusText;
    }
}

