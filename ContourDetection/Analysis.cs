using Emgu.CV.CvEnum;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Linemod;

namespace ContourDetection
{
    internal class Analysis
    {
        public (double F1Score, double pixelAccuracy, double IoU) CalculateMetrics(Bitmap detectedContours, Bitmap groundTruthContours)
        {
            bool[,] detectedArray = BitmapToBinaryArray(detectedContours);
            bool[,] groundTruthArray = BitmapToBinaryArray(groundTruthContours);

            double tp = CountTruePositive(detectedArray, groundTruthArray);
            double tn = CountTrueNegative(detectedArray, groundTruthArray);
            double fp = CountFalsePositive(detectedArray, groundTruthArray);
            double fn = CountFalseNegative(detectedArray, groundTruthArray);

            var metrics = new Metrics();
            double meanF1 = metrics.F1Score(tp, fp, fn);
            double pixelAccuracy = metrics.PixelAccuracy(tp, fp, tn, fn);
            double iou = metrics.IoU(tp, fp, fn);

            return (Math.Round(meanF1, 3), Math.Round(pixelAccuracy, 3), Math.Round(iou, 3));
        }

        private static bool[,] BitmapToBinaryArray(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            bool[,] binaryArray = new bool[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    binaryArray[y, x] = color.R < 10 && color.G < 10 && color.B < 10;
                }
            }

            return binaryArray;
        }

        private int CountTruePositive(bool[,] detected, bool[,] groundTruth)
        {
            return detected.Cast<bool>().Zip(groundTruth.Cast<bool>(), (d, g) => d && g).Count(b => b);
        }

        private int CountFalsePositive(bool[,] detected, bool[,] groundTruth)
        {
            return detected.Cast<bool>().Zip(groundTruth.Cast<bool>(), (d, g) => d && !g).Count(b => b);
        }

        private int CountFalseNegative(bool[,] detected, bool[,] groundTruth)
        {
            return detected.Cast<bool>().Zip(groundTruth.Cast<bool>(), (d, g) => !d && g).Count(b => b);
        }

        private int CountTrueNegative(bool[,] detected, bool[,] groundTruth)
        {
            return detected.Cast<bool>().Zip(groundTruth.Cast<bool>(), (d, g) => !d && !g).Count(b => b);
        }
    }
}
