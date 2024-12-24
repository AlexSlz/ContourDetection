namespace ContourDetection
{
    internal class Contour : GraphicElement
    {
        public TimeSpan TimeToFind = TimeSpan.Zero;

        public IAlgorithm Algorithm;
        public Contour(IAlgorithm algorithm, Bitmap bitmap)
        {
            Algorithm = algorithm;
            Id = Guid.NewGuid().ToString();
            Bitmap = bitmap;
        }

        public Contour(IAlgorithm algorithm, string fileName, int width, int height)
        {
            Id = Guid.NewGuid().ToString();
            Algorithm = algorithm;
            Bitmap = new Bitmap(fileName);
            Bitmap = new Bitmap(Bitmap, width, height);
        }

        public string GetName()
        {
            return String.Join("+", Algorithm.Name);
        }

        public string GetDescription()
        {
            return String.Join("\n", Algorithm.ToString())+ $"\nКонтури були знайдені за {GetTime()}";
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
