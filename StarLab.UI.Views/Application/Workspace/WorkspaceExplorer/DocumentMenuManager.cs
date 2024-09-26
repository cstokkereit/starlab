namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    internal class DocumentMenuManager : MenuStateManager, IMenuManager
    {
        //private readonly string document;

        public DocumentMenuManager(string document)
            : base(document) { }

        protected override bool IsTargetNode(TreeNode node)
        {
            return (string)node.Tag == Constants.DOCUMENT & node.Name == Name;
        }
    }
}
