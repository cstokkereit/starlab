namespace StarLab.Presentation.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Defines the properties and methods used by the <see cref="IOverlayManagerViewPresenter"/> to control the behaviour of the Overlay Manager panel.
    /// </summary>
    public interface IOverlayManagerView : IChildView
    {
        /// <summary>
        /// Adds an overlay check box to the view.
        /// </summary>
        /// <param name="name">The overlay name.</param>
        /// <param name="caption">The overlay caption.</param>
        /// <param name="shown">true if the overlay is shown on the chart; false otherwise.</param>
        void AddOverlay(string name, string caption, bool shown);
    }
}
