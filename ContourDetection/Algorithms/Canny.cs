using Emgu.CV;
using Emgu.CV.Structure;
using System.Windows.Forms;

namespace ContourDetection.Algorithms
{
    internal class Canny : IAlgorithm
    {
        public string Name => "Canny";

        private double _th;
        private double _thL;

        public override string ToString()
        {
            return $"{Name}\nНижній поріг - {_th}\nВерхній поріг - {_thL}";
        }

        public Canny(double th, double thL)
        {
            SetParameters(th, thL);
        }

        public void SetParameters(double th, double thL)
        {
            _th = th;
            _thL = thL;
        }

        public Contour Apply(GraphicElement image)
        {
            using (Image<Bgr, byte> img = image.Bitmap.ToImage<Bgr, byte>())
            {
                using (Mat dst = new Mat())
                {
                    CvInvoke.Canny(img, dst, _th, _thL);
                    var contour = new Contour(this, dst.ToBitmap());
                    return contour;
                }
            }
        }
    }
}
