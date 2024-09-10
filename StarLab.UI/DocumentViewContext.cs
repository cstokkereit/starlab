using StarLab.Presentation;
using StarLab.Presentation.Model;

namespace StarLab.UI
{
    public class DocumentViewContext : IViewContext
    {
        private readonly IContent content;

        private readonly string name;

        private readonly string text;

        private readonly string path;

        public DocumentViewContext(IDocument document)
        {
            content = document.Content;
            name = document.Name;
            path = document.Path;
            text = name; // May need to change
        }

        public IContent Content => content;

        public string DefaultLocation => Constants.DOCUMENT;

        public string FullName => name + '/' + Path;

        public string Name => name;

        public string Path => path;

        public string Text => text;

        public string View => Views.DOCUMENT;
    }
}
