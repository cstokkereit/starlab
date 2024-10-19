using StarLab.Application.DataTransfer;

namespace StarLab.Presentation.Model
{
    public class Document : IDocument
    {
        private readonly List<IContent> contents = new List<IContent>();

        private readonly string path;

        private readonly string view;

        private readonly string id;

        private string name;

        public Document(DocumentDTO dto)
        {
            name = dto.Name;
            path = dto.Path;
            view = dto.View;
            id = dto.ID;

            if (dto.Contents != null) CreateContents(dto.Contents);
        }

        public event EventHandler<string>? NameChanged;

        public IEnumerable<IContent> Contents => contents;

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

        public string View => view;

        private void CreateContents(IEnumerable<ContentDTO> dtos)
        {
            foreach (var dto in dtos)
            {
                var content = new Content(dto);
                contents.Add(content);
            }
        }
    }
}
