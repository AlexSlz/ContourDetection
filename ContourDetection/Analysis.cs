namespace ContourDetection
{
    internal class Analysis
    {
        public (double Precision, double Recall, double F1Score) EvaluateContours(Bitmap detectedContours, Bitmap groundTruthContours)
        {
            int tp = 0; // True Positive
            int fp = 0; // False Positive
            int fn = 0; // False Negative

            for (int y = 0; y < detectedContours.Height; y++)
            {
                for (int x = 0; x < detectedContours.Width; x++)
                {
                    bool detected = Check(detectedContours, x, y, 150); 
                    bool groundTruth = Check(groundTruthContours, x, y, 150);

                    if (detected && groundTruth)
                    {
                        tp++;
                    }
                    else if (detected && !groundTruth)
                    {
                        fp++;
                    }
                    else if (!detected && groundTruth)
                    {
                        fn++;
                    }
                }
            }

            double precision = tp / (double)(tp + fp);
            double recall = tp / (double)(tp + fn);
            double f1Score = 2 * (precision * recall) / (precision + recall);

            return (Math.Round(precision, 3), Math.Round(recall, 3), Math.Round(f1Score, 3));
        }

        private bool Check(Bitmap bitmap, int x, int y, int LowerThreshold)
        {
            return bitmap.GetPixel(x, y).R >= LowerThreshold && bitmap.GetPixel(x, y).G >= LowerThreshold && bitmap.GetPixel(x, y).B >= LowerThreshold;
        }
    }
}
