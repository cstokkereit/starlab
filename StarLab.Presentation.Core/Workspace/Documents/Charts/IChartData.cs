namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents one or more named series of values.
    /// </summary>
    public interface IChartData
    {
        /// <summary>
        /// Gets the names of the available series.
        /// </summary>
        IEnumerable<string> Series { get; }

        /// <summary>
        /// Gets the series of values with the specified name.
        /// </summary>
        /// <param name="name">The name of the series.</param>
        /// <returns>An ordered series of double values.</returns>
        double[] GetSeries(string name);
    }
}
