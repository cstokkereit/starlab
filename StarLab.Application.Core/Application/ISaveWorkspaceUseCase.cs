using StarLab.Application.Workspace;

namespace StarLab.Application
{
    /// <summary>
    /// Represents a use case that creates a file containing the definition of the current workspace.
    /// </summary>
    public interface ISaveWorkspaceUseCase
    {
        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dto">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        void Execute(WorkspaceDTO dto);
    }
}
