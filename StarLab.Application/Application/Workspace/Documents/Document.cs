namespace StarLab.Application.Workspace.Documents
{
    internal class Document
    {
        private readonly string view;

        private readonly string id;

        public Document(DocumentDTO dto)
        {
            Name = dto.Name ?? throw new ArgumentException();
            Path = dto.Path ?? throw new ArgumentException();

            view = dto.View ?? throw new ArgumentException();
            id = dto.ID ?? throw new ArgumentException();
        }

        public string ID => id;

        public string Name { get; set; }

        public string Path { get; set; }

        public string View => view;
    }
}
