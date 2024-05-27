namespace ContourDetection.Algorithms
{
    internal class Kirsch : IAlgorithm
    {
        public string Name => "Kirsch";
        public override string ToString()
        {
            return $"{Name}3x3 - Horizontal + Vertical";
        }

        public static double[,] Kirsch3x3Horizontal
        {
            get
            {
                return new double[,] {
                    {  5,  5,  5, },
                    { -3,  0, -3, },
                    { -3, -3, -3, },
                };
            }
        }

        public static double[,] Kirsch3x3Vertical
        {
            get
            {
                return new double[,] {
                    {  5, -3, -3, },
                    {  5,  0, -3, },
                    {  5, -3, -3, },
                };
            }
        }

        public Contour Apply(GraphicElement image)
        {
            Bitmap resultBitmap = image.Bitmap.ConvolutionFilter(Kirsch3x3Horizontal, Kirsch3x3Vertical, 1.0, 0, true);

            return new Contour(this, resultBitmap);
        }
    }
}
