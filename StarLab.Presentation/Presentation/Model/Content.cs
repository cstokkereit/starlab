using StarLab.Application;
using StarLab.Application.DataTransfer;

namespace StarLab.Presentation.Model
{
    public class Content : IContent
    {
        private readonly List<IContent> contents = new List<IContent>();

        private readonly string name;

        private readonly SplitViewPanels panel;

        private readonly string view;

        public Content(string? view, string? name, SplitViewPanels panel)
        {
            this.name = name ?? throw new ArgumentNullException();
            this.view = view ?? throw new ArgumentNullException();

            this.panel = panel;
        }

        public Content(string view)
        {
            this.view = view ?? throw new ArgumentNullException();
        }

        public Content(ContentDTO dto)
            : this(dto.View, dto.Name, (SplitViewPanels)dto.Panel)
        {
            if (dto.Contents != null)
            {
                CreateContents(dto.Contents);
            }
        }

        public IReadOnlyList<IContent> Contents => contents;

        public string Name => name;

        public SplitViewPanels Panel => panel;

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
