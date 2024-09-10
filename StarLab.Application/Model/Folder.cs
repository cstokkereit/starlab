using StarLab.Application.DataTransfer;

namespace StarLab.Application.Model
{
    internal class Folder : IFolder
    {
        private const string PATH = "{0}/{1}";

        private readonly IList<IDocument> documents = new List<IDocument>();

        private readonly IList<IFolder> folders = new List<IFolder>();

        private string name = string.Empty;

        #region Constructors

        public Folder(FolderDTO dto, IFolder parent)
        {
            Name = dto.Path == null ? string.Empty : dto.Path.Substring(dto.Path.LastIndexOf('/') + 1);

            if (parent != null)
            {
                parent.AddFolder(this);
                Parent = parent;
            }

            Expanded = dto.Expanded;
        }

        public Folder(FolderDTO dto)
        {
            Name = dto.Path == null ? string.Empty : dto.Path;

            Expanded = dto.Expanded;
        }

        #endregion

        #region IFolder Members

        public IEnumerable<IDocument> Documents => documents;

        public bool Expanded { get; private set; }

        public IEnumerable<IFolder> Folders => folders;

        public string Name
        {
            get => name;

            set
            {
                //ValidateFolderName(value, Parent.Folders);
                name = value;
            }
        }

        public IFolder? Parent { get; private set; }

        public string Path
        {
            get { return Parent == null ? Name : string.Format(PATH, Parent.Path, Name); }
        }

        public void AddFolder(IFolder folder)
        {
            //ValidateFolderName(folder.Name, folders);

            folders.Add(folder);
        }





        public void AddDocument(Document document)
        {
            ValidateDocumentName(document.Name);

            documents.Add(document);
        }









        #endregion

        private void ValidateDocumentName(string name)
        {
            if (name.Contains('/')) throw new ArgumentException(); // TODO

            foreach (var document in documents)
            {
                if (document.Name.Equals(name)) throw new ArgumentException(); // TODO
            }
        }

        private void ValidateFolderName(string name, IEnumerable<IFolder> folders)
        {
            if (name.Contains('/')) throw new ArgumentException(); // TODO

            foreach (var folder in folders)
            {
                if (folder.Name.Equals(name)) throw new ArgumentException(); // TODO
            }
        }
    }
}
