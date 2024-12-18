namespace StarLab.Application.Workspace.Documents
{
    internal class Document : IDocument
    {
        private readonly string path;

        private readonly string view;

        private readonly string id;

        private string name;

        public Document(string id, string name, string path, string? view)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(view)) throw new ArgumentNullException(nameof(view));
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            this.name = name;
            this.path = path;
            this.view = view;
            this.id = id;
        }

        public Document(string name, string path, string view)
            : this(Guid.NewGuid().ToString(), name, path, view) { }

        public Document(DocumentDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentNullException(nameof(dto.Name));
            if (string.IsNullOrEmpty(dto.Path)) throw new ArgumentNullException(nameof(dto.Path));
            if (string.IsNullOrEmpty(dto.View)) throw new ArgumentNullException(nameof(dto.View));
            if (string.IsNullOrEmpty(dto.ID)) throw new ArgumentNullException(nameof(dto.ID));

            name = dto.Name;
            path = dto.Path;
            view = dto.View;
            id = dto.ID;
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
