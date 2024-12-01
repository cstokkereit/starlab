namespace StarLab.Application.Workspace.Documents
{
    public class DocumentBuilder : IDocumentBuilder
    {
        private Document? document;

        public IDocumentBuilder CreateDocument(string name, string path)
        {
            document = new Document(Guid.NewGuid().ToString(), name, path, "StarLab.Application.Workspace.Documents.DocumentView, StarLab.UI");

            return this;
        }

        public IDocumentBuilder AddContent(string view, string name, SplitViewPanels panel)
        {
            if (document == null) throw new InvalidOperationException(); // TODO 

            document.AddContent(new Content(view, name, panel));

            return this;
        }

        public IDocument GetDocument()
        {
            if (document is null) throw new InvalidOperationException(); // TODO

            var temp = document;
            document = null;
            return temp;
        }
    }
}
