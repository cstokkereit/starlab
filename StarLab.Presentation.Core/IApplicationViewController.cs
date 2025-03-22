namespace StarLab.Presentation
{
    /// <summary>
    /// Represents a controller that can be used to control the main application window.
    /// </summary>
    public interface IApplicationViewController : IViewController
    {
        /// <summary>
        /// Closes the active document.
        /// </summary>
        void CloseActiveDocument();

        /// <summary>
        /// Closes the workspace.
        /// </summary>
        void CloseWorkspace();

        /// <summary>
        /// Exists the application.
        /// </summary>
        void Exit();

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
