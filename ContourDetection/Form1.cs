using ContourDetection.Algorithms;
using ContourDetection.Settings;

namespace ContourDetection
{
    public partial class Form1 : Form
    {
        ContourDetector _contourDetector = new ContourDetector();
        ContourDisplay _contourDisplay = new ContourDisplay();
        MyCustomPictureBox pictureBox;

        List<MyImage> Images = new List<MyImage>();
        MyImage SelectedImage = null;

        List<string> ListAlgorithms = new List<string>();

        public Form1()
        {
            InitializeComponent();

            pictureBox = new MyCustomPictureBox(pictureBox1);

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            checkBox1.CheckedChanged += (object sender, EventArgs e) => checkBox_CheckedChanged(checkBox1, "Canny");
            checkBox2.CheckedChanged += (object sender, EventArgs e) => checkBox_CheckedChanged(checkBox2, "Sobel");
            checkBox4.CheckedChanged += (object sender, EventArgs e) => checkBox_CheckedChanged(checkBox4, "Laplacian");
        }

        private void LoadNewImage()
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    MyImage temp = new MyImage(fileDialog.FileName);
                    temp.Show(pictureBox);

                    Images.Add(temp);
                    SelectedImage = Images.Last();
                    treeView1.Nodes.Add(temp.Id, temp.FileName);
                    pictureBox1.Enabled = true;
                }
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            LoadNewImage();
        }

        private void splitContainer1_Panel2_DoubleClick(object sender, EventArgs e)
        {
            LoadNewImage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SelectedImage == null || ListAlgorithms.Count == 0)
            {
                return;
            }

            var temp = new List<IAlgorithm>()
            {
                new Canny((double)numericUpDown1.Value, (double)numericUpDown2.Value),
                new Sobel(),
                new Laplacian(comboBox1.SelectedIndex, comboBox2.SelectedIndex, (int)numericUpDown3.Value)
            };
            temp.RemoveAll(item => !ListAlgorithms.Contains(item.Name));

            temp = temp.OrderBy(item => ListAlgorithms.IndexOf(item.Name)).ToList();

            _contourDetector.Select(temp);

            var contour = _contourDetector.ApplyAlgorithms(SelectedImage);

            SelectedImage.Contours.Add(contour);

            if (checkBox3.Checked) _contourDisplay.DrawContours(SelectedImage, contour);

            SelectedImage.DisplayOnTreeView(treeView1);

            GC.Collect();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            MyImage image = Images.Find(image => image.Id == e.Node.Name);
            if (image == null)
            {
                image = Images.Find(image => image.Id == e.Node.Parent.Name);
                var find = image.Contours.Find(contour => contour.Id == e.Node.Name);
                find.Show(pictureBox);
                DescriptionLabel.Text = find.GetDescription();
            }
            else
            {
                image.Show(pictureBox);
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
        private Form2 form2Instance;
        private void button2_Click(object sender, EventArgs e)
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
        private void Form2Instance_FormClosed(object sender, FormClosedEventArgs e)
        {
            form2Instance = null;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDown3.Enabled = comboBox2.SelectedIndex > 0;
        }
    }
}
