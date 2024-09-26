using StarLab.Application.DataTransfer;

namespace StarLab.Application.Model
{
    public class Document : IDocument
    {
        public Document(DocumentDTO dto)
        {
            Name = dto.Name;
            Path = dto.Path;
            ID = dto.ID;
        }

        public string ID { get; }

        public string Name { get; set; }

        public string Path { get; }
    }
}
