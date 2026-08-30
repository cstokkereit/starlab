namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A POCO that provides all of the information required to execute the RenameFolderUseCase.
    /// </summary>
    public readonly struct RenameFolderUseCaseArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentUseCaseArgs"/> struct.
        /// </summary>
        /// <param name="workspace">A <see cref="WorkspaceDTO"/> that holds the details of the workspace.</param>
        /// <param name="path">The folder path.</param>
        /// <param name="name">The folder name.</param>
        public RenameFolderUseCaseArgs(WorkspaceDTO workspace, string path, string name)
        {
            Workspace = workspace;
            Name = name;
            Path = path;
        }

        public readonly string Name;

        public readonly string Path;

        public readonly WorkspaceDTO Workspace;
    }
}
