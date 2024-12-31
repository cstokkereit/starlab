using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// Defines the properties and methods used by the <see cref="IAddDocumentViewPresenter"/> to control the behaviour of the Add Document dialog.
    /// </summary>
    public interface IAddDocumentView : IChildView
    {
        /// <summary>
        /// Gets or sets the name of the currently selected document type.
        /// </summary>
        string DocumentName { get; set; }

        /// <summary>
        /// Gets the type name of the currently selected document type.
        /// </summary>
        string DocumentType { get; }

        /// <summary>
        /// Adds a row to the list of available document types.
        /// </summary>
        /// <param name="key">The document type key.</param>
        /// <param name="text">The text describing the document type.</param>
        /// <param name="imageKey">The image key of the image representing the document type.</param>
        void AddDocument(string key, string text, string imageKey);

        /// <summary>
        /// Adds an <see cref="Image"/> to the list of available images.
        /// </summary>
        /// <param name="key">The image key.</param>
        /// <param name="image">The <see cref="Image"/> to be added.</param>
        void AddImage(string key, Image image);

        /// <summary>
        /// Attaches the <see cref="ICommand"/> provided to the Add button.
        /// </summary>
        /// <param name="command">The <see cref="ICommand"/> that will be executed when the Add button is clicked.</param>
        void AttachAddButtonCommand(ICommand command);

        /// <summary>
        /// Attaches the <see cref="ICommand"/> provided to the Cancel button.
        /// </summary>
        /// <param name="command">The <see cref="ICommand"/> that will be executed when the Cancel button is clicked.</param>
        void AttachCancelButtonCommand(ICommand command);
    }
}
