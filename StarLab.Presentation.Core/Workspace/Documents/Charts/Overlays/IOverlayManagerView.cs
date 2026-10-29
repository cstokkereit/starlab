namespace StarLab.Presentation.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Defines the properties and methods used by the <see cref="IOverlayManagerViewPresenter"/> to control the behaviour of the Overlay Manager panel.
    /// </summary>
    public interface IOverlayManagerView : IChildView, ICancellableView
    {
        /// <summary>
        /// Adds the specified overlay to the list of available overlays.
        /// </summary>
        /// <param name="name">The overlay name.</param>
        /// <param name="caption">The overlay caption.</param>
        /// <param name="shown">true if the overlay is currently shown on the chart; <c>false</c>.</param>
        void AddOverlay(string name, string caption, bool shown);

        /// <summary>
        /// Clears the list of overlays.
        /// </summary>
        void Clear();
    }
}
