using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourDetection.Metrics
{
    internal class IoU : Metrics
    {
        public IoU(bool[,] detectedArray, bool[,] groundTruthArray) : base(detectedArray, groundTruthArray)
        {
        }

        public override double Analysis()
        {
            int intersection = 0;
            int union = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool predIsForeground = detectedArray[y, x];
                    bool gtIsForeground = groundTruthArray[y, x];

                    if (predIsForeground && gtIsForeground)
                        intersection++;

                    if (predIsForeground || gtIsForeground)
                        union++;
                }
            }

            return union == 0 ? 0.0 : (double)intersection / union;
        }
    }
}
