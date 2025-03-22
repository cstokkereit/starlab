namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// Provides context for the WorkspaceChanged event.
    /// </summary>
    public class WorkspaceChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="WorkspaceChangedEventArgs"/> class.
        /// </summary>
        /// <param name="workspace">The modified <see cref="IWorkspace"/>.</param>
        public WorkspaceChangedEventArgs(IWorkspace workspace)
        {
            Workspace = workspace;
        }

        /// <summary>
        /// Gets the modified <see cref="IWorkspace"/>.
        /// </summary>
        public IWorkspace Workspace { get; }
    }
}
