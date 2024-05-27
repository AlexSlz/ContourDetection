using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;


namespace ContourDetection.Algorithms
{
    internal class DexiNed : IAlgorithm
    {
        public string Name => "DexiNed";
        public override string ToString()
        {
            return $"{Name} Datasets - BIPED";
        }
        public Contour Apply(GraphicElement image)
        {
            string workingDirectory = @$"{AppDomain.CurrentDomain.BaseDirectory}DexiNed";
            
            string imageFile = Path.Combine(workingDirectory, $@"data\image");

            image.Bitmap.Save(imageFile, ImageFormat.Png);

            string cmdFilePath = @$"{workingDirectory}\run.cmd";

            ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + cmdFilePath)
            {
                WorkingDirectory = workingDirectory,
                UseShellExecute = true,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = processInfo })
            {
                process.Start();

                process.WaitForExit();
            }

            string resultFolder = @$"{workingDirectory}\result\BIPED2CLASSIC\fused\";

            string resultFile = Directory.GetFiles(resultFolder)[0];

            Contour contour = null;
            using (Bitmap bitmap = new Bitmap(resultFile))
            {
                InvertColors(bitmap);
                contour = new Contour(this, new Bitmap(bitmap));
            }

            Directory.Delete(@$"{workingDirectory}\result", true);
            File.Delete(imageFile);

            return contour;
        }

        void InvertColors(Bitmap bitmap)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color originalColor = bitmap.GetPixel(x, y);
                    Color invertedColor = Color.FromArgb(
                        originalColor.A,
                        255 - originalColor.R, 
                        255 - originalColor.G,
                        255 - originalColor.B
                    );

                    bitmap.SetPixel(x, y, invertedColor);
                }
            }
        }
    }
}
