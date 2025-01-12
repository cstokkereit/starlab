namespace StarLab.Application
{
    /// <summary>
    /// Generates the context menu for a <see cref="TreeView"/> control.
    /// </summary>
    public class ContextMenuManager
    {
        private IDictionary<string, NodeMenuManager> managers = new Dictionary<string, NodeMenuManager>(); // A dictionary containing the node menu managers indexed by name.

        private ContextMenuStrip? menu; // The context menu strip that is controlled by this context menu manager.

        /// <summary>
        /// Adds a <see cref="NodeMenuManager"/> that will generate the context menu for a specific tree view node.
        /// </summary>
        /// <param name="manager">The <see cref="NodeMenuManager"/> to be added.</param>
        public void Add(NodeMenuManager manager)
        {
            if (!managers.ContainsKey(manager.Name))
            {
                manager.AttachContextMenuStrip(menu);
                managers.Add(manager.Name, manager);
            }
        }

        /// <summary>
        /// Removes the <see cref="NodeMenuManager"/> that generates the specified context menu.
        /// </summary>
        /// <param name="name">The name of the <see cref="ManualResetEvent=NodeMenuManager"/> to be removed.</param>
        public void Remove(string name)
        {
            if (managers.ContainsKey(name))
            {
                managers.Remove(name);
            }
        }

        /// <summary>
        /// Shows the context menu.
        /// </summary>
        public void ShowContextMenu()
        {
            menu?.Show();
        }

        /// <summary>
        /// Generates the context menu for the specified <see cref="TreeNode"/> node.
        /// </summary>
        /// <param name="node">The <see cref="TreeNode"/> for which a context menu is required.</param>
        public void Update(TreeNode node)
        {
            foreach (var manager in managers.Values)
            {
                manager.Update(node);
            }
        }

        /// <summary>
        /// Attaches the <see cref="ContextMenuStrip"/> provided.
        /// </summary>
        /// <param name="menu">The <see cref="ContextMenuStrip"/> that will be controlled by this instance.</param>
        internal void AttachContextMenuStrip(ContextMenuStrip? menu)
        {
            foreach (var manager in managers.Values)
            {
                manager.AttachContextMenuStrip(menu);
            }

            this.menu = menu;
        }
    }
}
