namespace TSP.Domain
{
     /// <summary>
     /// Represents a point in a two-dimensional space with (X, Y) coordinates.
     /// </summary>
    public class Point
    {
        /// <summary>
        /// X-coordinate of the point.
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Y-coordinate of the point
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class with default values for the coordinates.
        /// </summary>
        public Point() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class with specified values for the coordinates.
        /// </summary>
        /// <param name="X">X-coordinate of the point.</param>
        /// <param name="Y">Y-coordinate of the point.</param>
        public Point(double X, double Y) { this.X = X; this.Y = Y;}
        /// <summary>
        /// Determines whether the current instance is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>True if the current instance is equal to the specified object, otherwise false.</returns>
        public override bool Equals(object? obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Point other = (Point)obj;
            return X == other.X && Y == other.Y;
        }
        /// <summary>
        /// Computes the hash code for the current instance.
        /// </summary>
        /// <returns>A hash code value of the current instance.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
        /// <summary>
        /// Returns a string of the current's instance properties.
        /// </summary>
        /// <returns>A string in the format "Point(X, Y)" representing the current instance.</returns>
        public override string ToString()
        {
            return "Point(" + X + ", " + Y + ")";
        }
        /// <summary>
        /// Computes the cost from this to point to another.
        /// </summary>
        /// <param name="p">Another point.</param>
        /// <returns>Cost from current point to the specified point.</returns>
        public double CostTo(Point p)
        {
            return Math.Sqrt((X - p.X) * (X - p.X) + (Y - p.Y) * (Y - p.Y));
        }
    }
}
