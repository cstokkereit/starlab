namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Represents a use case that updates the name of an item within the workspace hierarchy.
    /// </summary>
    public interface IRenameItemUseCase
    {
        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dtoWorkspace">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        /// <param name="key">The key that identifies the item being renamed.</param>
        /// <param name="name">The new item name.</param>
        void Execute(WorkspaceDTO dto, string key, string name);
    }
}
