namespace ContourDetection
{
    internal class Contour : GraphicElement
    {
        public List<IAlgorithm> Algorithms;
        public Contour(IAlgorithm algorithm, Bitmap bitmap)
        {
            Algorithms = new List<IAlgorithm>() { algorithm };
            Id = Guid.NewGuid().ToString();
            Bitmap = bitmap;
        }

        public void Update(Contour contour)
        {
            Algorithms.AddRange(contour.Algorithms);
            Bitmap = contour.Bitmap;
        }

        public string GetName()
        {
            return String.Join("+", Algorithms.Select(item => item.Name));
        }

        public string GetDescription()
        {
            return String.Join("\n", Algorithms.Select(item => item.ToString()));
        }

    }
}
