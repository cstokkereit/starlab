﻿namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    internal class WorkspaceMenuManager : MenuStateManager, IMenuManager
    {
        public WorkspaceMenuManager(string name)
            : base(name) { }

        protected override bool IsTargetNode(TreeNode node)
        {
            return (string)node.Tag == Constants.WORKSPACE;
        }
    }
}
