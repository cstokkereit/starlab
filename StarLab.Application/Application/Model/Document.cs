using StarLab.Application.DataTransfer;

namespace StarLab.Application.Model
{
    public class Document : IDocument
    {
        private readonly List<IContent> contents = new List<IContent>();

        private readonly string view;

        private readonly string id;

        public Document(DocumentDTO dto)
        {
            CreateContents(dto.Contents);

            Name = dto.Name ?? throw new ArgumentException();
            Path = dto.Path ?? throw new ArgumentException();

            view = dto.View ?? throw new ArgumentException();
            id = dto.ID ?? throw new ArgumentException();
        }

        public IEnumerable<IContent> Contents => contents;

        public string ID => id;

        public string Name { get; set; }

        public string Path { get; set; }

        public string View => view;

        private void CreateContents(IEnumerable<ContentDTO> dtos)
        {
            foreach (var dto in dtos)
            {
                contents.Add(new Content(dto));
            }
        }
    }
}
