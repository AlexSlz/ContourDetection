using Emgu.CV;
using Emgu.CV.Dnn;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using System.Drawing;
using Emgu.CV.Reg;
using System.Windows.Forms.VisualStyles;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace ContourDetection.Algorithms
{
    internal class Hed : IAlgorithm
    {
        public string Name => "Hed";

        public override string ToString()
        {
            return $"{Name} Datasets - BSDS500";
        }

        public Contour Apply(GraphicElement image)
        {
            string workingDirectory = @$"{AppDomain.CurrentDomain.BaseDirectory}{Name}";

            string imageFile = Path.Combine(workingDirectory, $@"image");

            image.Bitmap.Save(imageFile, ImageFormat.Png);

            string cmdFilePath = @$"py hed.py";

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

            string resultFile = @$"{workingDirectory}\edge_detected_image.png";

            Contour contour = null;
            using (Bitmap bitmap = new Bitmap(resultFile))
            {
                contour = new Contour(this, new Bitmap(bitmap));
            }

            File.Delete(resultFile);
            File.Delete(imageFile);

            return contour;
        }
    }
}
