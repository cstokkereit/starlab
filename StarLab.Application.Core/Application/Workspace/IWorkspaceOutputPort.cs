﻿using StarLab.Application.DataTransfer;

namespace StarLab.Application.Workspace
{
    public interface IWorkspaceOutputPort : IOutputPort
    {
        void UpdateDocument(DocumentDTO dto);

        void UpdateFolders(WorkspaceDTO dto);

        void UpdateWorkspace(WorkspaceDTO dto);
    }
}
