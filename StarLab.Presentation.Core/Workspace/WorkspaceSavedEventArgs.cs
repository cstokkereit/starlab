namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// Provides context for the WorkspaceSaved event.
    /// </summary>
    public class WorkspaceSavedEventArgs : EventArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="WorkspaceSavedEventArgs"/> class.
        /// </summary>
        /// <param name="workspace">The modified <see cref="IWorkspace"/>.</param>
        public WorkspaceSavedEventArgs(IWorkspace workspace)
        {
            Workspace = workspace;
        }

        /// <summary>
        /// Gets the modified <see cref="IWorkspace"/>.
        /// </summary>
        public IWorkspace Workspace { get; }
    }
}
