using Emgu.CV;
using Emgu.CV.Structure;
using System.Threading.Tasks;


namespace ContourDetection.Algorithms
{
    internal class FCN : IAlgorithm
    {
        public string Name => "FCN";

        public override string ToString()
        {
            return Name;
        }

        public List<Contour> Apply(GraphicElement image)
        {
            var result = PyUtils.Run("py fcn.py", image, "fcn");

            var masks = PyUtils.GetMasksList("fcn");
            var contours = MyUtils.CreateContourList(this, result, masks, image.Bitmap);
            
            return contours;
        }

        public void Train()
        {
            var result = PyUtils.RunTrain($"py train.py", Name);
        }
    }
}
