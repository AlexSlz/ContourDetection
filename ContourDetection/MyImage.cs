namespace ContourDetection
{
    internal class MyImage : GraphicElement
    {
        public string FileName;
        public string FilePath;
        public List<Contour> Contours;

        public MyImage(string fileName)
        {
            Id = Guid.NewGuid().ToString();
            FilePath = fileName;
            FileName = Path.GetFileName(fileName);
            Bitmap = new Bitmap(fileName);
            Contours = new List<Contour>();
        }

        public void DisplayOnTreeView(TreeView treeView)
        {
            treeView.Nodes.Find(Id, false)[0].Nodes.Clear();
            foreach (var contour in Contours)
            {
                treeView.Nodes.Find(Id, false)[0].Nodes.Add(contour.Id, contour.GetName());
            }
        }
    }
}
