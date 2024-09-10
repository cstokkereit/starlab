namespace StarLab.UI.Controls
{
    public abstract class MenuStateManager
    {
        private ContextMenuStrip? menuStrip;

        public void Update(TreeNode node)
        {
            if (menuStrip != null && IsTargetNode(node))
            {
                menuStrip.Clear();

                CreateMenu(menuStrip, node);
            }
        }

        internal void AttachContextMenuStrip(ContextMenuStrip? menuStrip)
        {
            this.menuStrip = menuStrip;
        }

        protected abstract void CreateMenu(ContextMenuStrip menuStrip, TreeNode node);

        protected abstract bool IsTargetNode(TreeNode node);
    }
}
