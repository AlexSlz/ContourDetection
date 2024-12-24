using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourDetection.Metrics
{
    internal class PixelAccuracy : Metrics
    {
        public PixelAccuracy(bool[,] detectedArray, bool[,] groundTruthArray) : base(detectedArray, groundTruthArray)
        {
        }

        public override double Analysis()
        {
            int correctPixels = 0;
            int totalPixels = height * width;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool predIsForeground = detectedArray[y, x];
                    bool gtIsForeground = groundTruthArray[y, x];

                    if (predIsForeground == gtIsForeground)
                        correctPixels++;
                }
            }

            return (double)correctPixels / totalPixels;
        }
    }
}
