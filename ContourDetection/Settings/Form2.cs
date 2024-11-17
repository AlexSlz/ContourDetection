using ContourDetection.Settings;

namespace ContourDetection
{
    internal partial class Form2 : Form
    {
        ContourDisplay _contourDisplay;
        public Form2(ContourDisplay contourDisplay, EventHandler contour, EventHandler mask)
        {
            InitializeComponent();
            _contourDisplay = contourDisplay;
            numericUpDown1.Value = contourDisplay.Thickness;
            numericUpDown2.Value = contourDisplay.OpacityPercent;
            button1.Click += contour;
            button2.Click += mask;
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _contourDisplay.Thickness = (int)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            _contourDisplay.OpacityPercent = (int)numericUpDown2.Value;
        }
    }
}
