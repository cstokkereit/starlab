namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of an axis while the chart is being configured.
    /// </summary>
    public interface IAxisSettings : IChartElementSettings
    {
        /// <summary>
        /// Gets the name of the data field the axis represents.
        /// </summary>
        string Data { get; set; }

        /// <summary>
        /// Gets the axis label settings.
        /// </summary>
        ILabelSettings Label { get; }

        /// <summary>
        /// Gets the axis scale settings.
        /// </summary>
        IScaleSettings Scale { get; }
    }
}
