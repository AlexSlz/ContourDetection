using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourDetection
{
    internal static class TrainHelper
    {
        public static string SelectedWeight = "";

        public static string DataSetPath = "./";
        public static int ImageLimit = 0;
        public static int ImageSize = 224;
        public static int Epochs = 5;
        public static int BatchSize = 2;
        public static double LearningRate = 3e-4;
        public static string Optimizer = "Auto";
        public static bool CreateGraph = true;

        public static string GetLR()
        {
            return LearningRate.ToString().Replace(",", ".");
        }

        public static string GetLastFolders(string path = "")
        {
            if (path == "")
                path = DataSetPath;
            var directories = path.Split(Path.DirectorySeparatorChar)
                                  .Where(dir => !string.IsNullOrEmpty(dir))
                                  .ToArray();


            if (directories.Length < 3)
            {
                return path;
            }

            var lastThreeFolders = directories.Skip(Math.Max(0, directories.Length - 3));
            return "..\\" + Path.Combine(lastThreeFolders.ToArray());
        }

        public static void LoadMetricsToDataGridView(string filePath, DataGridView dataGridView)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string headerLine = sr.ReadLine();
                if (headerLine != null)
                {
                    string[] headers = headerLine.Split('\t');

                    foreach (string header in headers)
                    {
                        dataGridView.Columns.Add(header, header);
                    }
                }

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] values = line.Split('\t');
                    dataGridView.Rows.Add(values);
                }
            }
        }
    }
}
