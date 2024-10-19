using StarLab.Application.DataTransfer;

namespace StarLab.Application.Model
{
    public class Content : IContent
    {
        private readonly string name;

        private readonly int panel;

        private readonly string view;

        public Content(ContentDTO dto)
        {
            name = string.Empty + dto.Name;
            view = string.Empty + dto.View;

            panel = dto.Panel;
        }

        public string Name => name;

        public int Panel => panel;

        public string View => view;
    }
}
