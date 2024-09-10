namespace StarLab.UI.Controls
{
    public class ContextMenuManager
    {
        private List<MenuStateManager> managers = new List<MenuStateManager>();

        private ContextMenuStrip? menuStrip;

        public void Add(MenuStateManager manager)
        {
            if (!managers.Contains(manager))
            {
                manager.AttachContextMenuStrip(menuStrip);
                managers.Add(manager);
            }
        }

        public void Update(TreeNode node)
        {
            foreach (var manager in managers)
            {
                manager.Update(node);
            }
        }

        internal void AttachContextMenuStrip(ContextMenuStrip? menuStrip)
        {
            foreach (var manager in managers)
            {
                manager.AttachContextMenuStrip(menuStrip);
            }

            this.menuStrip = menuStrip;
        }
    }
}
