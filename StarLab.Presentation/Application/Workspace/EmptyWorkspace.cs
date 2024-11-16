using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    internal class EmptyWorkspace : IWorkspace
    {   
        public IDocument? ActiveDocument => null;

        public IEnumerable<IDocument> Documents => new List<IDocument>();

        public string FileName => string.Empty;

        public IEnumerable<IFolder> Folders => new List<IFolder>();

        public string Layout => string.Empty;

        public string Name => string.Empty;

        public IEnumerable<IProject> Projects => new List<IProject>();

        public void ClearActiveDocument()
        {
            // Do Nothing
        }

        public void Collapse()
        {
            // Do Nothing
        }

        public void Expand()
        {
            // Do Nothing
        }

        public IDocument GetDocument(string id)
        {
            throw new NotImplementedException();
        }

        public IFolder GetFolder(string path)
        {
            throw new NotImplementedException();
        }

        public IProject GetProject(string path)
        {
            throw new NotImplementedException();
        }

        public bool HasDocument(string id)
        {
            return false;
        }

        public bool HasFolder(string key)
        {
            return false;
        }

        public bool HasProject(string key)
        {
            return false;
        }

        public void SetActiveDocument(string id)
        {
            // Do Nothing
        }

        public void UpdateLayout(string layout)
        {
            // Do Nothing
        }
    }
}
