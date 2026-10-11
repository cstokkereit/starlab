namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// 
    /// </summary>
    public class ChartOverlayUseCaseArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="overlay"></param>
        public ChartOverlayUseCaseArgs(string overlay)
        {
            Overlay = overlay;
        }

        /// <summary>
        /// Gets the name of the overlay.
        /// </summary>
        public string Overlay { get; }
    }
}
