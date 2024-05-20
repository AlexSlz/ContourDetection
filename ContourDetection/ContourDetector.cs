using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV;
using System.Windows.Forms;
using static Emgu.CV.XImgproc.SupperpixelSLIC;

namespace ContourDetection
{
    internal class ContourDetector
    {
        private List<IAlgorithm> SelectedAlgorithms;

        public void Select(List<IAlgorithm> algorithms)
        {
            SelectedAlgorithms = new List<IAlgorithm>();
            SelectedAlgorithms.AddRange(algorithms);
        }

        public Contour ApplyAlgorithms(MyImage image)
        {
            var findContour = SelectedAlgorithms[0].Apply(image);
            for (int i = 1; i < SelectedAlgorithms.Count; i++)
            {
                findContour.Update(SelectedAlgorithms[i].Apply(findContour));
            }
            //image.Contours.Add(findContour);
            return findContour;
        }

        public void DrawContours(MyImage image, Contour contour)
        {
            using (Image<Bgr, byte> img = image.Bitmap.ToImage<Bgr, byte>())
            {
                using (Image<Gray, byte> edgeImage = contour.Bitmap.ToImage<Gray, byte>())
                {
                    using (VectorOfVectorOfPoint vector = new VectorOfVectorOfPoint())
                    {
                        CvInvoke.FindContours(edgeImage, vector, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);

                        CvInvoke.DrawContours(img, vector, -1, new MCvScalar(0, 0, 255), 1);

                        contour.Bitmap = img.ToBitmap();
                    }
                }
            }

        }
    }
}
