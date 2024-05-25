namespace ContourDetection
{
    internal class Contour : GraphicElement
    {
        public TimeSpan TimeToFind = TimeSpan.Zero;
        public List<IAlgorithm> Algorithms;
        public Contour(IAlgorithm algorithm, Bitmap bitmap)
        {
            Algorithms = new List<IAlgorithm>() { algorithm };
            Id = Guid.NewGuid().ToString();
            Bitmap = bitmap;
        }

        public void Update(Contour contour)
        {
            Algorithms.AddRange(contour.Algorithms);
            Bitmap = contour.Bitmap;
        }

        public string GetName()
        {
            return String.Join("+", Algorithms.Select(item => item.Name));
        }

        public string GetDescription()
        {
            return String.Join("\n", Algorithms.Select(item => item.ToString()));
        }


        public string GetTime()
        {
            string result = "";
            if (TimeToFind.Hours > 0)
            {
                result += $"{TimeToFind.Hours} год. ";
            }
            if (TimeToFind.Minutes > 0)
            {
                result += $"{TimeToFind.Minutes} хв. ";
            }
            if (TimeToFind.Seconds > 0)
            {
                result += $"{TimeToFind.Seconds} сек. ";
            }
            if (TimeToFind.Milliseconds > 0)
            {
                result += $"{TimeToFind.Milliseconds} мс.";
            }
            return result;
        }

    }
}
