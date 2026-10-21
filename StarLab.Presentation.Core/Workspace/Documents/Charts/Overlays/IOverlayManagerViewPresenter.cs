namespace StarLab.Presentation.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Defines the methods used by the <see cref="IOverlayManagerView"/> to communicate with its presenter.
    /// </summary>
    public interface IOverlayManagerViewPresenter : IChildViewPresenter
    {
        /// <summary>
        /// Removes the specified overlay from the chart.
        /// </summary>
        /// <param name="name">The name of the overlay.</param>
        void RemoveOverlay(string name);

        /// <summary>
        /// Adds the specified overlay to the chart.
        /// </summary>
        /// <param name="name">The name of the overlay.</param>
        void AddOverlay(string name);
    }
}
