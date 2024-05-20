namespace ContourDetection.Algorithms
{
    internal class Sobel : IAlgorithm
    {
        public string Name => "Sobel";

        public static double[,] Sobel3x3Horizontal
        {
            get {
                return new double[,] {
                    { -1,  0,  1, },
                    { -2,  0,  2, },
                    { -1,  0,  1, }, 
                };
            }
        }

        public static double[,] Sobel3x3Vertical
        {
            get { 
                return new double[,] { 
                    {  1,  2,  1, },
                    {  0,  0,  0, },
                    { -1, -2, -1, }, 
                };
            }
        }

        public Contour Apply(GraphicElement image)
        {
            Bitmap resultBitmap =
                   Convolution.ConvolutionFilter(image.Bitmap,
                                  Sobel3x3Horizontal,
                                    Sobel3x3Vertical, 1.0, 0, true);

            return new Contour(this, resultBitmap);
            /*            using (Image<Bgr, byte> img = image.Bitmap.ToImage<Bgr, byte>())
                        {
                            using (Mat dst = new Mat())
                            {
                                CvInvoke.Sobel(img, dst, Emgu.CV.CvEnum.DepthType.Cv64F, 1, 1, 3);
                                var contour = new Contour(this, dst.ToBitmap());
                                return contour;
                            }
                        }*/
        }
    }
}
