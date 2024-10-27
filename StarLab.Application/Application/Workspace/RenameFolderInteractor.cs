using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    internal class RenameFolderInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IRenameItemUseCase
    {
        public RenameFolderInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        public void Execute(WorkspaceDTO dto, string path, string name)
        {
            if (IsValid(name))
            {
                var workspace = new Workspace(dto);
                var folder = workspace.GetFolder(path);

                var folders = folder.Parent != null ? folder.Parent.Folders : workspace.Folders;

                if (IsValid(folders, name))
                {
                    workspace.RenameFolder(folder, name);
                    Mapper.Map(workspace.Documents, dto.Documents);
                    Mapper.Map(workspace.Folders, dto.Folders);
                    OutputPort.UpdateFolders(dto);
                }
                else
                {
                    throw CreateException(path.Substring(path.LastIndexOf('/') + 1), name);
                }
            }
            else
            {
                throw CreateException();
            }
        }

        private Exception CreateException(string oldName, string newName)
        {
            return new Exception(string.Format(Resources.CannotRenameBecauseNameAlreadyExists, oldName, newName, Resources.Folder.ToLower()));
        }

        private Exception CreateException()
        {
            return new Exception(string.Format(Resources.NameContainsIllegalCharacters, Resources.Folder, string.Join(' ', Constants.IllegalCharacters)));
        }

        private bool IsValid(IEnumerable<Folder> folders, string name)
        {
            var valid = true;

            foreach (var folder in folders)
            {
                if (folder.Name == name)
                {
                    valid = false;
                    break;
                }
            }

            return valid;
        }

        private bool IsValid(string name)
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
    }
}
