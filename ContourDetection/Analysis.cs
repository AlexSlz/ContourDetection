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

            var metrics = new Metrics(detectedArray, groundTruthArray);
            double meanF1 = metrics.F1Score();
            double pixelAccuracy = metrics.PixelAccuracy();
            double iou = metrics.IoU();

            return (Math.Round(meanF1, 4), Math.Round(pixelAccuracy, 4), Math.Round(iou, 4));
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
                    Color pixelColor = bitmap.GetPixel(x, y);
                    binaryArray[y, x] = pixelColor.R > 128 || pixelColor.G > 128 || pixelColor.B > 128;
                }
            }

            return binaryArray;
        }
    }
}
