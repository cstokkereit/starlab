namespace StarLab.Application
{
    /// <summary>
    /// TODO
    /// </summary>
    public class ContextMenuManager
    {
        private IDictionary<string, NodeMenuManager> managers = new Dictionary<string, NodeMenuManager>();

        private ContextMenuStrip? menuStrip;

        public void Add(NodeMenuManager manager)
        {
            if (!managers.ContainsKey(manager.Name))
            {
                manager.AttachContextMenuStrip(menuStrip);
                managers.Add(manager.Name, manager);
            }
        }

        public void Remove(string name)
        {
            if (managers.ContainsKey(name))
            {
                managers.Remove(name);
            }
        }

        public void ShowContextMenu()
        {
            menuStrip.Show();
        }

        public void Update(TreeNode node)
        {
            foreach (var manager in managers.Values)
            {
                manager.Update(node);
            }
        }

        internal void AttachContextMenuStrip(ContextMenuStrip? menuStrip)
        {
            foreach (var manager in managers.Values)
            {
                manager.AttachContextMenuStrip(menuStrip);
            }

            this.menuStrip = menuStrip;
        }

        internal void Clear()
        {
            managers.Clear();
        }
    }
}
