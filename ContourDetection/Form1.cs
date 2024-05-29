using ContourDetection.Algorithms;
using ContourDetection.Settings;
using System.Windows.Forms;

namespace ContourDetection
{
    public partial class Form1 : Form
    {
        ContourDetector _contourDetector = new ContourDetector();
        ContourDisplay _contourDisplay = new ContourDisplay();

        Analysis _analysis = new Analysis();

        MyCustomPictureBox pictureBox;

        List<MyImage> Images = new List<MyImage>();
        MyImage SelectedImage = null;

        MyImage GroundTruthContourImage = null;

        Contour SelectedContour = null;

        List<Contour> SelectedContourList = new List<Contour>();

        List<string> ListAlgorithms = new List<string>();

        public Form1()
        {
            InitializeComponent();

            pictureBox = new MyCustomPictureBox(pictureBox1);

            treeView1.ContextMenuStrip = contextMenuStrip1;

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            checkBox1.CheckedChanged += (object sender, EventArgs e) => checkBox_CheckedChanged(checkBox1, "Canny");
            checkBox2.CheckedChanged += (object sender, EventArgs e) => checkBox_CheckedChanged(checkBox2, "Sobel");
            checkBox4.CheckedChanged += (object sender, EventArgs e) => checkBox_CheckedChanged(checkBox4, "Laplacian");

            KirschcheckBox.CheckedChanged += (object sender, EventArgs e) => checkBox_CheckedChanged(KirschcheckBox, "Kirsch");
            PrewittcheckBox.CheckedChanged += (object sender, EventArgs e) => checkBox_CheckedChanged(PrewittcheckBox, "Prewitt");
            DexiNedcheckBox.CheckedChanged += (object sender, EventArgs e) => checkBox_CheckedChanged(DexiNedcheckBox, "DexiNed");
            HedcheckBox.CheckedChanged += (object sender, EventArgs e) => checkBox_CheckedChanged(HedcheckBox, "Hed");
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (SelectedImage == null || ListAlgorithms.Count == 0)
            {
                MessageBox.Show("Треба вибрати зображення та алгоритм.");
                return;
            }

            var temp = new List<IAlgorithm>()
            {
                new Canny((double)numericUpDown1.Value, (double)numericUpDown2.Value),
                new Sobel(),
                new Laplacian(comboBox1.SelectedIndex, comboBox2.SelectedIndex, (int)numericUpDown3.Value),
                new Kirsch(),
                new Prewitt(),
                new DexiNed(),
                new Hed()
            };
            temp.RemoveAll(item => !ListAlgorithms.Contains(item.Name));

            temp = temp.OrderBy(item => ListAlgorithms.IndexOf(item.Name)).ToList();

            _contourDetector.Select(temp);

            var contour = _contourDetector.ApplyAlgorithms(SelectedImage);

            SelectedImage.Contours.Add(contour);

            SelectedImage.DisplayOnTreeView(treeView1);

            GC.Collect();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ShowContourCheckBox.Checked = false;
            MyImage image = Images.Find(image => image.Id == e.Node.Name);
            if (image == null)
            {
                image = Images.Find(image => image.Id == e.Node.Parent.Name);
                var find = image.Contours.Find(contour => contour.Id == e.Node.Name);
                find.Show(pictureBox);
                DescriptionLabel.Text = find.GetDescription();
                SelectedContour = find;
            }
            else
            {
                image.Show(pictureBox);
                SelectedContour = null;
            }

            SelectedImage = image;
        }

        private void checkBox_CheckedChanged(CheckBox checkBox, string Name)
        {
            if (checkBox.Checked)
            {
                ListAlgorithms.Add(Name);
                listBox1.Items.Add(Name);
            }
            else
            {
                ListAlgorithms.Remove(Name);
                listBox1.Items.Remove(Name);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDown3.Enabled = comboBox2.SelectedIndex > 0;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                string res = "Потрібно вибрати зображення.";

                if (SelectedImage != null)
                    res = $"Вибране зображення: {SelectedImage.FileName}";
                if (GroundTruthContourImage != null)
                    res += $" | {GroundTruthContourImage.FileName}";

                AnalysisLabel.Text = res;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (SelectedImage == null) return;
            GroundTruthContourImage = OpenDialogImage();
            if (GroundTruthContourImage == null) return;

            AnalysisLabel.Text += $" | {GroundTruthContourImage.FileName}";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (GroundTruthContourImage == null) return;
            dataGridView1.Rows.Clear();
            foreach (var contour in SelectedImage.Contours)
            {
                var (precision, recall, f1Score) = _analysis.EvaluateContours(GroundTruthContourImage.Bitmap, contour.Bitmap);
                var IoUScore = _analysis.CalculateIoU(GroundTruthContourImage.Bitmap, contour.Bitmap);
                dataGridView1.Rows.Add(contour.GetName(), precision, recall, f1Score, IoUScore);
            }
        }

        private void ShowContourCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var state = ShowContourCheckBox.Checked;
            if (SelectedContour == null) return;

            if (state)
            {
                SelectedContour.ContourOnImage = _contourDisplay.DrawContours(SelectedImage, SelectedContour);
                GraphicElement.Show(pictureBox, SelectedContour.ContourOnImage);
            }
            if (!state)
            {
                SelectedContour.Show(pictureBox);
            }
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
                form2Instance = new Form2(_contourDisplay);
                form2Instance.FormClosed += Form2Instance_FormClosed;
                form2Instance.Show();
            }
            else
            {
                form2Instance.BringToFront();
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
                    image = Images.Find(image => image.Id == treeView1.SelectedNode.Parent.Name);
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
                            image = Images.Find(image => image.Id == treeView1.SelectedNode.Parent.Name);
                            var find = image.Contours.Find(contour => contour.Id == treeView1.SelectedNode.Name);

                            if (find.ContourOnImage != null)
                                find.ContourOnImage.Save(saveFileDialog.FileName);
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
    }
}
