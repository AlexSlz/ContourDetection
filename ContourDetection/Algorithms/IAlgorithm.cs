namespace ContourDetection
{
    internal interface IAlgorithm
    {
        string Name { get; }
        string ToString();
        List<Contour> Apply(GraphicElement image);
        void Train();
    }
}
