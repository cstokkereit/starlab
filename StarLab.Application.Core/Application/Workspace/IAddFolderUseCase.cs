﻿namespace StarLab.Application.Workspace
{
    public interface IAddFolderUseCase
    {
        void Execute(WorkspaceDTO dto, string key);
    }
}