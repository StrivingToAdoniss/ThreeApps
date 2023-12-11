namespace Object2
{
    partial class Object2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Object2));
            listBox1 = new ListBox();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 32;
            listBox1.Location = new Point(22, 26);
            listBox1.Margin = new Padding(6, 6, 6, 6);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(342, 260);
            listBox1.TabIndex = 0;
            // 
            // Object2
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(431, 337);
            Controls.Add(listBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(6, 6, 6, 6);
            MaximizeBox = false;
            Name = "Object2";
            Text = "Object2";
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox1;
    }
}