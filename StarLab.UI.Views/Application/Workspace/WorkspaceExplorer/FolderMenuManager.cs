﻿namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    internal class FolderMenuManager : MenuStateManager, IMenuManager
    {
        public FolderMenuManager(string name)
            : base(name) { }

        protected override bool IsTargetNode(TreeNode node)
        {
            return (string)node.Tag == Constants.FOLDER & node.Name == Name;
        }
    }
}
