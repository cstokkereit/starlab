namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a chart axis.
    /// </summary>
    public interface IAxis : IFrameElement
    {
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
