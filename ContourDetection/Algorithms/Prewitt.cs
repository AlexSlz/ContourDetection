namespace ContourDetection.Algorithms
{
    internal class Prewitt : IAlgorithm
    {
        public string Name => "Prewitt";
        public override string ToString()
        {
            return $"{Name}3x3 - Horizontal + Vertical";
        }

        public static double[,] Prewitt3x3Horizontal
        {
            get
            {
                return new double[,] {
                    { -1,  0,  1, },
                    { -1,  0,  1, },
                    { -1,  0,  1, },
                };
            }
        }

        public static double[,] Prewitt3x3Vertical
        {
            get
            {
                return new double[,] {
                    {  1,  1,  1, },
                    {  0,  0,  0, },
                    { -1, -1, -1, },
                };
            }
        }

        public Contour Apply(GraphicElement image)
        {
            Bitmap resultBitmap = Convolution.ConvolutionFilter(image.Bitmap, Prewitt3x3Horizontal, Prewitt3x3Vertical, 1.0, 0, true);
            return new Contour(this, resultBitmap);
        }
    }
}
