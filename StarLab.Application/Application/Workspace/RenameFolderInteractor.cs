using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    internal class RenameFolderInteractor : WorkspaceInteractor, IRenameItemUseCase
    {
        public RenameFolderInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        public void Execute(WorkspaceDTO dto, string path, string name)
        {
            if (IsValid(name))
            {
                var workspace = new Workspace(dto);
                var folder = workspace.GetFolder(path);
                var folders = folder.Parent.Folders;

                if (IsValid(folders, name))
                {
                    workspace.RenameFolder(folder, name);
                    UpdateWorkspace(workspace, dto.Projects);
                    OutputPort.UpdateFolders(dto);
                }
                else
                {
                    throw CreateTargetExistsException(path.Substring(path.LastIndexOf('/') + 1), name, Resources.Folder);
                }
            }
            else
            {
                throw CreateInvalidNameException(name, Resources.Folder);
            }
        }

        private bool IsValid(IEnumerable<IFolder> folders, string name)
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
    }
}
