namespace StarLab.Application.Data
{
    /// <summary>
    /// Represents a point with a label.
    /// </summary>
    public interface ILabelledPoint : IPoint
    {
        /// <summary>
        /// Gets the label.
        /// </summary>
        string Label { get; }
    }
}
