namespace StarLab.Application.Workspace.Documents
{
    internal class Document : IDocument
    {
        private readonly string path;

        private readonly string view;

        private readonly string id;

        private string name;

        public Document(string id, string name, string path, string view)
        {
            this.name = name;
            this.path = path;
            this.view = view;
            this.id = id;
        }

        public Document(string name, string path, string view)
            : this(Guid.NewGuid().ToString(), name, path, view) { }

        public Document(DocumentDTO dto)
        {
            name = dto.Name ?? throw new ArgumentException(); // TODO
            path = dto.Path ?? throw new ArgumentException();
            view = dto.View ?? throw new ArgumentException();
            id = dto.ID ?? throw new ArgumentException();
        }

        public event EventHandler<string>? NameChanged;

        public string FullName => $"{Path}/{Name}";

        public string ID => id;

        public string Name
        {
            get { return name; }

            set
            {
                name = value;

                NameChanged?.Invoke(this, name);
            }
        }

        public string Path => path;

        public string View => view;
    }
}
