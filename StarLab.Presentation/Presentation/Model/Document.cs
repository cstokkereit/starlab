using StarLab.Application.DataTransfer;

namespace StarLab.Presentation.Model
{
    public class Document : IDocument
    {
        private readonly Content content;

        private readonly string path;

        private readonly string type;

        private readonly string view;

        private readonly string id;

        private string name;

        public Document(DocumentDTO dto)
        {
            content = new Content(dto.Content);

            name = dto.Name;
            path = dto.Path;
            type = dto.Type;
            view = dto.View;
            id = dto.ID;
        }

        public event EventHandler<string>? NameChanged;

        public IContent Content => content;

        public string FullName => Path + '/' + Name;

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

        public string Type => type;

        public string View => view;
    }
}
