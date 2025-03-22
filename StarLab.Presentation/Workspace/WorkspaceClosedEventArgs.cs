namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// Provides context for the WorkspaceClosed event.
    /// </summary>
    public class WorkspaceClosedEventArgs : EventArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="WorkspaceClosedEventArgs"/> class.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> that was closed.</param>
        public WorkspaceClosedEventArgs(IWorkspace workspace)
        {
            Workspace = workspace;
        }

        /// <summary>
        /// Gets the <see cref="IWorkspace"/> that was closed.
        /// </summary>
        public IWorkspace Workspace { get; }
    }
}
