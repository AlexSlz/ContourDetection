using Emgu.CV;
using Emgu.CV.Structure;
using System.Threading.Tasks;


namespace ContourDetection.Algorithms
{
    internal class Yolo : IAlgorithm
    {
        public string Name => "Yolo";

        public override string ToString()
        {
            return Name;
        }

        public List<Contour> Apply(GraphicElement image)
        {
            var result = PyUtils.Run("py yolo.py", image, "yolo");

            var masks = PyUtils.GetMasksList("yolo");
            var contours = MyUtils.CreateContourList(this, result, masks, image.Bitmap);
            return contours;
        }

        public void Train()
        {
            var result = PyUtils.RunTrain($"py train_yolo.py", Name);
        }
    }
}
