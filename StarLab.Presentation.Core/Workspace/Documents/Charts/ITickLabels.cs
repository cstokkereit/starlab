namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITickLabels
    {
        /// <summary>
        /// Gets the background colour.
        /// </summary>
        string BackColour { get; }

        /// <summary>
        /// TODO
        /// </summary>
        IFont Font { get; }

        /// <summary>
        /// Gets the foreground colour.
        /// </summary>
        public string ForeColour { get; }

        /// <summary>
        /// TODO
        /// </summary>
        int Rotation { get; }

        /// <summary>
        /// A flag indicating that the tick labels are visible.
        /// </summary>
        bool Visible { get; }
    }
}
