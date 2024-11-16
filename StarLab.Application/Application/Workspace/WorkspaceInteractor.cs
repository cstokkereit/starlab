using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    internal class WorkspaceInteractor : UseCaseInteractor<IWorkspaceOutputPort>
    {
        public WorkspaceInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        protected Exception CreateInvalidNameException(string name, string target)
        {
            var message = string.Empty;

            if (string.IsNullOrEmpty(name))
                message = string.Format(Resources.NameNullOrEmptyMessage, target.ToLower());
            else
                message = string.Format(Resources.NameCannotIncludeMessage, target.ToLower(), string.Join(' ', Constants.IllegalCharacters));

            return new Exception(message);
        }

        protected Exception CreateTargetExistsException(string oldName, string newName, string target)
        {
            return new Exception(string.Format(Resources.NameAlreadyExistsMessage, oldName, newName, target.ToLower()));
        }

        protected IEnumerable<IFolder> GetChildFolders(IFolder folder)
        {
            var folders = new List<IFolder>();

            foreach (var child in folder.Folders)
            {
                folders.Add(child);
                folders.AddRange(GetChildFolders(child));
            }

            return folders;
        }

        protected bool IsValid(string name)
        {
            var valid = !string.IsNullOrEmpty(name);

            if (valid)
            {
                foreach (var character in Constants.IllegalCharacters)
                {
                    if (name.Contains(character))
                    {
                        valid = false;
                        break;
                    }
                }
            }

            return valid;
        }

        protected void UpdateWorkspace(Workspace workspace, IEnumerable<ProjectDTO> dtos)
        {
            foreach (var dto in dtos)
            {
                if (!string.IsNullOrEmpty(dto.Name))
                {
                    var project = workspace.GetProject(dto.Name);
                    Mapper.Map(GetChildFolders(project), dto.Folders);
                }
            }
        }
    }
}
