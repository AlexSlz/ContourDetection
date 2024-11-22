namespace ContourDetection
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
            DescriptionLabel = new Label();
            pictureBox1 = new PictureBox();
            tabPage2 = new TabPage();
            button5 = new Button();
            label9 = new Label();
            ImageSizeNumeric = new NumericUpDown();
            label8 = new Label();
            label7 = new Label();
            OptimizerComboBox = new ComboBox();
            ImageLimitNumeric = new NumericUpDown();
            label6 = new Label();
            checkBoxGraph = new CheckBox();
            TrainingButton = new Button();
            label5 = new Label();
            LearningRateNumeric = new NumericUpDown();
            BatchSizeNumeric = new NumericUpDown();
            label4 = new Label();
            EpochsNumeric = new NumericUpDown();
            label3 = new Label();
            datasetPathLabel = new Label();
            label2 = new Label();
            label1 = new Label();
            button2 = new Button();
            comboBox1 = new ComboBox();
            button1 = new Button();
            tabPage3 = new TabPage();
            dataGridView1 = new DataGridView();
            Head1 = new DataGridViewTextBoxColumn();
            Head2 = new DataGridViewTextBoxColumn();
            Head3 = new DataGridViewTextBoxColumn();
            Head4 = new DataGridViewTextBoxColumn();
            Head5 = new DataGridViewTextBoxColumn();
            button3 = new Button();
            AnalysisLabel = new Label();
            tabPage4 = new TabPage();
            button8 = new Button();
            label10 = new Label();
            button7 = new Button();
            button6 = new Button();
            dataGridView2 = new DataGridView();
            contextMenuStrip1 = new ContextMenuStrip(components);
            ЗберегтиToolStripMenuItem = new ToolStripMenuItem();
            ВидалитиToolStripMenuItem = new ToolStripMenuItem();
            вибратиСправжнєЗображенняToolStripMenuItem = new ToolStripMenuItem();
            label11 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ImageSizeNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ImageLimitNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LearningRateNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BatchSizeNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EpochsNumeric).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
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
            button4.Location = new Point(3, 484);
            button4.Name = "button4";
            button4.Size = new Size(30, 30);
            button4.TabIndex = 13;
            button4.Text = "+";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
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
            tabPage2.Controls.Add(button5);
            tabPage2.Controls.Add(label9);
            tabPage2.Controls.Add(ImageSizeNumeric);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(OptimizerComboBox);
            tabPage2.Controls.Add(ImageLimitNumeric);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(checkBoxGraph);
            tabPage2.Controls.Add(TrainingButton);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(LearningRateNumeric);
            tabPage2.Controls.Add(BatchSizeNumeric);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(EpochsNumeric);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(datasetPathLabel);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(button2);
            tabPage2.Controls.Add(comboBox1);
            tabPage2.Controls.Add(button1);
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(776, 523);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Алгоритми";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button5.Location = new Point(734, 16);
            button5.Name = "button5";
            button5.Size = new Size(34, 33);
            button5.TabIndex = 22;
            button5.Text = "X";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(25, 53);
            label9.Name = "label9";
            label9.Size = new Size(23, 25);
            label9.TabIndex = 21;
            label9.Text = "./";
            // 
            // ImageSizeNumeric
            // 
            ImageSizeNumeric.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ImageSizeNumeric.Location = new Point(270, 240);
            ImageSizeNumeric.Maximum = new decimal(new int[] { 700, 0, 0, 0 });
            ImageSizeNumeric.Minimum = new decimal(new int[] { 128, 0, 0, 0 });
            ImageSizeNumeric.Name = "ImageSizeNumeric";
            ImageSizeNumeric.Size = new Size(165, 33);
            ImageSizeNumeric.TabIndex = 20;
            ImageSizeNumeric.Value = new decimal(new int[] { 224, 0, 0, 0 });
            ImageSizeNumeric.ValueChanged += ImageSizeNumeric_ValueChanged;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label8.AutoSize = true;
            label8.Location = new Point(81, 242);
            label8.Name = "label8";
            label8.Size = new Size(183, 25);
            label8.TabIndex = 19;
            label8.Text = "Розмір зображення";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Location = new Point(142, 399);
            label7.Name = "label7";
            label7.Size = new Size(122, 25);
            label7.TabIndex = 18;
            label7.Text = "Оптимізатор";
            // 
            // OptimizerComboBox
            // 
            OptimizerComboBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            OptimizerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            OptimizerComboBox.FormattingEnabled = true;
            OptimizerComboBox.ImeMode = ImeMode.NoControl;
            OptimizerComboBox.Items.AddRange(new object[] { "Auto", "AdamW", "SGD" });
            OptimizerComboBox.Location = new Point(270, 396);
            OptimizerComboBox.Name = "OptimizerComboBox";
            OptimizerComboBox.Size = new Size(165, 33);
            OptimizerComboBox.TabIndex = 17;
            OptimizerComboBox.SelectedIndexChanged += OptimizerComboBox_SelectedIndexChanged;
            // 
            // ImageLimitNumeric
            // 
            ImageLimitNumeric.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ImageLimitNumeric.Location = new Point(270, 201);
            ImageLimitNumeric.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            ImageLimitNumeric.Name = "ImageLimitNumeric";
            ImageLimitNumeric.Size = new Size(165, 33);
            ImageLimitNumeric.TabIndex = 15;
            ImageLimitNumeric.ValueChanged += ImageLimitNumeric_ValueChanged;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Location = new Point(18, 203);
            label6.Name = "label6";
            label6.Size = new Size(246, 25);
            label6.TabIndex = 14;
            label6.Text = "Ліміт зображень у датасеті";
            // 
            // checkBoxGraph
            // 
            checkBoxGraph.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            checkBoxGraph.AutoSize = true;
            checkBoxGraph.Location = new Point(148, 435);
            checkBoxGraph.Name = "checkBoxGraph";
            checkBoxGraph.Size = new Size(287, 29);
            checkBoxGraph.TabIndex = 13;
            checkBoxGraph.Text = "Зберегти результат навчання";
            checkBoxGraph.UseVisualStyleBackColor = true;
            checkBoxGraph.CheckedChanged += checkBoxGraph_CheckedChanged;
            // 
            // TrainingButton
            // 
            TrainingButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TrainingButton.Location = new Point(8, 470);
            TrainingButton.Name = "TrainingButton";
            TrainingButton.Size = new Size(760, 45);
            TrainingButton.TabIndex = 12;
            TrainingButton.Text = "Почати навчання";
            TrainingButton.UseVisualStyleBackColor = true;
            TrainingButton.Click += TrainingButton_Click;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new Point(119, 359);
            label5.Name = "label5";
            label5.Size = new Size(145, 25);
            label5.TabIndex = 11;
            label5.Text = "Темп навчання";
            // 
            // LearningRateNumeric
            // 
            LearningRateNumeric.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            LearningRateNumeric.DecimalPlaces = 4;
            LearningRateNumeric.Increment = new decimal(new int[] { 1, 0, 0, 393216 });
            LearningRateNumeric.Location = new Point(270, 357);
            LearningRateNumeric.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            LearningRateNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 393216 });
            LearningRateNumeric.Name = "LearningRateNumeric";
            LearningRateNumeric.Size = new Size(165, 33);
            LearningRateNumeric.TabIndex = 10;
            LearningRateNumeric.ThousandsSeparator = true;
            LearningRateNumeric.Value = new decimal(new int[] { 119, 0, 0, 393216 });
            LearningRateNumeric.ValueChanged += LearningRateNumeric_ValueChanged;
            // 
            // BatchSizeNumeric
            // 
            BatchSizeNumeric.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            BatchSizeNumeric.Location = new Point(270, 318);
            BatchSizeNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            BatchSizeNumeric.Name = "BatchSizeNumeric";
            BatchSizeNumeric.Size = new Size(165, 33);
            BatchSizeNumeric.TabIndex = 9;
            BatchSizeNumeric.Value = new decimal(new int[] { 2, 0, 0, 0 });
            BatchSizeNumeric.ValueChanged += BatchSizeNumeric_ValueChanged;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(155, 320);
            label4.Name = "label4";
            label4.Size = new Size(109, 25);
            label4.TabIndex = 8;
            label4.Text = "BATCH SIZE";
            // 
            // EpochsNumeric
            // 
            EpochsNumeric.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            EpochsNumeric.Location = new Point(270, 279);
            EpochsNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            EpochsNumeric.Name = "EpochsNumeric";
            EpochsNumeric.Size = new Size(165, 33);
            EpochsNumeric.TabIndex = 7;
            EpochsNumeric.Value = new decimal(new int[] { 5, 0, 0, 0 });
            EpochsNumeric.ValueChanged += EpochsNumeric_ValueChanged;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(129, 281);
            label3.Name = "label3";
            label3.Size = new Size(135, 25);
            label3.TabIndex = 6;
            label3.Text = "Кількість епох";
            // 
            // datasetPathLabel
            // 
            datasetPathLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            datasetPathLabel.AutoSize = true;
            datasetPathLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Underline, GraphicsUnit.Point, 204);
            datasetPathLabel.ForeColor = SystemColors.MenuHighlight;
            datasetPathLabel.Location = new Point(270, 169);
            datasetPathLabel.Name = "datasetPathLabel";
            datasetPathLabel.Size = new Size(133, 25);
            datasetPathLabel.TabIndex = 5;
            datasetPathLabel.Text = "../dataset/VOC";
            datasetPathLabel.Click += datasetPathLabel_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(184, 169);
            label2.Name = "label2";
            label2.Size = new Size(80, 25);
            label2.TabIndex = 4;
            label2.Text = "Датасет";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 19);
            label1.Name = "label1";
            label1.Size = new Size(80, 25);
            label1.TabIndex = 3;
            label1.Text = "Модель";
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.Location = new Point(441, 16);
            button2.Name = "button2";
            button2.Size = new Size(287, 33);
            button2.TabIndex = 2;
            button2.Text = "Завантажити ваги моделі";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.ImeMode = ImeMode.NoControl;
            comboBox1.Location = new Point(94, 16);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(341, 33);
            comboBox1.TabIndex = 1;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button1.Location = new Point(8, 81);
            button1.Name = "button1";
            button1.Size = new Size(760, 45);
            button1.TabIndex = 0;
            button1.Text = "Застосувати";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // tabPage3
            // 
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
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Head1, Head2, Head3, Head4, Head5 });
            dataGridView1.Location = new Point(8, 102);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.Size = new Size(760, 413);
            dataGridView1.TabIndex = 4;
            // 
            // Head1
            // 
            Head1.HeaderText = "Name";
            Head1.Name = "Head1";
            Head1.Width = 87;
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
            // Head5
            // 
            Head5.HeaderText = "TotalScore";
            Head5.Name = "Head5";
            Head5.Width = 124;
            // 
            // button3
            // 
            button3.Location = new Point(8, 63);
            button3.Name = "button3";
            button3.Size = new Size(314, 33);
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
            // tabPage4
            // 
            tabPage4.Controls.Add(label11);
            tabPage4.Controls.Add(button8);
            tabPage4.Controls.Add(label10);
            tabPage4.Controls.Add(button7);
            tabPage4.Controls.Add(button6);
            tabPage4.Controls.Add(dataGridView2);
            tabPage4.Location = new Point(4, 34);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(776, 523);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Результати навчання";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            button8.Location = new Point(302, 58);
            button8.Name = "button8";
            button8.Size = new Size(107, 44);
            button8.TabIndex = 10;
            button8.Text = "Dice";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(8, 68);
            label10.Name = "label10";
            label10.Size = new Size(175, 25);
            label10.TabIndex = 9;
            label10.Text = "Побудувати графік";
            // 
            // button7
            // 
            button7.Location = new Point(189, 58);
            button7.Name = "button7";
            button7.Size = new Size(107, 44);
            button7.TabIndex = 8;
            button7.Text = "Loss";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button6
            // 
            button6.Location = new Point(8, 3);
            button6.Name = "button6";
            button6.Size = new Size(208, 49);
            button6.TabIndex = 6;
            button6.Text = "Завантажити";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Dock = DockStyle.Bottom;
            dataGridView2.Location = new Point(0, 110);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowTemplate.Height = 25;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView2.Size = new Size(776, 413);
            dataGridView2.TabIndex = 5;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { ЗберегтиToolStripMenuItem, ВидалитиToolStripMenuItem, вибратиСправжнєЗображенняToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(235, 70);
            // 
            // ЗберегтиToolStripMenuItem
            // 
            ЗберегтиToolStripMenuItem.Name = "ЗберегтиToolStripMenuItem";
            ЗберегтиToolStripMenuItem.Size = new Size(234, 22);
            ЗберегтиToolStripMenuItem.Text = "Зберегти";
            ЗберегтиToolStripMenuItem.Click += ЗберегтиToolStripMenuItem_Click;
            // 
            // ВидалитиToolStripMenuItem
            // 
            ВидалитиToolStripMenuItem.Name = "ВидалитиToolStripMenuItem";
            ВидалитиToolStripMenuItem.Size = new Size(234, 22);
            ВидалитиToolStripMenuItem.Text = "Видалити";
            ВидалитиToolStripMenuItem.Click += ВидалитиToolStripMenuItem_Click;
            // 
            // вибратиСправжнєЗображенняToolStripMenuItem
            // 
            вибратиСправжнєЗображенняToolStripMenuItem.Name = "вибратиСправжнєЗображенняToolStripMenuItem";
            вибратиСправжнєЗображенняToolStripMenuItem.Size = new Size(234, 22);
            вибратиСправжнєЗображенняToolStripMenuItem.Text = "Вибрати Істинне зображення";
            вибратиСправжнєЗображенняToolStripMenuItem.Click += вибратиСправжнєЗображенняToolStripMenuItem_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(222, 15);
            label11.Name = "label11";
            label11.Size = new Size(23, 25);
            label11.TabIndex = 11;
            label11.Text = "./";
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
            ((System.ComponentModel.ISupportInitialize)ImageSizeNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)ImageLimitNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)LearningRateNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)BatchSizeNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)EpochsNumeric).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
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
        private Label DescriptionLabel;
        private TabPage tabPage3;
        private Label AnalysisLabel;
        private Button button3;
        private Button button4;
        private DataGridView dataGridView1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem ВидалитиToolStripMenuItem;
        private ToolStripMenuItem ЗберегтиToolStripMenuItem;
        private ToolStripMenuItem вибратиСправжнєЗображенняToolStripMenuItem;
        private ComboBox comboBox1;
        private Button button2;
        private Label label1;
        private Label label2;
        private Label datasetPathLabel;
        private Label label3;
        private NumericUpDown EpochsNumeric;
        private NumericUpDown LearningRateNumeric;
        private NumericUpDown BatchSizeNumeric;
        private Label label4;
        private Label label5;
        private Button TrainingButton;
        private CheckBox checkBoxGraph;
        private NumericUpDown ImageLimitNumeric;
        private Label label6;
        private Label label7;
        private ComboBox OptimizerComboBox;
        private NumericUpDown ImageSizeNumeric;
        private Label label8;
        private Label label9;
        private Button button5;
        private TabPage tabPage4;
        private DataGridView dataGridView2;
        private Button button6;
        private Button button7;
        private DataGridViewTextBoxColumn Head1;
        private DataGridViewTextBoxColumn Head2;
        private DataGridViewTextBoxColumn Head3;
        private DataGridViewTextBoxColumn Head4;
        private DataGridViewTextBoxColumn Head5;
        private Label label10;
        private Button button8;
        private Label label11;
    }
}
