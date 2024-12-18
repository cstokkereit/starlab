namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IDocumentView : IDockableView, IToolbarManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        void HideSplitContent(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        void ShowSplitContent(string name);
    }
}
