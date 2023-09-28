namespace TSP.Domain
{
    /// <summary>
    /// Represents a collection which consists of objects of type <see cref="Point"/>.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// The collection of Points that form this Map.
        /// </summary>
        public List<Point> Points { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class with an empty collection which consists of objects of type <see cref="Point"/>.
        /// </summary>
        public Map() {
            Points = new List<Point>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class with a specified collection which consists of objects of type <see cref="Point"/>.
        /// </summary>
        /// <param name="Points">Specified collection of objects of type <see cref="Point"/>.</param>
        public Map(List<Point> Points)
        {
            this.Points = Points;
        }
        /// <summary>
        /// Adds a <see cref="Point"/> to this instance of <see cref="Map"/>.
        /// </summary>
        /// <param name="point">Specified <see cref="Point"/> that should be added to the collection.</param>
        public void Add(Point point)
        {
            Points.Add(point);
        }
    }
}
