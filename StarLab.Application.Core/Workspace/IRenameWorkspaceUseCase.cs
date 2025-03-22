namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Represents a use case that updates the workspace name.
    /// </summary>
    public interface IRenameWorkspaceUseCase
    {
        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dto">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        /// <param name="name">The new workspace name.</param>
        void Execute(WorkspaceDTO dto, string name);
    }
}
