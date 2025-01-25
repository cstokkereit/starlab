namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Represents a use case that loads a workspace from a file.
    /// </summary>
    public interface IOpenWorkspaceUseCase
    {
        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="filename">The name of the file that defines the workspace.</param>
        void Execute(string filename);
    }
}
