using StarLab.Application.DataTransfer;

namespace StarLab.Presentation.Model
{
    public class Document : IDocument
    {
        private readonly Content content;

        private readonly string name;

        private readonly string path;

        private readonly string type;

        private readonly string view;

        public Document(DocumentDTO dto) 
        {
            content = new Content(dto.Content);

            name = dto.Name;
            path = dto.Path;
            type = dto.Type;
            view = dto.View;
        }

        public IContent Content => content;

        public string FullName => Path + '/' + Name;

        public string Name => name;

        public string Path => path;

        public string Type => type;

        public string View => view;
    }
}
