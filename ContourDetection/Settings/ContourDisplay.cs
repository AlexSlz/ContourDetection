namespace ContourDetection.Settings
{
    internal class ContourDisplay
    {
        public Color contourColor = Color.Red;
        public int Thickness = 0;
        public int LowerThreshold = 150;

        public void DrawContours(MyImage image, Contour contour)
        {
            int width = image.Bitmap.Width;
            int height = image.Bitmap.Height;

            Bitmap result = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color contourPixel = contour.Bitmap.GetPixel(x, y);

                    if (contourPixel.R >= LowerThreshold && contourPixel.G >= LowerThreshold && contourPixel.B >= LowerThreshold)
                    {
                        result.SetPixel(x, y, contourColor);

                        for (int i = 1; i <= Thickness; i++)
                        {
                            if (x + i < width)
                                result.SetPixel(x + i, y, contourColor);
                            if (x - i >= 0)
                                result.SetPixel(x - i, y, contourColor);
                            if (y + i < height)
                                result.SetPixel(x, y + i, contourColor);
                            if (y - i >= 0)
                                result.SetPixel(x, y - i, contourColor);
                        }

                    }
                    else
                    {
                        Color originalPixel = image.Bitmap.GetPixel(x, y);
                        result.SetPixel(x, y, originalPixel);
                    }
                }
            }

            contour.Bitmap = result;
        }
    }
}
