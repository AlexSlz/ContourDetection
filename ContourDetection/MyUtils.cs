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

        public static Bitmap ToBlackWhite(Bitmap originalImage)
        {
            Bitmap resultImage = new Bitmap(originalImage.Width, originalImage.Height);

            for (int y = 0; y < originalImage.Height; y++)
            {
                for (int x = 0; x < originalImage.Width; x++)
                {
                    Color pixelColor = originalImage.GetPixel(x, y);

                    if (pixelColor.R == 0 && pixelColor.G == 0 && pixelColor.B == 0)
                    {
                        resultImage.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        resultImage.SetPixel(x, y, Color.White);
                    }
                }
            }
            return resultImage;
        }
    }
}
