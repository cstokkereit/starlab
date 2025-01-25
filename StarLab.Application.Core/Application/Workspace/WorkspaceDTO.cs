namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A data transfer object that represents a workspace"/>.
    /// </summary>
    public class WorkspaceDTO
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="WorkspaceDTO"/> class.
        /// </summary>
        public WorkspaceDTO()
        {
            Projects = new List<ProjectDTO>();
        }

        public string? ActiveDocument;

        public string? FileName;

        public List<ProjectDTO> Projects;

        public string? Layout;
    }
}
