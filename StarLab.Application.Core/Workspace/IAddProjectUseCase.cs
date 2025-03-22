namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Represents a use case that adds a project to the workspace.
    /// </summary>
    public interface IAddProjectUseCase
    {
        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dtoWorkspace">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        /// <param name="dtoProject">A <see cref="ProjectDTO"/> that defines the project being added.</param>
        void Execute(WorkspaceDTO dtoWorkspace, ProjectDTO dtoProject);
    }
}
