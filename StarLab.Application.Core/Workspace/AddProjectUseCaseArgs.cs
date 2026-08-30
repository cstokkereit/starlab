namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A POCO that provides all of the information required to execute the AddProjectUseCase.
    /// </summary>
    public readonly struct AddProjectUseCaseArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AddProjectUseCaseArgs"/> struct.
        /// </summary>
        /// <param name="workspace">A <see cref="WorkspaceDTO"/> that holds the details of the workspace.</param>
        /// <param name="project">A <see cref="ProjectDTO"/> that holds the details of the project.</param>
        public AddProjectUseCaseArgs(WorkspaceDTO workspace, ProjectDTO project)
        {
            Workspace = workspace;
            Project = project;
        }

        public readonly ProjectDTO Project;

        public readonly WorkspaceDTO Workspace;
    }
}
