using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Tests
{
    /// <summary>
    /// A helper class that uses the Builder Pattern to construct the <see cref="WorkspaceDTO"/>s used in unit tests.
    /// </summary>
    public class WorkspaceDtoBuilder
    {
        private WorkspaceDTO workspace; // The DTO being constructed.

        private string workspaceName; // The workspace name.

        /// <summary>
        /// Initialises a new instance of the <see cref="WorkspaceDtoBuilder"/> class.
        /// </summary>
        /// <param name="filename">The workspace filename.</param>
        public WorkspaceDtoBuilder(string filename)
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
        /// Adds the <see cref="ChartDTO"/> provided to the specified document.
        /// </summary>
        /// <param name="id">The document ID.</param>
        /// <param name="chart">The <see cref="ChartDTO"/> that defines the chart.</param>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="WorkspaceDTO"/>.</returns>
        public WorkspaceDtoBuilder AddChart(string id, ChartDTO chart)
        {
            var document = GetDocument(id);

            if (document != null)
            {
                document.Chart = chart;
            }

            return this;
        }

        /// <summary>
        /// Adds a <see cref="DocumentDTO"/> to the <see cref="WorkspaceDTO"/>.
        /// </summary>
        /// <param name="id">The docmument ID.</param>
        /// <param name="view">The docmument view.</param>
        /// <param name="name">The docmument name.</param>
        /// <param name="path">The docmument path.</param>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="WorkspaceDTO"/>.</returns>
        public WorkspaceDtoBuilder AddDocument(string id, string view, string name, string path)
        {
            var document = new DocumentDTO
            {
                ID = id,
                Name = name,
                Path = path,
                View = view
            };

            var project = GetProject(path);

            project?.Documents.Add(document);

            return this;
        }

        /// <summary>
        /// Adds a <see cref="DocumentDTO"/> to the <see cref="WorkspaceDTO"/>.
        /// </summary>
        /// <param name="id">The docmument ID.</param>
        /// <param name="name">The docmument name.</param>
        /// <param name="path">The docmument path.</param>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="WorkspaceDTO"/>.</returns>
        public WorkspaceDtoBuilder AddDocument(string id, string name, string path)
        {
            return AddDocument(id, string.Empty, name, path);
        }

        /// <summary>
        /// Adds a <see cref="FolderDTO"/> to the <see cref="WorkspaceDTO"/>.
        /// </summary>
        /// <param name="path">The folder path.</param>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="WorkspaceDTO"/>.</returns>
        public WorkspaceDtoBuilder AddFolder(string path)
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
        public WorkspaceDtoBuilder AddProject(string name)
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
        public WorkspaceDTO CreateWorkspace()
        {
            return workspace;
        }

        /// <summary>
        /// Gets the <see cref="DocumentDTO"/> with the specified document ID.
        /// </summary>
        /// <param name="id">The document ID.</param>
        /// <returns>The required <see cref="DocumentDTO"/>.</returns>
        private DocumentDTO? GetDocument(string id)
        {
            foreach (var project in workspace.Projects)
            {
                foreach (var document in project.Documents)
                {
                    if (document.ID == id) return document;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the <see cref="ProjectDTO"/> with the specified path.
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
