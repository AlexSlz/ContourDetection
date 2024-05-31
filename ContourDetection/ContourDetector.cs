using System.Diagnostics;

namespace ContourDetection
{
    internal class ContourDetector
    {
        private List<IAlgorithm> SelectedAlgorithms;
        Stopwatch _stopwatch = new Stopwatch();

        public void Select(List<IAlgorithm> algorithms)
        {
            SelectedAlgorithms = new List<IAlgorithm>();
            SelectedAlgorithms.AddRange(algorithms);
        }

        public Contour ApplyAlgorithms(MyImage image)
        {
            _stopwatch.Restart();
            _stopwatch.Start();
            var findContour = SelectedAlgorithms[0].Apply(image);
            for (int i = 1; i < SelectedAlgorithms.Count; i++)
            {
                findContour.Update(SelectedAlgorithms[i].Apply(findContour));
            }
            _stopwatch.Stop();
            findContour.TimeToFind = _stopwatch.Elapsed;
            return findContour;
        }

        public List<Contour> ApplyAlgorithmsList(MyImage image)
        {
            List<Contour> contours = new List<Contour>();
            foreach (var algorithm in SelectedAlgorithms)
            {
                _stopwatch.Restart();
                _stopwatch.Start();
                var contour = algorithm.Apply(image);
                _stopwatch.Stop();
                contour.TimeToFind = _stopwatch.Elapsed;
                contours.Add(contour);
            }
            return contours;
        }
    }
}
