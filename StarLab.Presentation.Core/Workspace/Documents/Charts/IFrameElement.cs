namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a visual element that is part of the chart frame.
    /// </summary>
    public interface IFrameElement : IChartElement
    {
        /// <summary>
        /// Gets the colour of the chart element.
        /// </summary>
        public string Colour { get; }
    }
}
