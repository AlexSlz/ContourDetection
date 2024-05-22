namespace ContourDetection
{
    internal class ContourDetector
    {
        private List<IAlgorithm> SelectedAlgorithms;

        public void Select(List<IAlgorithm> algorithms)
        {
            SelectedAlgorithms = new List<IAlgorithm>();
            SelectedAlgorithms.AddRange(algorithms);
        }

        public Contour ApplyAlgorithms(MyImage image)
        {
            var findContour = SelectedAlgorithms[0].Apply(image);
            for (int i = 1; i < SelectedAlgorithms.Count; i++)
            {
                findContour.Update(SelectedAlgorithms[i].Apply(findContour));
            }
            //image.Contours.Add(findContour);
            return findContour;
        }
    }
}
