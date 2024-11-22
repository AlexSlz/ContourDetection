using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace ContourDetection
{
    internal static class TrainHelper
    {
        public static string SelectedWeight = "";

        public static string DataSetPath = "datasets/voc";
        public static int ImageLimit = 0;
        public static int ImageSize = 224;
        public static int Epochs = 5;
        public static int BatchSize = 2;
        public static double LearningRate = 3e-4;
        public static string Optimizer = "Auto";
        public static bool SaveTxt = true;

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
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
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
                    for (int i = 1; i < values.Length; i++)
                    {
                        values[i] = values[i].Replace(".", ",");
                    }
                    dataGridView.Rows.Add(values);
                }
            }
        }

        public static void CreateChartFromData(DataGridView dataGridView, Chart chart, string[] yColumns, int step = 1, bool showLabel = false)
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();

            ChartArea chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            double xMin = double.MaxValue, xMax = double.MinValue;
            double yMin = double.MaxValue, yMax = double.MinValue;

            foreach (var yColumn in yColumns)
            {
                Series series = new Series(yColumn)
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 2,
                    MarkerStyle = MarkerStyle.Circle, 
                    MarkerSize = 7,
                    LabelForeColor = Color.Black, 
                    Font = new Font("Arial", 12) 
                };


                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    double xValue = Convert.ToDouble(row.Cells[0].Value);
                    double yValue = Convert.ToDouble(row.Cells[yColumn].Value.ToString().Replace(".", ","));
                    series.Points.AddXY(xValue, yValue);

                    if (xValue < xMin) xMin = xValue;
                    if (xValue > xMax) xMax = xValue;
                    if (yValue < yMin) yMin = yValue;
                    if (yValue > yMax) yMax = yValue;
                }

                chart.Series.Add(series);

                if(showLabel)
                    foreach (var point in series.Points)
                    {
                        point.Label = point.YValues[0].ToString("F2");
                    }
            }

            double xMargin = (xMax - xMin) * 0.2;
            double yMargin = (yMax - yMin) * 0.2;

            chart.ChartAreas[0].AxisX.Minimum = xMin - xMargin;
            chart.ChartAreas[0].AxisX.Maximum = xMax + xMargin;
            chart.ChartAreas[0].AxisY.Minimum = yMin - yMargin;
            chart.ChartAreas[0].AxisY.Maximum = yMax + yMargin;

            chart.ChartAreas[0].AxisX.Interval = step;

            chart.ChartAreas[0].AxisX.IsLogarithmic = false;

            //chart.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            //chart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart.ChartAreas[0].AxisX.LabelStyle.Format = "0";
            chart.ChartAreas[0].AxisY.LabelStyle.Format = "F2";
        }
    }
}
