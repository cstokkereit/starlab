using StarLab.Application;

namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkspaceController : IWorkspaceController
    {
        public WorkspaceController()
        {

        }

        /// <summary>
        /// Closes the workspace.
        /// </summary>
        public void CloseWorkspace()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new workspace.
        /// </summary>
        public void NewWorkspace()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Opens a workspace.
        /// </summary>
        public void OpenWorkspace()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the workspace.
        /// </summary>
        public void SaveWorkspace()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Opens the default workspace.
        /// </summary>
        private void OpenDefaultWorkspace()
        {
            //if (!string.IsNullOrEmpty(Settings.Workspace))
            //{
            //    OpenWorkspace(Settings.Workspace);
            //}
        }

        /// <summary>
        /// Opens the specified workspace file. If the path to the workspace file is omitted an Open File dialog will be displayed.
        /// </summary>
        /// <param name="filename">The fully qualified path to the workspace file.</param>
        private void OpenWorkspace(string filename)
        {
            //if (string.IsNullOrEmpty(filename))
            //{
            //    filename = View.ShowOpenFileDialog(StringResources.OpenWorkspace, StringResources.WorkspaceFileFilter);
            //}

            //var interactor = UseCaseFactory.CreateOpenWorkspaceUseCase(this);

            //interactor.Execute(filename);

            //UpdateCommandState(Actions.CloseWorkspace, true);
        }
    }
}
