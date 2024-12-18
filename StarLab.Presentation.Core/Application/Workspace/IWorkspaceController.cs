namespace StarLab.Application.Workspace
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IWorkspaceController : IViewController
    {
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="path"></param>
        void AddFolder(string path);

        /// <summary>
        /// 
        /// </summary>
        void CloseActiveDocument();

        /// <summary>
        /// 
        /// </summary>
        void CloseWorkspace();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void DeleteDocument(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        void DeleteFolder(string path);

        /// <summary>
        /// 
        /// </summary>
        void Exit();

        /// <summary>
        /// 
        /// </summary>
        void NewWorkspace();

        /// <summary>
        /// 
        /// </summary>
        void OpenWorkspace();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        void RenameDocument(string id, string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        void RenameFolder(string key, string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        void RenameWorkspace(string name);

        /// <summary>
        /// 
        /// </summary>
        void SaveWorkspace();
    }
}
