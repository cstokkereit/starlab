using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IAddDocumentView : IChildView
    {
        /// <summary>
        /// 
        /// </summary>
        string DocumentName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string DocumentType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        /// <param name="imageKey"></param>
        void AddDocument(string key, string text, string imageKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="image"></param>
        void AddImage(string key, Image image);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        void AttachAddButtonCommand(ICommand command);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        void AttachCancelButtonCommand(ICommand command);
    }
}
