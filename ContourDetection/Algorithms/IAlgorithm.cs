namespace ContourDetection
{
    internal interface IAlgorithm
    {
        string Name { get; }
        string ToString();
        Contour Apply(GraphicElement image);
    }
}
