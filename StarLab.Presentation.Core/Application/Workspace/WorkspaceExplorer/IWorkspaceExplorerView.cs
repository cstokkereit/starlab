using StarLab.Commands;

namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IWorkspaceExplorerView : IChildView
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        int AddImage(Image image);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parentKey"></param>
        /// <param name="text"></param>
        /// <param name="imageIndex"></param>
        void AddDocumentNode(string key, string parentKey, string text, int imageIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parentKey"></param>
        /// <param name="text"></param>
        /// <param name="imageIndex"></param>
        /// <param name="selectedImageIndex"></param>
        void AddFolderNode(string key, string parentKey, string text, int imageIndex, int selectedImageIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parentKey"></param>
        /// <param name="text"></param>
        /// <param name="imageIndex"></param>
        void AddProjectNode(string key, string parentKey, string text, int imageIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        /// <param name="imageIndex"></param>
        void AddWorkspaceNode(string key, string text, int imageIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tooltip"></param>
        /// <param name="image"></param>
        /// <param name="command"></param>
        void AddToolbarButton(string name, string tooltip, Image image, ICommand command);

        /// <summary>
        /// 
        /// </summary>
        void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void CollapseNode(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        IMenuManager CreateDocumentMenuManager(string document);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        IMenuManager CreateFolderMenuManager(string folder);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        IMenuManager CreateProjectMenuManager(string project);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IMenuManager CreateWorkspaceMenuManager();

        /// <summary>
        /// 
        /// </summary>
        string DefaultLocation { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void EditNodeLabel(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void ExpandNode(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetSelectedNode();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void SelectNode(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        void SetNodeText(string key, string text);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="imageIndex"></param>
        /// <param name="selectedImageIndex"></param>
        void UpdateNodeState(string key, int imageIndex, int selectedImageIndex);
    }
}
