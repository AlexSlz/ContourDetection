namespace ContourDetection
{
    internal class ContourDetector
    {
        private List<IAlgorithm> SelectedAlgorithms;

        public void Select(List<IAlgorithm> algorithms)
        {
            SelectedAlgorithms = new List<IAlgorithm>();
            SelectedAlgorithms.AddRange(algorithms);
        }

        public Contour ApplyAlgorithms(MyImage image)
        {
            var findContour = SelectedAlgorithms[0].Apply(image);
            for (int i = 1; i < SelectedAlgorithms.Count; i++)
            {
                findContour.Update(SelectedAlgorithms[i].Apply(findContour));
            }
            //image.Contours.Add(findContour);
            return findContour;
        }

        public void DrawContours(MyImage image, Contour contour, int thickness = 0, Color contourColor = default(Color))
        {
            if (object.Equals(contourColor, default(Color))) contourColor = Color.Red;

            int width = image.Bitmap.Width;
            int height = image.Bitmap.Height;

            Bitmap result = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color contourPixel = contour.Bitmap.GetPixel(x, y);

                    if (contourPixel.R == 255 && contourPixel.G == 255 && contourPixel.B == 255)
                    {
                        result.SetPixel(x, y, contourColor);

                        for (int i = 1; i <= thickness; i++)
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
