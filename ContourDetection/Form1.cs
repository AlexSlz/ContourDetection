using ContourDetection.Algorithms;

namespace ContourDetection
{
    public partial class Form1 : Form
    {
        ContourDetector contourDetector = new ContourDetector();

        List<MyImage> Images = new List<MyImage>();
        MyImage SelectedImage = null;

        List<string> ListAlgorithms = new List<string>();

        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;

            checkBox1.CheckedChanged += (object sender, EventArgs e) => checkBox_CheckedChanged(checkBox1, "Canny");
            checkBox2.CheckedChanged += (object sender, EventArgs e) => checkBox_CheckedChanged(checkBox2, "Sobel");
        }

        private void LoadNewImage()
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    MyImage temp = new MyImage(fileDialog.FileName);
                    temp.Show(pictureBox1);

                    Images.Add(temp);
                    SelectedImage = Images.Last();
                    treeView1.Nodes.Add(temp.Id, temp.FileName);
                }
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
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
                new Sobel()
            };
            temp.RemoveAll(item => !ListAlgorithms.Contains(item.Name));

            temp = temp.OrderBy(item => ListAlgorithms.IndexOf(item.Name)).ToList();

            contourDetector.Select(temp);

            var contour = contourDetector.ApplyAlgorithms(SelectedImage);

            SelectedImage.Contours.Add(contour);

            if(checkBox3.Checked) contourDetector.DrawContours(SelectedImage, contour);

            SelectedImage.DisplayOnTreeView(treeView1);

            GC.Collect();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            MyImage image = Images.Find(image => image.Id == e.Node.Name);
            if (image == null)
            {
                image = Images.Find(image => image.Id == e.Node.Parent.Name);
                var find = image.Contours.Find(contour => contour.Id == e.Node.Name);
                find.Show(pictureBox1);
                label3.Text = find.GetDescription();
            }
            else
            {
                image.Show(pictureBox1);
                label3.Text = "";
            }
            SelectedImage = image;
        }

        private void checkBox_CheckedChanged(CheckBox checkBox,string Name)
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
    }
}
