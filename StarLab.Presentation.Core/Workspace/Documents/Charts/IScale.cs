namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a chart axis scale.
    /// </summary>
    public interface IScale
    {
        /// <summary>
        /// A flag indicating that the scale is generated automatically to fit the data.
        /// </summary>
        bool Autoscale { get; }

        /// <summary>
        /// Gets the background colour.
        /// </summary>
        string BackColour { get; }

        /// <summary>
        /// Gets the foreground colour.
        /// </summary>
        string ForeColour { get; }

        /// <summary>
        /// Gets the major tick marks.
        /// </summary>
        ITickMarks MajorTickMarks { get; }

        /// <summary>
        /// Gets the maximum value.
        /// </summary>
        double Maximum { get; }

        /// <summary>
        /// Gets the minimum value.
        /// </summary>
        double Minimum { get; }

        /// <summary>
        /// Gets the minor tick marks.
        /// </summary>
        ITickMarks MinorTickMarks { get; }

        /// <summary>
        /// A flag indicating that the axis scale is reversed.
        /// </summary>
        bool Reversed { get; }

        /// <summary>
        /// Gets the tick labels.
        /// </summary>
        ITickLabels TickLabels { get; }

        /// <summary>
        /// A flag indicating whether the scale is visible.
        /// </summary>
        bool Visible { get; }
    }
}
