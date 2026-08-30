using StarLab.Application.Workspace.Documents.Charts;
using StarLab.Application.Workspace.Documents.Tables;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// A data transfer object that represents a document in the workspace hierarchy.
    /// </summary>
    public class DocumentDTO
    {
        public ChartDTO? Chart = null;

        public string ID = string.Empty;

        public string Name = string.Empty;

        public string Path = string.Empty;

        public TableDTO? Table = null;

        public string Type = string.Empty;

        public string View = string.Empty;
    }
}
