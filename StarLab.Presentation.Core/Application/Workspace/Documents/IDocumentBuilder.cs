namespace StarLab.Application.Workspace.Documents
{
    public interface IDocumentBuilder
    {   
        IDocumentBuilder CreateDocument(string name, string path);

        IDocument GetDocument();
    }
}
