namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Represents a use case that adds a folder at a specified location within the workspace hierarchy.
    /// </summary>
    public interface IAddFolderUseCase
    {
        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dto">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        /// <param name="key">The key that identifies the parent folder of the folder being created.</param>
        void Execute(WorkspaceDTO dto, string key);
    }
}
