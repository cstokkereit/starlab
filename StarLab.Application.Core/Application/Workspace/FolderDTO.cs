namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A data transfer object that represents a folder in the workspace hierarchy.
    /// </summary>
    public class FolderDTO
    {
        public bool Expanded;

        public bool IsNew;

        public string? Path;
    }
}
