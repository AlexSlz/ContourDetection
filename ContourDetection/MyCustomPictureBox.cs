namespace ContourDetection
{
    internal class MyCustomPictureBox
    {
        PictureBox _pictureBox;

        bool _dragging = false;

        Point _downPoint;

        float _zoomScale = 1.0f;
        const float MinZoomScale = 0.2f; // Minimum zoom scale
        const float MaxZoomScale = 10.0f; // Maximum zoom scale

        public PictureBox PictureBox
        {
            get { return _pictureBox; }
        }

        public void ResetZoom()
        {
            _zoomScale = 1;
        }

        public MyCustomPictureBox(PictureBox pictureBox)
        {
            _pictureBox = pictureBox;
            _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            _pictureBox.MouseDown += _pictureBox_MouseDown;
            _pictureBox.MouseMove += _pictureBox_MouseMove;
            _pictureBox.MouseUp += _pictureBox_MouseUp;
            _pictureBox.MouseWheel += _pictureBox_MouseWheel;
        }

        private void _pictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0)
            {
                ZoomPictureBox(e.Delta, e.Location);
            }
        }

        private void ZoomPictureBox(int delta, Point location)
        {
            const float zoomFactor = 1.1f;

            if (delta > 0)
            {
                _zoomScale *= zoomFactor;
            }
            else
            {
                _zoomScale /= zoomFactor;
            }

            _zoomScale = Math.Max(MinZoomScale, Math.Min(MaxZoomScale, _zoomScale));

            int newWidth = (int)(_pictureBox.Image.Width * _zoomScale);
            int newHeight = (int)(_pictureBox.Image.Height * _zoomScale);

            float relativeX = (float)location.X / _pictureBox.Width;
            float relativeY = (float)location.Y / _pictureBox.Height;

            int newLeft = _pictureBox.Left - (int)((newWidth - _pictureBox.Width) * relativeX);
            int newTop = _pictureBox.Top - (int)((newHeight - _pictureBox.Height) * relativeY);

            _pictureBox.Width = newWidth;
            _pictureBox.Height = newHeight;
            _pictureBox.Left = newLeft;
            _pictureBox.Top = newTop;

            _pictureBox.Invalidate();
        }

        private void _pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void _pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            Control c = sender as Control;
            if (_dragging && c != null)
            {
                c.Top = e.Y + c.Top - _downPoint.Y;
                c.Left = e.X + c.Left - _downPoint.X;
            }
        }

        private void _pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                _dragging = true;
                _downPoint.X = e.X;
                _downPoint.Y = e.Y;
            }
        }
    }
}
