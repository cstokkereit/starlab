namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// Defines the methods used by the <see cref="IAddDocumentView"/> to communicate with its presenter.
    /// </summary>
    public interface IAddDocumentViewPresenter : IChildViewPresenter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="definitionName"></param>
        void AddDocument(string name, string definitionName);
    }
}
