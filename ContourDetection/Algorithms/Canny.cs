using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourDetection.Algorithms
{
    internal class Canny : IAlgorithm
    {
        public string Name => "Canny";

        public Contour Apply(MyImage image)
        {
            var imgCanny = new Image<Bgr, byte>(image.FilePath).Canny(50, 20);
            var contour = new Contour(this, imgCanny.ToBitmap());
            image.Contours.Add(contour);
            return contour;
        }
    }
}
