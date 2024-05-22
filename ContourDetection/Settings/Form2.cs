using ContourDetection.Settings;

namespace ContourDetection
{
    internal partial class Form2 : Form
    {
        ContourDisplay _contourDisplay;
        public Form2(ContourDisplay contourDisplay)
        {
            InitializeComponent();
            _contourDisplay = contourDisplay;
            button1.BackColor = contourDisplay.contourColor;
            numericUpDown1.Value = contourDisplay.Thickness;
            numericUpDown2.Value = contourDisplay.LowerThreshold;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                button1.BackColor = colorDialog.Color;
                _contourDisplay.contourColor = colorDialog.Color;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _contourDisplay.Thickness = (int)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            _contourDisplay.LowerThreshold = (int)numericUpDown2.Value;
        }
    }
}
