namespace ContourDetection.Algorithms
{
    internal class Sobel : IAlgorithm
    {
        public string Name => "Sobel";

        public override string ToString()
        {
            return $"{Name}3x3 - Horizontal + Vertical";
        }

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
        }
    }
}
