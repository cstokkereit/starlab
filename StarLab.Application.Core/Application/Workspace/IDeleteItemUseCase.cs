namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Represents a use case that removes an item from the workspace hierarchy.
    /// </summary>
    public interface IDeleteItemUseCase
    {
        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dtoWorkspace">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        /// <param name="key">The key that identifies the item being removed.</param>
        void Execute(WorkspaceDTO dto, string key);
    }
}
