using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// A data transfer object that represents a document in the workspace hierarchy.
    /// </summary>
    public class DocumentDTO
    {
        public ChartDTO? Chart;

        public string? ID;

        public string? Name;

        public string? Path;

        public string? View;
    }
}
