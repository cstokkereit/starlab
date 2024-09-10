using StarLab.Presentation;
using StarLab.UI.Controls;

namespace StarLab.UI.Workspaces.WorkspaceExplorer
{
    internal class FolderMenuManager : MenuStateManager
    {
        protected override bool IsTargetNode(TreeNode node)
        {
            return (string)node.Tag == Constants.FOLDER;
        }
    }
}
