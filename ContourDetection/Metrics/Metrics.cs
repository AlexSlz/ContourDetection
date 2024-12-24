namespace ContourDetection.Metrics
{
    internal abstract class Metrics
    {
        protected readonly bool[,] detectedArray;
        protected readonly bool[,] groundTruthArray;
        protected readonly int height;
        protected readonly int width;
        public Metrics(bool[,] detectedArray, bool[,] groundTruthArray)
        {
            this.detectedArray = detectedArray;
            this.groundTruthArray = groundTruthArray;
            height = detectedArray.GetLength(0);
            width = detectedArray.GetLength(1);
        }

        public abstract double Analysis();

        public double IoU()
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
        public double F1Score()
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
        public double PixelAccuracy()
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
