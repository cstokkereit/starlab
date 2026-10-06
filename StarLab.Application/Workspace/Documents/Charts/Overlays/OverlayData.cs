namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// The data required to generate an overlay that contains one or more data points.
    /// </summary>
    internal class OverlayData : IOverlayData
    {
        private readonly Dictionary<string, List<double>> data = new Dictionary<string, List<double>>(); // A dictionary containing the lists of data values indexed by field name.

        /// <summary>
        /// Gets a <see cref="double[]"/> containing the ordered values that will be used to specify the location of points with respect to one of the coordinate axes.
        /// </summary>
        /// <param name="field">The name of the field that contains the data.</param>
        /// <returns>A <see cref="double[]"/> containing the specified values.</returns>
        public double[] GetValues(string field)
        {
            if (data.TryGetValue(field, out var values))
            {
                return values.ToArray();
            }

            return Array.Empty<double>();
        }

        /// <summary>
        /// Adds a value from the specified field.
        /// </summary>
        /// <param name="field">The name of the field.</param>
        /// <param name="value">The value to add.</param>
        public void AddValue(string field, double value)
        {
            if (!data.ContainsKey(field))
            {
                data[field] = new List<double>();
            }

            data[field].Add(value);
        }
    }
}
