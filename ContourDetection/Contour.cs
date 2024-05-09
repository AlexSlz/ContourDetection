namespace ContourDetection
{
    internal class Contour : GraphicElement
    {
        public IAlgorithm Algorithm;
        public Contour(IAlgorithm algorithm, Bitmap bitmap)
        {
            Id = Guid.NewGuid().ToString();
            this.Algorithm = algorithm;
            Bitmap = bitmap;
        }
    }
}
