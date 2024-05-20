using Emgu.CV.Structure;
using Emgu.CV;

namespace ContourDetection.Algorithms
{
    internal class Sobel : IAlgorithm
    {
        public string Name => "Sobel";

        public Contour Apply(GraphicElement image)
        {
            using (Image<Bgr, byte> img = image.Bitmap.ToImage<Bgr, byte>())
            {
                using (Mat dst = new Mat())
                {
                    CvInvoke.Sobel(img, dst, Emgu.CV.CvEnum.DepthType.Cv64F, 1, 1, 3);
                    var contour = new Contour(this, dst.ToBitmap());
                    return contour;
                }
            }
        }
    }
}
