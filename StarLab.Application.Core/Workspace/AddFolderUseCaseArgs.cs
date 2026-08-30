namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A POCO that provides all of the information required to execute the AddFolderUseCase.
    /// </summary>
    public readonly struct AddFolderUseCaseArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AddFolderUseCaseArgs"/> struct.
        /// </summary>
        /// <param name="workspace">A <see cref="WorkspaceDTO"/> that holds the details of the workspace.</param>
        /// <param name="path">The folder path.</param>
        public AddFolderUseCaseArgs(WorkspaceDTO workspace, string path) 
        {
            Workspace = workspace;
            Path = path;
        }

        public readonly string Path;

        public readonly WorkspaceDTO Workspace;
    }
}
