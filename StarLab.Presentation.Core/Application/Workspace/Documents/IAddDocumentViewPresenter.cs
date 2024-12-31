namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// Defines the methods used by the <see cref="IAddDocumentView"/> to communicate with its presenter.
    /// </summary>
    public interface IAddDocumentViewPresenter : IChildViewPresenter
    {
        /// <summary>
        /// Adds the document to the workspace and closes the dialog controlled by the parent controller.
        /// </summary>
        void AddDocument();

        /// <summary>
        /// Closes the dialog controlled by the parent controller.
        /// </summary>
        void Cancel();
    }
}
