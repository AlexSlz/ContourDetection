﻿namespace ContourDetection
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            splitContainer1 = new SplitContainer();
            treeView1 = new TreeView();
            button4 = new Button();
            ShowContourCheckBox = new CheckBox();
            DescriptionLabel = new Label();
            pictureBox1 = new PictureBox();
            tabPage2 = new TabPage();
            HedcheckBox = new CheckBox();
            DexiNedcheckBox = new CheckBox();
            KirschcheckBox = new CheckBox();
            PrewittcheckBox = new CheckBox();
            label3 = new Label();
            numericUpDown3 = new NumericUpDown();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            label4 = new Label();
            checkBox4 = new CheckBox();
            listBox1 = new ListBox();
            checkBox2 = new CheckBox();
            numericUpDown2 = new NumericUpDown();
            numericUpDown1 = new NumericUpDown();
            label2 = new Label();
            label1 = new Label();
            checkBox1 = new CheckBox();
            button1 = new Button();
            tabPage3 = new TabPage();
            dataGridView1 = new DataGridView();
            Head1 = new DataGridViewTextBoxColumn();
            Head2 = new DataGridViewTextBoxColumn();
            Head3 = new DataGridViewTextBoxColumn();
            Head4 = new DataGridViewTextBoxColumn();
            button3 = new Button();
            AnalysisLabel = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            ЗберегтиToolStripMenuItem = new ToolStripMenuItem();
            ВидалитиToolStripMenuItem = new ToolStripMenuItem();
            вибратиСправжнєЗображенняToolStripMenuItem = new ToolStripMenuItem();
            SaveButton = new Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(784, 561);
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(splitContainer1);
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(776, 523);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Головна";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(treeView1);
            splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = Color.DarkGray;
            splitContainer1.Panel2.Controls.Add(button4);
            splitContainer1.Panel2.Controls.Add(ShowContourCheckBox);
            splitContainer1.Panel2.Controls.Add(DescriptionLabel);
            splitContainer1.Panel2.Controls.Add(pictureBox1);
            splitContainer1.Panel2.DoubleClick += splitContainer1_Panel2_DoubleClick;
            splitContainer1.Panel2MinSize = 550;
            splitContainer1.Size = new Size(770, 517);
            splitContainer1.SplitterDistance = 200;
            splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            treeView1.Dock = DockStyle.Fill;
            treeView1.Location = new Point(0, 0);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(200, 517);
            treeView1.TabIndex = 0;
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button4.Location = new Point(355, 481);
            button4.Name = "button4";
            button4.Size = new Size(30, 30);
            button4.TabIndex = 13;
            button4.Text = "+";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // ShowContourCheckBox
            // 
            ShowContourCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ShowContourCheckBox.AutoSize = true;
            ShowContourCheckBox.Location = new Point(3, 483);
            ShowContourCheckBox.Name = "ShowContourCheckBox";
            ShowContourCheckBox.Size = new Size(346, 29);
            ShowContourCheckBox.TabIndex = 12;
            ShowContourCheckBox.Text = "Відобразити контури на зображенні";
            ShowContourCheckBox.UseVisualStyleBackColor = true;
            ShowContourCheckBox.CheckedChanged += ShowContourCheckBox_CheckedChanged;
            // 
            // DescriptionLabel
            // 
            DescriptionLabel.AutoSize = true;
            DescriptionLabel.BackColor = Color.WhiteSmoke;
            DescriptionLabel.Location = new Point(0, 0);
            DescriptionLabel.Name = "DescriptionLabel";
            DescriptionLabel.Size = new Size(0, 25);
            DescriptionLabel.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.DarkGray;
            pictureBox1.Enabled = false;
            pictureBox1.Location = new Point(45, 33);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(181, 133);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.DoubleClick += pictureBox1_DoubleClick;
            // 
            // tabPage2
            // 
            tabPage2.AutoScroll = true;
            tabPage2.Controls.Add(HedcheckBox);
            tabPage2.Controls.Add(DexiNedcheckBox);
            tabPage2.Controls.Add(KirschcheckBox);
            tabPage2.Controls.Add(PrewittcheckBox);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(numericUpDown3);
            tabPage2.Controls.Add(comboBox2);
            tabPage2.Controls.Add(comboBox1);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(checkBox4);
            tabPage2.Controls.Add(listBox1);
            tabPage2.Controls.Add(checkBox2);
            tabPage2.Controls.Add(numericUpDown2);
            tabPage2.Controls.Add(numericUpDown1);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(checkBox1);
            tabPage2.Controls.Add(button1);
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(776, 523);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Алгоритми";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // HedcheckBox
            // 
            HedcheckBox.AutoSize = true;
            HedcheckBox.Location = new Point(8, 360);
            HedcheckBox.Name = "HedcheckBox";
            HedcheckBox.Size = new Size(65, 29);
            HedcheckBox.TabIndex = 23;
            HedcheckBox.Text = "Hed";
            HedcheckBox.UseVisualStyleBackColor = true;
            // 
            // DexiNedcheckBox
            // 
            DexiNedcheckBox.AutoSize = true;
            DexiNedcheckBox.Location = new Point(8, 325);
            DexiNedcheckBox.Name = "DexiNedcheckBox";
            DexiNedcheckBox.Size = new Size(103, 29);
            DexiNedcheckBox.TabIndex = 21;
            DexiNedcheckBox.Text = "DexiNed";
            DexiNedcheckBox.UseVisualStyleBackColor = true;
            // 
            // KirschcheckBox
            // 
            KirschcheckBox.AutoSize = true;
            KirschcheckBox.Location = new Point(8, 290);
            KirschcheckBox.Name = "KirschcheckBox";
            KirschcheckBox.Size = new Size(82, 29);
            KirschcheckBox.TabIndex = 20;
            KirschcheckBox.Text = "Kirsch";
            KirschcheckBox.UseVisualStyleBackColor = true;
            // 
            // PrewittcheckBox
            // 
            PrewittcheckBox.AutoSize = true;
            PrewittcheckBox.Location = new Point(8, 255);
            PrewittcheckBox.Name = "PrewittcheckBox";
            PrewittcheckBox.Size = new Size(90, 29);
            PrewittcheckBox.TabIndex = 19;
            PrewittcheckBox.Text = "Prewitt";
            PrewittcheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 218);
            label3.Name = "label3";
            label3.Size = new Size(75, 25);
            label3.TabIndex = 18;
            label3.Text = "Фактор";
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(89, 216);
            numericUpDown3.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(172, 33);
            numericUpDown3.TabIndex = 17;
            numericUpDown3.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "None", "3x3", "5x5-1", "5x5-2" });
            comboBox2.Location = new Point(156, 177);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(105, 33);
            comboBox2.TabIndex = 16;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.ImeMode = ImeMode.NoControl;
            comboBox1.Items.AddRange(new object[] { "3x3", "5x5" });
            comboBox1.Location = new Point(125, 138);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(136, 33);
            comboBox1.TabIndex = 15;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8, 180);
            label4.Name = "label4";
            label4.Size = new Size(142, 25);
            label4.TabIndex = 14;
            label4.Text = "Матриця Гауса";
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(8, 140);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(111, 29);
            checkBox4.TabIndex = 12;
            checkBox4.Text = "Laplacian";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            listBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 25;
            listBox1.Location = new Point(422, 6);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(346, 129);
            listBox1.TabIndex = 9;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(8, 105);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(78, 29);
            checkBox2.TabIndex = 8;
            checkBox2.Text = "Sobel";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(141, 75);
            numericUpDown2.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(120, 33);
            numericUpDown2.TabIndex = 7;
            numericUpDown2.Value = new decimal(new int[] { 200, 0, 0, 0 });
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(141, 36);
            numericUpDown1.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(120, 33);
            numericUpDown1.TabIndex = 6;
            numericUpDown1.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 77);
            label2.Name = "label2";
            label2.Size = new Size(130, 25);
            label2.TabIndex = 5;
            label2.Text = "Верхній поріг";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 38);
            label1.Name = "label1";
            label1.Size = new Size(127, 25);
            label1.TabIndex = 3;
            label1.Text = "Нижній поріг";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(8, 6);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(84, 29);
            checkBox1.TabIndex = 1;
            checkBox1.Text = "Canny";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Location = new Point(422, 138);
            button1.Name = "button1";
            button1.Size = new Size(346, 57);
            button1.TabIndex = 0;
            button1.Text = "Застосувати";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(SaveButton);
            tabPage3.Controls.Add(dataGridView1);
            tabPage3.Controls.Add(button3);
            tabPage3.Controls.Add(AnalysisLabel);
            tabPage3.Location = new Point(4, 34);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(776, 523);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Аналіз";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Head1, Head2, Head3, Head4 });
            dataGridView1.Location = new Point(8, 102);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.Size = new Size(760, 413);
            dataGridView1.TabIndex = 4;
            // 
            // Head1
            // 
            Head1.HeaderText = "AlgorithmName";
            Head1.Name = "Head1";
            Head1.Width = 171;
            // 
            // Head2
            // 
            Head2.HeaderText = "F1Score";
            Head2.Name = "Head2";
            Head2.Width = 103;
            // 
            // Head3
            // 
            Head3.HeaderText = "PixelAccuracy";
            Head3.Name = "Head3";
            Head3.Width = 153;
            // 
            // Head4
            // 
            Head4.HeaderText = "IoU";
            Head4.Name = "Head4";
            Head4.Width = 66;
            // 
            // button3
            // 
            button3.Location = new Point(8, 39);
            button3.Name = "button3";
            button3.Size = new Size(314, 57);
            button3.TabIndex = 3;
            button3.Text = "Аналізувати";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // AnalysisLabel
            // 
            AnalysisLabel.AutoSize = true;
            AnalysisLabel.Location = new Point(8, 11);
            AnalysisLabel.Name = "AnalysisLabel";
            AnalysisLabel.Size = new Size(63, 25);
            AnalysisLabel.TabIndex = 0;
            AnalysisLabel.Text = "label5";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { ЗберегтиToolStripMenuItem, ВидалитиToolStripMenuItem, вибратиСправжнєЗображенняToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(252, 70);
            // 
            // ЗберегтиToolStripMenuItem
            // 
            ЗберегтиToolStripMenuItem.Name = "ЗберегтиToolStripMenuItem";
            ЗберегтиToolStripMenuItem.Size = new Size(251, 22);
            ЗберегтиToolStripMenuItem.Text = "Зберегти";
            ЗберегтиToolStripMenuItem.Click += ЗберегтиToolStripMenuItem_Click;
            // 
            // ВидалитиToolStripMenuItem
            // 
            ВидалитиToolStripMenuItem.Name = "ВидалитиToolStripMenuItem";
            ВидалитиToolStripMenuItem.Size = new Size(251, 22);
            ВидалитиToolStripMenuItem.Text = "Видалити";
            ВидалитиToolStripMenuItem.Click += ВидалитиToolStripMenuItem_Click;
            // 
            // вибратиСправжнєЗображенняToolStripMenuItem
            // 
            вибратиСправжнєЗображенняToolStripMenuItem.Name = "вибратиСправжнєЗображенняToolStripMenuItem";
            вибратиСправжнєЗображенняToolStripMenuItem.Size = new Size(251, 22);
            вибратиСправжнєЗображенняToolStripMenuItem.Text = "Вибрати Справжнє Зображення";
            вибратиСправжнєЗображенняToolStripMenuItem.Click += вибратиСправжнєЗображенняToolStripMenuItem_Click;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(328, 39);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(314, 57);
            SaveButton.TabIndex = 5;
            SaveButton.Text = "Зберегти у Excel";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(tabControl1);
            MinimumSize = new Size(800, 600);
            Name = "Form1";
            Text = "AlexContourDetection";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button button1;
        private SplitContainer splitContainer1;
        private TreeView treeView1;
        private PictureBox pictureBox1;
        private Label label2;
        private Label label1;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private ListBox listBox1;
        private Label DescriptionLabel;
        private CheckBox checkBox4;
        private Label label4;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private Label label3;
        private NumericUpDown numericUpDown3;
        private TabPage tabPage3;
        private Label AnalysisLabel;
        private Button button3;
        private CheckBox PrewittcheckBox;
        private CheckBox KirschcheckBox;
        private CheckBox DexiNedcheckBox;
        private CheckBox HedcheckBox;
        private CheckBox ShowContourCheckBox;
        private Button button4;
        private DataGridView dataGridView1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem ВидалитиToolStripMenuItem;
        private ToolStripMenuItem ЗберегтиToolStripMenuItem;
        private ToolStripMenuItem вибратиСправжнєЗображенняToolStripMenuItem;
        private DataGridViewTextBoxColumn Head1;
        private DataGridViewTextBoxColumn Head2;
        private DataGridViewTextBoxColumn Head3;
        private DataGridViewTextBoxColumn Head4;
        private Button SaveButton;
    }
}
