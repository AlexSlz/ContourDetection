using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourDetection.Metrics
{
    internal class F1Score : Metrics
    {
        public F1Score(bool[,] detectedArray, bool[,] groundTruthArray) : base(detectedArray, groundTruthArray)
        {
        }

        public override double Analysis()
        {
            int truePositives = 0;
            int falsePositives = 0;
            int falseNegatives = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool predIsForeground = detectedArray[y, x];
                    bool gtIsForeground = groundTruthArray[y, x];

                    if (predIsForeground && gtIsForeground)
                        truePositives++;
                    else if (predIsForeground && !gtIsForeground)
                        falsePositives++;
                    else if (!predIsForeground && gtIsForeground)
                        falseNegatives++;
                }
            }

            double precision = truePositives / (double)(truePositives + falsePositives);
            double recall = truePositives / (double)(truePositives + falseNegatives);

            return 2 * precision * recall / (precision + recall);
        }
    }
}
