namespace ContourDetection
{
    partial class Form2
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
            button1 = new Button();
            label1 = new Label();
            numericUpDown1 = new NumericUpDown();
            label2 = new Label();
            label3 = new Label();
            numericUpDown2 = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(150, 12);
            button1.Name = "button1";
            button1.Size = new Size(85, 41);
            button1.TabIndex = 0;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(135, 25);
            label1.TabIndex = 1;
            label1.Text = "Колір Контуру";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(184, 55);
            numericUpDown1.Margin = new Padding(5);
            numericUpDown1.Maximum = new decimal(new int[] { 9, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(51, 33);
            numericUpDown1.TabIndex = 2;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 57);
            label2.Name = "label2";
            label2.Size = new Size(164, 25);
            label2.TabIndex = 3;
            label2.Text = "Товщина контуру";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 93);
            label3.Name = "label3";
            label3.Size = new Size(127, 25);
            label3.TabIndex = 5;
            label3.Text = "Нижній поріг";
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(147, 91);
            numericUpDown2.Margin = new Padding(5);
            numericUpDown2.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(88, 33);
            numericUpDown2.TabIndex = 4;
            numericUpDown2.ValueChanged += numericUpDown2_ValueChanged;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(247, 131);
            Controls.Add(label3);
            Controls.Add(numericUpDown2);
            Controls.Add(label2);
            Controls.Add(numericUpDown1);
            Controls.Add(label1);
            Controls.Add(button1);
            Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form2";
            Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private NumericUpDown numericUpDown1;
        private Label label2;
        private Label label3;
        private NumericUpDown numericUpDown2;
    }
}