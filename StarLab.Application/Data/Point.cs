namespace StarLab.Application.Data
{
    /// <summary>
    /// A data point that may be defined with respect to a number of different coordinate systems.
    /// </summary>
    internal class Point : IPoint
    {
        private readonly Dictionary<string, double> coordinates = new Dictionary<string, double>(); // Holds the coordinate values index by name.

        /// <summary>
        /// Initialises a new instance of the <see cref="Point"/> class.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Point"/> class.
        /// </summary>
        public Point()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Gets the X coordinate.
        /// </summary>
        public double X { get; }

        /// <summary>
        /// Gets the Y coordinate.
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// Gets the named coordinate.
        /// </summary>
        /// <param name="name">The coordinate name.</param>
        /// <returns>The value of the specified coordinate.</returns>
        public double GetCoordinate(string name)
        {
            return coordinates[name];
        }

        /// <summary>
        /// Sets the specified coordinate.
        /// </summary>
        /// <param name="name">The coordinate name.</param>
        /// <param name="value">The coordinate value.</param>
        public void SetCoordinate(string name, double value)
        {
            coordinates.Add(name, value);
        }
    }
}
