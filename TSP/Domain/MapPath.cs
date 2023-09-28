namespace TSP.Domain
{
    /// <summary>
    /// A collection of objects of type <see cref="Point"/> that represent a path, with a starting <see cref="Point"/>, final <see cref="Point"/> and a total cost of the path.
    /// </summary>
    public class MapPath
    {
        /// <summary>
        /// Collection of objects of type <see cref="Point"/> which form this <see cref="MapPath"/>.
        /// </summary>
        public List<Point> Points { get; set; }
        /// <summary>
        /// Starting <see cref="Point"/> of the <see cref="MapPath"/>.
        /// </summary>
        public Point? Source { get; set; }
        /// <summary>
        /// Final <see cref="Point"/> of the <see cref="MapPath"/>.
        /// </summary>
        public Point? Destination { get; set; }
        /// <summary>
        /// Cost of the <see cref="MapPath"/>.
        /// </summary>
        public double ManualCost { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="MapPath"/> class with an empty collection of objects which consists of objects of type <see rcef="Point"/>.
        /// </summary>
        public MapPath()
        {
            Points = new List<Point>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MapPath"/> class with a specified collection which consists of objects of type <see cref="Point"/>. 
        /// The first object in the collection will be considered the starting <see cref="Point"/>, and the last will be considered the final <see cref="Point"/>.
        /// </summary>
        public MapPath(List<Point> points)
        {
            this.Points = points;
            if(this.Points.Count > 0)
            {
                Source = this.Points[0];
                Destination = this.Points[points.Count - 1];
            }
        }
        /// <summary>
        /// Adds a new <see cref="Point"/> to the current <see cref="MapPath"/>. 
        /// If the collection is empty, the <see cref="Point"/> added will be considered a starting and ending <see cref="Point"/>. 
        /// If the collection is not empty, the <see cref="Point"/> added will be considered an ending <see cref="Point"/>.
        /// </summary>
        /// <param name="point"></param>
        public void Add(Point point)
        {
            if(this.Points.Count == 0)
            {
                Source = point;
            }
            Points.Add(point);
            Destination = point;
        }
        /// <summary>
        /// Shuffles the collection of objects of type <see cref="Point"/> by using a variation of the Fisher-Yates Shuffle algorithm. 
        /// At the end of the shuffling, the starting and final objects of type <see cref="Point"/> will be updated correspondently.
        /// </summary>
        public void Shuffle()
        {
            // A variation of the Fisher-Yates Shuffle algorithm.
            Random random = new Random();
            int n = Points.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Point value = Points[k];
                Points[k] = Points[n];
                Points[n] = value;
            }
            if (Points.Count > 0)
            {
                Source = Points[0];
                Destination = Points[Points.Count - 1];
            }
        }
        /// <summary>
        /// Shuffles the collection of objects of type <see cref="Point"/>, but with a specified start and end.
        /// </summary>
        /// <param name="source">The specified starting <see cref="Point"/>.</param>
        /// <param name="destination">The specified ending <see cref="Point"/></param>
        /// <exception cref="Exception">Throws an Exception if the starting or ending <see cref="Point"/> are not found in the collection.</exception>
        public void Generate(Point source, Point destination)
        {
            if (Points.Find(point => point == source) == null)
            {
                throw new Exception("Source " + source.ToString() + " not found in Path.");
            }
            if (Points.Find(point => point == destination) == null)
            {
                throw new Exception("Destination " + destination.ToString() + " not found in Path.");
            }
            Points.Remove(source);
            Points.Remove(destination);
            Random random = new Random();
            Points.Sort((X, Y) => random.Next(-1, 2));
            Points.Insert(0, source);
            Points.Add(destination);
        }
    }
}
