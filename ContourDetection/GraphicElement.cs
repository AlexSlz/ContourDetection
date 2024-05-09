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
    }
}
