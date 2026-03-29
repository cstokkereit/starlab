namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// Represents a controller that can be used to control the workspace.
    /// </summary>
    public interface IWorkspaceController
    {
        /// <summary>
        /// Closes the workspace.
        /// </summary>
        void CloseWorkspace();

        /// <summary>
        /// Creates a new workspace.
        /// </summary>
        void NewWorkspace();

        /// <summary>
        /// Opens a workspace.
        /// </summary>
        void OpenWorkspace();

        /// <summary>
        /// Saves the workspace.
        /// </summary>
        void SaveWorkspace();
    }
}
