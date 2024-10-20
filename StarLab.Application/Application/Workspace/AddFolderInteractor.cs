﻿using AutoMapper;
using StarLab.Application.DataTransfer;
using StarLab.Application.Model;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    internal class AddFolderInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IAddFolderUseCase
    {
        public AddFolderInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        public void Execute(WorkspaceDTO dto, string key)
        {
            var workspace = new Model.Workspace(dto);

            var parent = workspace.GetFolder(key);
            var name = GetName(parent);

            var folder = new Folder(name, parent);
            workspace.AddFolder(folder);

            Mapper.Map(workspace.Folders, dto.Folders);

            OutputPort.UpdateFolders(dto);
        }

        private string GetName(IFolder parent)
        {
            var name = Resources.DefaultFolderName;
            var names = GetNames(parent.Folders);
            var n = 1;

            while (names.Contains(name))
            {
                name = Resources.DefaultFolderName + n++;
            }

            return name;
        }

        private List<string> GetNames(IEnumerable<IFolder> folders)
        {
            var names = new List<string>();

            foreach (var folder in folders)
            {
                names.Add(folder.Name);
            }

            return names;
        }
    }
}
