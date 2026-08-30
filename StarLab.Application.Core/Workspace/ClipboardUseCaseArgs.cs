namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A POCO that provides all of the information required to execute the clipboard UseCase.
    /// </summary>
    public readonly struct ClipboardUseCaseArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ClipboardUseCaseArgs"/> struct.
        /// </summary>
        /// <param name="workspace">A <see cref="WorkspaceDTO"/> that holds the details of the workspace.</param>
        /// <param name="source">The source document or folder.</param>
        /// <param name="destination">The destination folder.</param>
        public ClipboardUseCaseArgs(WorkspaceDTO workspace, string source, string destination)
        {
            Destination = destination;
            Workspace = workspace;
            Source = source;
        }

        public readonly string Destination;

        public readonly string Source;

        public readonly WorkspaceDTO Workspace;
    }
}
