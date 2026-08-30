namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A data transfer object that represents the database that provides the data for a project in the workspace hierarchy.
    /// </summary>
    public class DatabaseDTO
    {
        public string Host = string.Empty;

        public string Name = string.Empty;

        public int Port;
    }
}
