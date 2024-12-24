using ContourDetection.Algorithms;
using ContourDetection.Metrics;
using ContourDetection.Settings;

namespace ContourDetection
{
    internal partial class Form1 : Form
    {
        ContourDetector _contourDetector = new ContourDetector();
        ContourDisplay _contourDisplay = new ContourDisplay();

        Analysis _analysis = new Analysis();

        MyCustomPictureBox pictureBox;

        List<MyImage> Images = new List<MyImage>();
        MyImage SelectedImage = null;

        MyImage GroundTruthContourImage = null;

        Contour SelectedContour = null;

        List<IAlgorithm> AlgorithmList = new List<IAlgorithm>()
            {
                new DeepLabv3(),
                new FCN(),
                new MaskRCNN(),
                new Yolo()
            };

        public Form1()
        {
            InitializeComponent();
            pictureBox = new MyCustomPictureBox(pictureBox1);


            foreach (var algorithm in AlgorithmList)
            {
                comboBox1.Items.Add(algorithm.Name);
            }

            comboBox1.SelectedIndex = 0;
            OptimizerComboBox.SelectedIndex = 0;
            checkBoxGraph.Checked = TrainHelper.SaveTxt;

            treeView1.ContextMenuStrip = contextMenuStrip1;
            label9.Text = TrainHelper.SelectedWeight;
            datasetPathLabel.Text = TrainHelper.DataSetPath;
        }
        string filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";
        private MyImage OpenDialogImage()
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = filter;
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    return new MyImage(fileDialog.FileName);
                }
                return null;
            }
        }

        private void LoadDefaultImage()
        {
            var temp = OpenDialogImage();
            if (temp == null) return;

            temp.Show(pictureBox);

            Images.Add(temp);
            SelectedImage = Images.Last();
            treeView1.Nodes.Add(temp.Id, temp.FileName);
            pictureBox1.Enabled = true;
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            LoadDefaultImage();
        }

        private void splitContainer1_Panel2_DoubleClick(object sender, EventArgs e)
        {
            LoadDefaultImage();
        }
        //Контури
        private void button1_Click(object sender, EventArgs e)
        {
            IAlgorithm algorithm = AlgorithmList.Find(item => item.Name == comboBox1.Text);

            if (algorithm == null)
            {
                MessageBox.Show("Такого алгаритму не знайдено.");
                return;
            }

            if (SelectedImage == null)
            {
                MessageBox.Show("Спочатку потрібно завантажити зображення.");
                return;
            }
            var FindContours = SelectedImage.Contours.FindAll(item => item.Algorithm.Name == algorithm.Name);
            if (FindContours != null)
            {
                foreach (var findContour in FindContours)
                {
                    SelectedImage.Contours.Remove(findContour);
                }
            }

            _contourDetector.Select(algorithm);

            var contours = _contourDetector.ApplyAlgorithm(SelectedImage);
            if (contours != null && contours?.Count != 0)
            {
                SelectedImage.Contours.AddRange(contours);
                SelectedImage.DisplayOnTreeView(treeView1);
                tabControl1.SelectTab(0);
            }
            else
            {
                MessageBox.Show("Нічого не знайдено!");
            }
            GC.Collect();
        }

        public MyImage FindImageInTreeView(TreeNode selectedNode)
        {
            var node = selectedNode.Parent;
            while (node.Level != 0)
            {
                node = node.Parent;
            }

            return Images.Find(image => image.Id == node.Name);
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            treeView1.SelectedNode = e.Node;
            MyImage image = Images.Find(image => image.Id == e.Node.Name);
            if (image == null)
            {
                image = FindImageInTreeView(e.Node);
                var find = image.Contours.Find(contour => contour.Id == e.Node.Name);
                var SelectedMainContour = find;
                if (e.Node.Level != 1)
                    SelectedMainContour = image.Contours.Find(contour => contour.Id == e.Node.Parent.Name);
                find.Show(pictureBox);
                DescriptionLabel.Text = SelectedMainContour.GetDescription();
                SelectedContour = find;
            }
            else
            {
                image.Show(pictureBox);
                DescriptionLabel.Text = "";
                SelectedContour = null;
            }
            if(SelectedImage != null)
                SelectedImage.ShowOnImage = null;
            SelectedImage = image;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                AnalysisLabel.Text = "Потрібно вибрати зображення та Істинне зображення для перевірки.";

                if (SelectedImage != null)
                    AnalysisLabel.Text = $"Вибране зображення: {SelectedImage.FileName}";
                if (GroundTruthContourImage != null)
                    AnalysisLabel.Text += $"\nІстинне зображення: {GroundTruthContourImage.FileName}";
            }
        }

        //Аналіз
        private void button3_Click(object sender, EventArgs e)
        {
            if (GroundTruthContourImage == null) return;
            dataGridView1.Rows.Clear();
            var groupedItems = SelectedImage.Contours.GroupBy(item => item.Algorithm.Name).Where(group => group.Count() > 1).Select(group => group.First()).ToList();
            foreach (var contour in groupedItems)
            {
                var (F1Score, pixelAccuracy, IoU) = _analysis.CalculateMetrics(contour.Bitmap, GroundTruthContourImage.Bitmap);

                var total = (F1Score + pixelAccuracy + IoU) / 3;

                dataGridView1.Rows.Add(contour.GetName(), F1Score, pixelAccuracy, IoU, Math.Round(total, 4), contour.TimeToFind.Seconds);
            }
        }

        private void DrawContoursOrMasks(Func<MyImage, List<Contour>, Bitmap> drawFunction)
        {
            if (SelectedImage == null || SelectedContour == null) return;

            List<Contour> contours = SelectedImage.Contours.FindAll(contour => contour.Algorithm.Name == SelectedContour.Algorithm.Name);
            if (contours.Count == 0) return;
            
            SelectedImage.ShowOnImage = drawFunction(SelectedImage, contours);

            GraphicElement.Show(pictureBox, SelectedImage.ShowOnImage);

            GC.Collect();
        }

        private void DrawContours(object sender, EventArgs e)
        {
            DrawContoursOrMasks(_contourDisplay.DrawContours);
        }

        private void DrawMasks(object sender, EventArgs e)
        {
            DrawContoursOrMasks(_contourDisplay.DrawMasks);
        }

        private Form2 form2Instance;
        private void Form2Instance_FormClosed(object sender, FormClosedEventArgs e)
        {
            form2Instance = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (form2Instance == null || form2Instance.IsDisposed)
            {
                form2Instance = new Form2(_contourDisplay, DrawContours, DrawMasks);
                form2Instance.FormClosed += Form2Instance_FormClosed;
                form2Instance.Show();
            }
            else
            {
                form2Instance.BringToFront();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "PyTorch Train Files(*.pt; *.pth)|*.pt; *.pth";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    TrainHelper.SelectedWeight = fileDialog.FileName;
                    label9.Text = "Вибрані ваги: " + TrainHelper.GetLastFolders(fileDialog.FileName);
                }
            }
        }

        private void ВидалитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (MessageBox.Show($"Бажаєте видалити {treeView1.SelectedNode.Text}?", "???", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                MyImage image = Images.Find(image => image.Id == treeView1.SelectedNode.Name);
                if (image == null)
                {
                    image = FindImageInTreeView(treeView1.SelectedNode);
                    var find = image.Contours.Find(contour => contour.Id == treeView1.SelectedNode.Name);
                    image.Contours.Remove(find);
                }
                else
                {
                    Images.Remove(image);
                }
                treeView1.Nodes.Remove(treeView1.SelectedNode);
                SelectedImage = null;
                SelectedContour = null;
                pictureBox.PictureBox.Image = null;
            }
        }

        private void ЗберегтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = filter;
                if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
                {
                    try
                    {
                        MyImage image = Images.Find(image => image.Id == treeView1.SelectedNode.Name);

                        if (image == null)
                        {
                            image = FindImageInTreeView(treeView1.SelectedNode);
                            var find = image.Contours.Find(contour => contour.Id == treeView1.SelectedNode.Name);

                            if (image.ShowOnImage != null)
                                image.ShowOnImage.Save(saveFileDialog.FileName);
                            else
                                find.Bitmap.Save(saveFileDialog.FileName);
                        }
                        else
                        {
                            image.Bitmap.Save(saveFileDialog.FileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void вибратиСправжнєЗображенняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedImage == null) return;
            GroundTruthContourImage = OpenDialogImage();
            if (GroundTruthContourImage == null) return;
            GroundTruthContourImage.Bitmap = MyUtils.ToBlackWhite(GroundTruthContourImage.Bitmap);
            GroundTruthContourImage.Show(pictureBox);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            ExportResult.ExportToExcel(dataGridView1);
        }

        private void datasetPathLabel_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    TrainHelper.DataSetPath = fbd.SelectedPath;


                    datasetPathLabel.Text = TrainHelper.GetLastFolders();
                }
            }
        }

        private void ImageLimitNumeric_ValueChanged(object sender, EventArgs e)
        {
            TrainHelper.ImageLimit = (int)ImageLimitNumeric.Value;
        }

        private void EpochsNumeric_ValueChanged(object sender, EventArgs e)
        {
            TrainHelper.Epochs = (int)EpochsNumeric.Value;
        }

        private void BatchSizeNumeric_ValueChanged(object sender, EventArgs e)
        {
            TrainHelper.BatchSize = (int)BatchSizeNumeric.Value;
        }

        private void LearningRateNumeric_ValueChanged(object sender, EventArgs e)
        {
            TrainHelper.LearningRate = Double.Parse(LearningRateNumeric.Value.ToString());
            string value = LearningRateNumeric.Value.ToString();

            int decimalPlaces = 0;
            if (value.Contains(','))
            {
                decimalPlaces = value.Split(',')[1].Length;
            }
            LearningRateNumeric.DecimalPlaces = decimalPlaces;
        }

        private void OptimizerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrainHelper.Optimizer = OptimizerComboBox.Text;
        }

        private void checkBoxGraph_CheckedChanged(object sender, EventArgs e)
        {
            TrainHelper.SaveTxt = checkBoxGraph.Checked;
        }

        private void ImageSizeNumeric_ValueChanged(object sender, EventArgs e)
        {
            TrainHelper.ImageSize = (int)ImageSizeNumeric.Value;
        }

        private void TrainingButton_Click(object sender, EventArgs e)
        {
            IAlgorithm algorithm = AlgorithmList.Find(item => item.Name == comboBox1.Text);

            algorithm.Train();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TrainHelper.SelectedWeight = "";
            label9.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "PyTorch Train Files(*.txt;)|*.txt";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    TrainHelper.LoadMetricsToDataGridView(fileDialog.FileName, dataGridView2);
                    label11.Text = TrainHelper.GetLastFolders(fileDialog.FileName);
                }
            }
        }
        private LossForm lossFormInstance;
        private void lossFormInstance_FormClosed(object sender, FormClosedEventArgs e)
        {
            lossFormInstance = null;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string[] a = { "Train_loss", "Val_loss" };
            OpenGraph(a);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string[] a = { "Train_Dice", "Val_Dice" };
            OpenGraph(a);
        }

        private void OpenGraph(string[] metricName)
        {
            if (lossFormInstance == null || lossFormInstance.IsDisposed)
            {
                lossFormInstance = new LossForm(dataGridView2, metricName);
                lossFormInstance.FormClosed += lossFormInstance_FormClosed;
                lossFormInstance.Show();
            }
            else
            {
                lossFormInstance.BringToFront();
            }
        }
    }
}
