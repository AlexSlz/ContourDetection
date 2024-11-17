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
            var cmd = "py deeplabv3.py";

            if (TrainHelper.SelectedWeight != "")
            {
                cmd += $" --modelPath={TrainHelper.SelectedWeight}";
            }

            var result = PyUtils.Run(cmd, image, "deeplabv3");

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
