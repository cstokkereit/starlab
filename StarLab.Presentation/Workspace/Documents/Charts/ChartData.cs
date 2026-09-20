namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Holds one or more named series of values.
    /// </summary>
    internal class ChartData : IChartData
    {
        private readonly Dictionary<string, double[]> series = new Dictionary<string, double[]>(); // A dictionary that holds the available series indexed by name.

        /// <summary>
        /// Gets the names of the available series.
        /// </summary>
        public IEnumerable<string> Series => series.Keys;

        /// <summary>
        /// Adds a series of values with the specified name.
        /// </summary>
        /// <param name="name">The name of the series.</param>
        /// <param name="values">A <see cref="double[]"/> containing the values that make up the series.</param>
        public void AddSeries(string name, double[] values)
        {
            series.Add(name, values);
        }

        /// <summary>
        /// Removes all series.
        /// </summary>
        public void Clear()
        {
            series.Clear();
        }

        /// <summary>
        /// Gets the series of values with the specified name.
        /// </summary>
        /// <param name="name">The name of the series.</param>
        /// <returns>An ordered series of double values.</returns>
        public double[] GetSeries(string name)
        {
            return series[name];
        }
    }
}
