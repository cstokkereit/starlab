namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// Provides context for the ActiveDocumentChanged event.
    /// </summary>
    public class ActiveDocumentChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ActiveDocumentChangedEventArgs"/> class.
        /// </summary>
        /// <param name="workspace">The modified <see cref="IWorkspace"/>.</param>
        public ActiveDocumentChangedEventArgs(IWorkspace workspace)
        {
            Workspace = workspace;
        }

        /// <summary>
        /// Gets the modified <see cref="IWorkspace"/>.
        /// </summary>
        public IWorkspace Workspace { get; }
    }
}
