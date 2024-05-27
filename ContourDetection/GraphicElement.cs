﻿using System.Windows.Forms;

namespace ContourDetection
{
    internal abstract class GraphicElement
    {
        public string Id { get; protected set; }
        public Bitmap Bitmap;

        public void Show(PictureBox pictureBox)
        {
            pictureBox.Image = Bitmap;
        }

        public void Show(MyCustomPictureBox pictureBox)
        {
            pictureBox.PictureBox.Left = 10;
            pictureBox.PictureBox.Top = 10;
            pictureBox.PictureBox.Size = Bitmap.Size;
            pictureBox.PictureBox.Image = Bitmap;
            pictureBox.ResetZoom();
        }
    }
}
