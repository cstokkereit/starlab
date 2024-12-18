namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IWorkspaceExplorerViewPresenter : IChildViewPresenter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void DocumentSelected(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void FolderCollapsed(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void FolderExpanded(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void FolderSelected(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeType"></param>
        /// <param name="expanded"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        int GetImageIndex(string nodeType, bool expanded, bool selected);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        void Initialise(IApplicationController controller);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void OpenDocument(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void ProjectCollapsed(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void ProjectExpanded(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void ProjectSelected(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        void RenameDocument(string id, string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        void RenameFolder(string id, string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        void RenameWorkspace(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void ShowErrorMessage(string message);

        /// <summary>
        /// 
        /// </summary>
        void ViewActivated();

        /// <summary>
        /// 
        /// </summary>
        void ViewDeactivated();

        /// <summary>
        /// 
        /// </summary>
        void WorkspaceCollapsed();

        /// <summary>
        /// 
        /// </summary>
        void WorkspaceExpanded();

        /// <summary>
        /// 
        /// </summary>
        void WorkspaceSelected();
    }
}
