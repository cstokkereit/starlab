using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IProject : ICollapsible
    {
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
        string Key { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        string ParentKey { get; }
    }
}
