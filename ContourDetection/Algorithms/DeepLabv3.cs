using Emgu.CV;
using Emgu.CV.Structure;
using System.Threading.Tasks;


namespace ContourDetection.Algorithms
{
    internal class DeepLabv3 : IAlgorithm
    {
        public string Name => "DeepLabv3";

        public override string ToString()
        {
            return Name;
        }

        public List<Contour> Apply(GraphicElement image)
        {
            var result = PyUtils.Run("py deeplabv3.py", image, "deeplabv3");

            if(result == "not found")
            {
                return null;
            }

            var masks = PyUtils.GetMasksList("deeplabv3");
            var contours = MyUtils.CreateContourList(this, result, masks, image.Bitmap);
            
            return contours;
        }

        public void Train()
        {
            var result = PyUtils.RunTrain($"py train.py", Name);
        }
    }
}
