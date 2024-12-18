namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IWorkspaceExplorerController : IChildViewController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void Collapse(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void Rename(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        void OpenDocument(string name);

        /// <summary>
        /// 
        /// </summary>
        void Synchronise();
    }
}
