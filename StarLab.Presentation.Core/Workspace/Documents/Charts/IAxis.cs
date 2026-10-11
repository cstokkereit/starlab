namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a chart axis.
    /// </summary>
    public interface IAxis : IChartElement
    {
        /// <summary>
        /// Gets the name of the data field the axis represents.
        /// </summary>
        string Data { get; }

        /// <summary>
        /// Gets the axis <see cref="ILabel"/>.
        /// </summary>
        ILabel Label { get; }

        /// <summary>
        /// Gets the axis <see cref="IScale"/>.
        /// </summary>
        IScale Scale { get; }
    }
}
