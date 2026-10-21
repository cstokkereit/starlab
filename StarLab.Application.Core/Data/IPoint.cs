namespace StarLab.Application.Data
{
    /// <summary>
    /// Represents a data point.
    /// </summary>
    public interface IPoint
    {
        /// <summary>
        /// Gets the X coordinate.
        /// </summary>
        double X { get; }

        /// <summary>
        /// Gets the Y coordinate.
        /// </summary>
        double Y { get; }

        /// <summary>
        /// Gets the named coordinate.
        /// </summary>
        /// <param name="name">The coordinate name.</param>
        /// <returns>The value of the specified coordinate.</returns>
        double GetCoordinate(string name);
    }
}
