
using StarLab.Application.DataTransfer;

namespace StarLab.Application.Model
{
    public class Content : IContent
    {
        private readonly List<IContent> contents = new List<IContent>();

        public Content(ContentDTO dto) // May not be needed
        {
            CreateContents(dto.Contents);

            Name = dto.Name;
            View = dto.View;
        }

        public List<IContent> Contents => contents;

        public string Name { get; }

        public string View { get; }

        private void CreateContents(IEnumerable<ContentDTO> dtos)
        {
            foreach (var dto in dtos)
            {
                contents.Add(new Content(dto));
            }
        }
    }
}
