namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of an axis scale while the chart is being configured.
    /// </summary>
    public interface IScaleSettings : IVisibilitySettings
    {
        /// <summary>
        /// TODO
        /// </summary>
        bool Autoscale { get; set; }

        /// <summary>
        /// TODO
        /// </summary>
        ITickMarkSettings MajorTickMarks { get; }

        /// <summary>
        /// Gets or sets the maimum value.
        /// </summary>
        double Maximum { get; set; }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        double Minimum { get; set; }

        /// <summary>
        /// TODO
        /// </summary>
        ITickMarkSettings MinorTickMarks { get; }

        /// <summary>
        /// Gets or sets a flag indicating that the axis scale is reversed.
        /// </summary>
        bool Reversed { get; set; }

        /// <summary>
        /// TODO
        /// </summary>
        ITickLabelSettings TickLabels { get; }
    }
}
