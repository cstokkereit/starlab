using StarLab.Application.Workspace;

namespace StarLab.Application
{
    public interface IClipboardInteractionUseCase
    {
        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dto">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        /// <param name="key">The key that identifies the folder being copied.</param>
        void Execute(WorkspaceDTO dto, string key);
    }
}
