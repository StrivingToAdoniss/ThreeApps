using System.Windows.Forms;

namespace ThreeApps
{
    partial class Lab6
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lab6));
            numericUpDown1 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            numericUpDown3 = new NumericUpDown();
            n = new Label();
            Min = new Label();
            Max = new Label();
            StartButton = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            SuspendLayout();
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(129, 22);
            numericUpDown1.Minimum = 1;
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(58, 23);
            numericUpDown1.TabIndex = 0;
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(129, 73);
            numericUpDown2.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numericUpDown2.Minimum = new decimal(new int[] { 9999, 0, 0, int.MinValue });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(58, 23);
            numericUpDown2.TabIndex = 1;
            numericUpDown2.DecimalPlaces = 2;
            numericUpDown2.ValueChanged += numericUpDown2_ValueChanged;
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(129, 130);
            numericUpDown3.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numericUpDown3.Minimum = new decimal(new int[] { 9999, 0, 0, int.MinValue });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(58, 23);
            numericUpDown3.TabIndex = 2;
            numericUpDown3.DecimalPlaces = 2;
            numericUpDown3.ValueChanged += numericUpDown3_ValueChanged;
            // 
            // n
            // 
            n.AutoSize = true;
            n.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            n.Location = new Point(12, 22);
            n.Name = "n";
            n.Size = new Size(29, 25);
            n.TabIndex = 3;
            n.Text = "n:";
            // 
            // Min
            // 
            Min.AutoSize = true;
            Min.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            Min.Location = new Point(12, 73);
            Min.Name = "Min";
            Min.Size = new Size(52, 25);
            Min.TabIndex = 4;
            Min.Text = "Min:";
            // 
            // Max
            // 
            Max.AutoSize = true;
            Max.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            Max.Location = new Point(12, 130);
            Max.Name = "Max";
            Max.Size = new Size(56, 25);
            Max.TabIndex = 5;
            Max.Text = "Max:";
            // 
            // StartButton
            // 
            StartButton.Text = "Виконати";
            StartButton.Name = "StartButton";
            StartButton.Location = new System.Drawing.Point(60, 175);
            StartButton.Click += StartButton_Click;
            // 
            // Lab6
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(214, 211);
            Controls.Add(Max);
            Controls.Add(Min);
            Controls.Add(n);
            Controls.Add(numericUpDown3);
            Controls.Add(numericUpDown2);
            Controls.Add(numericUpDown1);
            Controls.Add(StartButton);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Lab6";
            Text = "Lab6";
            Load += Lab6_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown3;
        private Label n;
        private Label Min;
        private Label Max;
        private Button StartButton;
    }
}