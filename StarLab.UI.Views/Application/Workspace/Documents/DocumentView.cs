using StarLab.Presentation;
using StarLab.Presentation.Model;

namespace StarLab.Application.Workspace.Documents
{
    public class DocumentView : DockableView
    {
        private readonly string id;

        public DocumentView(IDocument document, IControlView content, IPresenterFactory factory)
            : base(document, content, factory) 
        {
            id = document.ID;
        }

        #region #region IDockableView Members

        public override string ID => id; 

        #endregion

        //public event EventHandler<NameChangedEventArgs> NameChanged;

        //public void SetName(string newName)
        //{
        //    var oldName = Name;

        //    Name = newName;

        //    NameChanged?.Invoke(this, new NameChangedEventArgs(oldName, newName));
        //}
    }
}
