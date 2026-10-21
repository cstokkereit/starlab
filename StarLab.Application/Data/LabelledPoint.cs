namespace StarLab.Application.Data
{
    /// <summary>
    /// A point with a label.
    /// </summary>
    internal class LabelledPoint : Point, ILabelledPoint
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="LabelledPoint"/> class.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="label">The point label.</param>
        public LabelledPoint(double x, double y, string label)
            : base(x, y)
        {
            Label = label;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="LabelledPoint"/> class.
        /// </summary>
        /// <param name="label">The point label.</param>
        public LabelledPoint(string label)
        {
            Label = label;
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        public string Label { get; }
    }
}
