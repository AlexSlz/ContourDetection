using Emgu.CV;
using Emgu.CV.Structure;
using System.Threading.Tasks;


namespace ContourDetection.Algorithms
{
    internal class MaskRCNN : IAlgorithm
    {
        public string Name => "MaskRCNN";

        public override string ToString()
        {
            return Name;
        }

        public List<Contour> Apply(GraphicElement image)
        {
            var result = PyUtils.Run("py maskrcnn.py", image, "maskrcnn");

            var masks = PyUtils.GetMasksList("maskrcnn");
            var contours = MyUtils.CreateContourList(this, result, masks, image.Bitmap);

            return contours;
        }

        public void Train()
        {
            throw new NotImplementedException();
        }
    }
}
