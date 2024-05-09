using ContourDetection.Algorithms;
using Emgu.CV.CvEnum;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using System.Drawing.Imaging;
using System.Drawing;

namespace ContourDetection
{
    public partial class Form1 : Form
    {
        List<MyImage> Images = new List<MyImage>();
        MyImage SelectedImage = null;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
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
            if (SelectedImage == null)
                return;
            Canny canny = new Canny();
            var findContour = canny.Apply(SelectedImage, (double)numericUpDown1.Value, (double)numericUpDown2.Value);
            treeView1.Nodes.Find(SelectedImage.Id, false)[0].Nodes.Add(findContour.Id, "Canny");

            Image<Bgr, byte> originalImage = SelectedImage.Bitmap.ToImage<Bgr, byte>();
            Image<Gray, byte> edgeImage = findContour.Bitmap.ToImage<Gray, byte>();

            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(edgeImage, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);

                CvInvoke.DrawContours(originalImage, contours, -1, new MCvScalar(0, 0, 255), 1);
            }

            pictureBox1.Image = originalImage.ToBitmap();

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            MyImage image = Images.Find(image => image.Id == e.Node.Name);
            if(image == null)
            {
                image = Images.Find(image => image.Id == e.Node.Parent.Name);
                image.Contours.Find(contour => contour.Id == e.Node.Name).Show(pictureBox1);
            }
            else
            {
                image.Show(pictureBox1);
            }
            SelectedImage = image;
        }
    }
}
