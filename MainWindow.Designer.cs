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
        /// 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            MainProgressIndicator = new System.Windows.Forms.ProgressBar();
            StatusText = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // MainProgressIndicator
            // 
            MainProgressIndicator.Location = new System.Drawing.Point(15, 14);
            MainProgressIndicator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MainProgressIndicator.Name = "MainProgressIndicator";
            MainProgressIndicator.Size = new System.Drawing.Size(536, 27);
            MainProgressIndicator.TabIndex = 0;
            MainProgressIndicator.Click += MainProgressIndicator_Click;
            // 
            // StatusText
            // 
            StatusText.Enabled = false;
            StatusText.Location = new System.Drawing.Point(14, 57);
            StatusText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            StatusText.Name = "StatusText";
            StatusText.Size = new System.Drawing.Size(538, 23);
            StatusText.TabIndex = 1;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(565, 93);
            ControlBox = false;
            Controls.Add(StatusText);
            Controls.Add(MainProgressIndicator);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximumSize = new System.Drawing.Size(581, 132);
            MinimumSize = new System.Drawing.Size(581, 132);
            Name = "MainWindow";
            Text = "Printer Adder V2";
            Shown += MainWindow_Shown_1;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar MainProgressIndicator;
        private System.Windows.Forms.TextBox StatusText;
    }
}

