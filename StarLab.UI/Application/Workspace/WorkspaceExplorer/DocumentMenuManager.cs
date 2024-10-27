namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    internal class DocumentMenuManager : MenuStateManager, IMenuManager
    {
        public DocumentMenuManager(string name)
            : base(name) { }

        protected override bool IsTargetNode(TreeNode node)
        {
            if (node != null)
                return (string)node.Tag == Constants.DOCUMENT & node.Name == Name;

            return false;
        }
    }
}
