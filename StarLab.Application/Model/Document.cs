using StarLab.Application.DataTransfer;

namespace StarLab.Application.Model
{
    public class Document : IDocument
    {
        public Document(DocumentDTO dto)
        {
            Content = new Content(dto.Content);
            Name = dto.Name;
            Path = dto.Path;
            Type = dto.Type;
            Type = dto.View;
        }

        public IContent? Content { get; }

        public string Name { get; }

        public string Path { get; }

        public string Type { get; }

        public string View { get; }
    }
}
