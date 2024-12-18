using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IWorkspace
    {
        /// <summary>
        /// 
        /// </summary>
        IDocument? ActiveDocument { get; }

        /// <summary>
        /// 
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// 
        /// </summary>
        string Layout { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<IDocument> Documents { get; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<IFolder> Folders { get; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<IProject> Projects { get; }

        /// <summary>
        /// 
        /// </summary>
        void ClearActiveDocument();

        /// <summary>
        /// 
        /// </summary>
        void Collapse();

        /// <summary>
        /// 
        /// </summary>
        void Expand();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IDocument GetDocument(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IFolder GetFolder(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IProject GetProject(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool HasDocument(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool HasFolder(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool HasProject(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void SetActiveDocument(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layout"></param>
        void UpdateLayout(string layout);
    }
}
