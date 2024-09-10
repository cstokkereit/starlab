using StarLab.Presentation;
using StarLab.UI.Controls;

namespace StarLab.UI.Workspaces.WorkspaceExplorer
{
    internal class WorkspaceMenuManager : MenuStateManager
    {
        protected override void CreateMenu(Controls.ContextMenuStrip menuStrip, TreeNode node)
        {
            menuStrip.AddMenuItem("Winnie", "Winnie");
        }

        protected override bool IsTargetNode(TreeNode node)
        {
            return (string)node.Tag == Constants.WORKSPACE;
        }
    }
}
