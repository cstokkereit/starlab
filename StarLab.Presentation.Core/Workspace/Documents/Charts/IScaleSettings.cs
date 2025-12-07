namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of an axis scale while the chart is being configured.
    /// </summary>
    public interface IScaleSettings :  IColourSettings, IVisibilitySettings
    {
        /// <summary>
        /// Gets or sets a flag that determines whether the scale is generated automatically to fit the data.
        /// </summary>
        bool Autoscale { get; set; }

        /// <summary>
        /// Gets the settings for the major tick marks.
        /// </summary>
        ITickMarkSettings MajorTickMarks { get; }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        double Maximum { get; set; }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        double Minimum { get; set; }

        /// <summary>
        /// Gets the settings for the minor tick marks.
        /// </summary>
        ITickMarkSettings MinorTickMarks { get; }

        /// <summary>
        /// Gets or sets a flag indicating that the axis scale is reversed.
        /// </summary>
        bool Reversed { get; set; }

        /// <summary>
        /// Gets the tick label settings.
        /// </summary>
        ITickLabelSettings TickLabels { get; }
    }
}
