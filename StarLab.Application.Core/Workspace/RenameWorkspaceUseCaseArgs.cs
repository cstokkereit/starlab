namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A POCO that provides all of the information required to execute the RenameWorkspaceUseCase.
    /// </summary>
    public readonly struct RenameWorkspaceUseCaseArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="RenameWorkspaceUseCaseArgs"/> struct.
        /// </summary>
        /// <param name="workspace">A <see cref="WorkspaceDTO"/> that holds the details of the workspace.</param>
        /// <param name="name">The workspace name.</param>
        public RenameWorkspaceUseCaseArgs(WorkspaceDTO workspace, string name)
        {
            Workspace = workspace;
            Name = name;
        }

        public readonly string Name;

        public readonly WorkspaceDTO Workspace;
    }
}
