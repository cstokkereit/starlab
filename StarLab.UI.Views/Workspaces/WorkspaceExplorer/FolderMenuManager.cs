using StarLab.Presentation;
using StarLab.UI.Controls;

namespace StarLab.UI.Workspaces.WorkspaceExplorer
{
    internal class FolderMenuManager : MenuStateManager, IMenuManager
    {
        protected override bool IsTargetNode(TreeNode node)
        {
            return (string)node.Tag == Constants.FOLDER;
        }
    }
}
