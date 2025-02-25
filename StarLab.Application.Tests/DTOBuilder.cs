using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;

namespace StarLab
{
    internal class DTOBuilder
    {
        private WorkspaceDTO workspace;

        public DTOBuilder(string filename)
        {
            workspace = new WorkspaceDTO
            {
                FileName = filename
            };
        }

        public DTOBuilder AddDocument(string id, string name, string path)
        {
            var document = new DocumentDTO
            {
                ID = id,
                Name = name,
                Path = path,
                View = "view"
            };

            var project = GetProject(path);

            project?.Documents.Add(document);

            return this;
        }

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

        public DTOBuilder AddProject(string name)
        {
            var project = new ProjectDTO
            {
                Name = name
            };

            workspace.Projects.Add(project);

            return this;
        }

        public WorkspaceDTO CreateWworkspace()
        {
            return workspace;
        }

        private ProjectDTO? GetProject(string path)
        {
            foreach (var project in workspace.Projects)
            {
                if (path.StartsWith($"Workspace/{project.Name}")) return project;
            }

            return null;
        }
    }
}
