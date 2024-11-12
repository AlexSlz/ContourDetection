using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourDetection
{
    internal static class MyUtils
    {
        public static List<Contour> CreateContourList(IAlgorithm algorithm, string result, List<string> masks, Bitmap originalImage)
        {
            List<Contour> contours = new List<Contour>() { new Contour(algorithm, result, originalImage.Width, originalImage.Height) };
            foreach (var mask in masks)
            {
                contours.Add(new Contour(algorithm, mask, originalImage.Width, originalImage.Height));
            }
            return contours;
        }
    }
}
