using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV;
using System.Threading.Tasks;
using Emgu.CV.CvEnum;
using System.Drawing.Imaging;
using System.Drawing;

namespace ContourDetection.Settings
{
    internal class ContourDisplay
    {
        public int Thickness = 1;
        public int OpacityPercent = 50;

        public Bitmap DrawContours(MyImage image, List<Contour> contours)
        {
            Bitmap result = new Bitmap(image.Bitmap);
            contours.RemoveAt(0);
            Random random = new Random();
            foreach (Contour contour in contours)
            {
                Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                Bitmap mask = contour.Bitmap;
                result = DrawContour(result, mask, randomColor);
            }

            return result;
        }

        private Bitmap DrawContour(Bitmap image, Bitmap mask, Color contourColor)
        {
            int width = image.Width;
            int height = image.Height;

            Bitmap result = new Bitmap(image);

            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    if (IsEdge(mask, x, y))
                    {
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
                }
            }

            return result;
        }

        private bool IsEdge(Bitmap mask, int x, int y)
        {
            Color center = mask.GetPixel(x, y);

            if (center.R == 0 || center.G == 0 || center.B == 0)
            {
                return false;
            }
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    if (dx == 0 && dy == 0)
                        continue;

                    Color neighbor = mask.GetPixel(x + dx, y + dy);

                    if (neighbor.R < 50 && neighbor.G < 50 && neighbor.B < 50)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public Bitmap DrawMasks(MyImage image, List<Contour> contours)
        {
            int width = image.Bitmap.Width;
            int height = image.Bitmap.Height;
            contours.RemoveAt(0);
            Bitmap result = new Bitmap(image.Bitmap);

            Random random = new Random();

            foreach (var contour in contours)
            {
                Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color contourPixel = contour.Bitmap.GetPixel(x, y);

                        if (contourPixel.R >= 150 && contourPixel.G >= 150 && contourPixel.B >= 150)
                        {
                            int opacity = (OpacityPercent * 255) / 100;
                            Color color = result.GetPixel(x, y);
                            int r = (color.R * (255 - opacity) + randomColor.R * opacity) / 255;
                            int g = (color.G * (255 - opacity) + randomColor.G * opacity) / 255;
                            int b = (color.B * (255 - opacity) + randomColor.B * opacity) / 255;
                            int a = (color.A * (255 - opacity) + randomColor.A * opacity) / 255; // This combines the alpha (opacity) values as well

                            Color blendedColor = Color.FromArgb(a, r, g, b);
                            result.SetPixel(x, y, blendedColor);
                        }
                    }
                }
            }
            return result;
        }
    }
}
