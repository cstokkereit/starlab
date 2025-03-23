using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A helper class that uses the Builder Pattern to construct the <see cref="WorkspaceDTO"/>s used for unit testing.
    /// </summary>
    internal class DTOBuilder
    {
        private WorkspaceDTO workspace; // The DTO being constructed.

        private string workspaceName; // The workspace name.

        /// <summary>
        /// Initialises a new instance of the <see cref="DTOBuilder"/> class.
        /// </summary>
        /// <param name="filename">The workspace filename.</param>
        public DTOBuilder(string filename)
        {
            workspace = new WorkspaceDTO
            {
                FileName = filename
            };

            workspaceName = workspace.FileName;

            if (!string.IsNullOrEmpty(workspace.FileName) && workspace.FileName.Contains("\\"))
            {
                workspaceName = workspace.FileName.Substring(workspace.FileName.LastIndexOf("\\") + 1);
            }

            if (workspaceName.Contains('.'))
            {
                workspaceName = workspaceName.Substring(0, workspaceName.IndexOf('.'));
            }
        }

        /// <summary>
        /// Adds a <see cref="DocumentDTO"/> to the <see cref="WorkspaceDTO"/>.
        /// </summary>
        /// <param name="id">The docmument ID.</param>
        /// <param name="name">The docmument name.</param>
        /// <param name="path">The docmument path.</param>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="WorkspaceDTO"/>.</returns>
        public DTOBuilder AddDocument(string id, string name, string path)
        {
            var document = new DocumentDTO
            {
                ID = id,
                Name = name,
                Path = path,
                View = string.Empty
            };

            var project = GetProject(path);

            project?.Documents.Add(document);

            return this;
        }

        /// <summary>
        /// Adds a <see cref="FolderDTO"/> to the <see cref="WorkspaceDTO"/>.
        /// </summary>
        /// <param name="path">The folder path.</param>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="WorkspaceDTO"/>.</returns>
        public DTOBuilder AddFolder(string path)
        {
            var folder = new FolderDTO
            {
                Path = path
            };

            var project = GetProject(path);

            project?.Folders.Add(folder);

            return this;
        }

        /// <summary>
        /// Adds a <see cref="ProjectDTO"/> to the <see cref="WorkspaceDTO"/>.
        /// </summary>
        /// <param name="name">The project name.</param>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="WorkspaceDTO"/>.</returns>
        public DTOBuilder AddProject(string name)
        {
            var project = new ProjectDTO
            {
                Name = name
            };

            workspace.Projects.Add(project);

            return this;
        }

        /// <summary>
        /// Creates the <see cref="WorkspaceDTO"/>.
        /// </summary>
        /// <returns>The required <see cref="WorkspaceDTO"/>.</returns>
        public WorkspaceDTO CreateWworkspace()
        {
            return workspace;
        }

        /// <summary>
        /// Creates a <see cref="ProjectDTO"/>.
        /// </summary>
        /// <param name="path">The project path.</param>
        /// <returns>The required <see cref="ProjectDTO"/>.</returns>
        private ProjectDTO? GetProject(string path)
        {
            foreach (var project in workspace.Projects)
            {
                if (path.StartsWith($"{workspaceName}/{project.Name}")) return project;
            }

            return null;
        }
    }
}
