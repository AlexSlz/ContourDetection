using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Emgu.CV.OCR.Tesseract;
using static System.Net.WebRequestMethods;

namespace ContourDetection
{
    internal static class PyUtils
    {
        static string workingDirectory = @$"{AppDomain.CurrentDomain.BaseDirectory}../py";
        static string workingDirectoryTrain = @$"{workingDirectory}/train";
        public static string wD => workingDirectory;
        public static string Run(string cmdCommand, GraphicElement image, string folderName)
        {
            if (!Directory.Exists(workingDirectory))
            {
                MessageBox.Show("Немає папки пайтон");
                return null;
            }
            string imageFile = Path.Combine(workingDirectory, $@"input.jpg");

            image.Bitmap.Save(imageFile, ImageFormat.Jpeg);

            ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + cmdCommand)
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

            return Directory.GetFiles($"{workingDirectory}/results/{folderName}")[0];
        }

        public static string RunTrain(string cmdCommand, string folderName)
        {
            if (!Directory.Exists(workingDirectoryTrain))
            {
                MessageBox.Show("Немає папки пайтон.");
                return null;
            }

            string args = $" --lr {TrainHelper.GetLR()}" +
                $" --isize {TrainHelper.ImageSize}" +
                $" --ilimit {TrainHelper.ImageLimit}" +
                $" --bsize {TrainHelper.BatchSize}" +
                $" --e {TrainHelper.Epochs}" +
                $" --o {TrainHelper.Optimizer}" +
                $" --dpath {TrainHelper.DataSetPath}" +
                $" --model {folderName}";
            if(TrainHelper.CreateGraph)
                args += " --graph";
            if (!TrainHelper.DataSetPath.Contains("VOC", StringComparison.OrdinalIgnoreCase))
                args += " --customDataSet";

            ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + cmdCommand + args)
            {
                WorkingDirectory = workingDirectoryTrain,
                UseShellExecute = true,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = processInfo })
            {
                process.Start();
                process.WaitForExit();
            }
            //string txtFile = Directory.GetFiles($"{workingDirectoryTrain}/../results_train/{folderName}", "*.pth")[0];
            //string graph = Directory.GetFiles($"{workingDirectoryTrain}/../results_train/{folderName}", "*.png")[0];
            //TrainHelper.WeightsPath.Add(pthFiles.Find(item => item.Contains($"e{TrainHelper.Epochs}")));
            return "Hello Moto";
        }

        public static List<string> GetMasksList(string folderName)
        {
            List<string> files = Directory.GetFiles($"{workingDirectory}/results/{folderName}/masks").ToList();
            return files;
        }
    }
}
