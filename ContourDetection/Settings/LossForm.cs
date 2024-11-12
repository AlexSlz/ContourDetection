using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ContourDetection.Settings
{
    public partial class LossForm : Form
    {
        public LossForm(DataGridView dataGridView, string[] yColumns, int step = 1)
        {
            InitializeComponent();
            this.Text = string.Join(", ", yColumns);
            TrainHelper.CreateChartFromData(dataGridView, chart1, yColumns);
        }
    }
}
