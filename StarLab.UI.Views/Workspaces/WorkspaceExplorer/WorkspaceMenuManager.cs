using StarLab.Presentation;
using StarLab.UI.Controls;

namespace StarLab.UI.Workspaces.WorkspaceExplorer
{
    internal class WorkspaceMenuManager : MenuStateManager
    {
        protected override bool IsTargetNode(TreeNode node)
        {
            return (string)node.Tag == Constants.WORKSPACE;
        }
    }
}
