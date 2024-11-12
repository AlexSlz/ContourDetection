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
            var mainNode = treeView.Nodes.Find(Id, false)[0];
            mainNode.Nodes.Clear();
            var groupedItems = Contours.GroupBy(item => item.Algorithm.Name).Where(group => group.Count() > 1).Select(group => group.ToList()).ToList();
            
            foreach (var algorithm  in groupedItems)
            {
                var secondNode = treeView.Nodes.Find(Id, false)[0].Nodes.Add(algorithm[0].Id, algorithm[0].GetName());
                int i = 1;
                foreach (var contour in algorithm.Skip(1))
                {
                    secondNode.Nodes.Add(contour.Id, contour.GetName() + i++);
                }
            }
        }
    }
}
