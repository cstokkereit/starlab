namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A data transfer object that represents a workspace"/>.
    /// </summary>
    public class WorkspaceDTO
    {
        public string ActiveDocument = string.Empty;

        public string FileName = string.Empty;

        public List<ProjectDTO> Projects = new List<ProjectDTO>();

        public string Layout = string.Empty;

        public string SelectedFolder = string.Empty;
    }
}
