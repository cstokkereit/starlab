﻿using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    public interface IAddDocumentUseCase
    {
        void Execute(WorkspaceDTO dto, string path);
    }
}