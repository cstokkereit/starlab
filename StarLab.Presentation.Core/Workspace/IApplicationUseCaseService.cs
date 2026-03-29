namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// Defines the methods required to execute the use cases that implement the application functionality.
    /// </summary>
    public interface IApplicationUseCaseService : IUseCaseService
    {
        /// <summary>
        /// Executes the OpenWorkspace use case.
        /// </summary>
        /// <param name="filename">The fully qualified path to the workspace file.</param>
        void OpenWorkspace(string filename);

        /// <summary>
        /// Executes the SaveWorkspace use case.
        /// </summary>
        /// <param name="workspace">The current workspace.</param>
        void SaveWorkspace(IWorkspace workspace);
    }
}
