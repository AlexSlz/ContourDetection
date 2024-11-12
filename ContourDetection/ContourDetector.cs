using System.Diagnostics;

namespace ContourDetection
{
    internal class ContourDetector
    {
        private IAlgorithm SelectedAlgorithm;
        Stopwatch _stopwatch = new Stopwatch();

        public void Select(IAlgorithm algorithm)
        {
            SelectedAlgorithm = algorithm;
        }

        public List<Contour> ApplyAlgorithm(MyImage image)
        {
            _stopwatch.Restart();
            _stopwatch.Start();
            var findContour = SelectedAlgorithm.Apply(image);
            _stopwatch.Stop();
            findContour[0].TimeToFind = _stopwatch.Elapsed;
            return findContour;
        }
    }
}
