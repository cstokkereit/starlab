namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a chart axis scale.
    /// </summary>
    public interface IScale
    {
        /// <summary>
        /// TODO
        /// </summary>
        bool Autoscale { get; }

        /// <summary>
        /// TODO
        /// </summary>
        ITickMarks MajorTickMarks { get; }

        /// <summary>
        /// Gets the maimum value.
        /// </summary>
        double Maximum { get; }

        /// <summary>
        /// Gets the minimum value.
        /// </summary>
        double Minimum { get; }

        /// <summary>
        /// TODO
        /// </summary>
        ITickMarks MinorTickMarks { get; }

        /// <summary>
        /// A flag indicating that the axis scale is reversed.
        /// </summary>
        bool Reversed { get; }

        /// <summary>
        /// TODO
        /// </summary>
        ITickLabels TickLabels { get; }
    }
}
