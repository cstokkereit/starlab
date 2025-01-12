using StarLab.Commands;

namespace StarLab.Application
{
    /// <summary>
    /// Extends the <see cref="System.Windows.Forms.ContextMenuStrip"/> control.
    /// </summary>
    public class ContextMenuStrip : System.Windows.Forms.ContextMenuStrip, IMenuManager
    {
        private Dictionary<string, ToolStripMenuItem> menuItems = new Dictionary<string, ToolStripMenuItem>(); // A dictionary containing the menu items indexed by name.

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        public void AddMenuItem(string name, string text)
        {
            AddItem(name, text);
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        public void AddMenuItem(string parent, string name, string text)
        {
            AddItem(parent, name, text);
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="image">The menu item <see cref="Image"/>.</param>
        public void AddMenuItem(string name, string text, Image image)
        {
            var item = AddItem(name, text);

            if (item != null) item.Image = image;
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="command">The <see cref="ICommand"/> that will be invoked when the menu item is clicked.</param>
        public void AddMenuItem(string name, string text, ICommand command)
        {
            var item = AddItem(name, text);

            if (item != null && command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(item);
            }
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="image">The menu item <see cref="Image"/>.</param>
        public void AddMenuItem(string parent, string name, string text, Image image)
        {
            var item = AddItem(parent, name, text);

            if (item != null) item.Image = image;
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="command">The <see cref="ICommand"/> that will be invoked when the menu item is clicked.</param>
        public void AddMenuItem(string parent, string name, string text, ICommand command)
        {
            var item = AddItem(parent, name, text);

            if (item != null && command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(item);
            }
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="image">The menu item <see cref="Image"/>.</param>
        /// <param name="command">The <see cref="ICommand"/> that will be invoked when the menu item is clicked.</param>
        public void AddMenuItem(string name, string text, Image image, ICommand command)
        {
            var item = AddItem(name, text);

            if (item != null && command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(item);
                item.Image = image;
            }
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="image">The menu item <see cref="Image"/>.</param>
        /// <param name="command">The <see cref="ICommand"/> that will be invoked when the menu item is clicked.</param>
        public void AddMenuItem(string parent, string name, string text, Image image, ICommand command)
        {
            var item = AddItem(parent, name, text);

            if (item != null && command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(item);
                item.Image = image;
            }
        }

        /// <summary>
        /// Adds a separator to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        public void AddMenuSeparator(string parent)
        {
            if (menuItems.ContainsKey(parent))
            {
                var parentItem = menuItems[parent];

                parentItem.DropDownItems.Add(new ToolStripSeparator());
            }
        }

        /// <summary>
        /// Adds a separator to the menu.
        /// </summary>
        public void AddMenuSeparator()
        {
            Items.Add(new ToolStripSeparator());
        }

        /// <summary>
        /// Removes all menu items from the menu.
        /// </summary>
        public void Clear()
        {
            menuItems.Clear();
            Items.Clear();
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        private ToolStripMenuItem? AddItem(string name, string text)
        {
            ToolStripMenuItem item = null;

            if (!menuItems.ContainsKey(name))
            {
                item = new ToolStripMenuItem(text)
                {
                    Name = name
                };

                menuItems.Add(name, item);
                Items.Add(item);
            }

            return item;
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        private ToolStripMenuItem? AddItem(string parent, string name, string text)
        {
            ToolStripMenuItem item = null;

            if (menuItems.ContainsKey(parent) && !menuItems.ContainsKey(name))
            {
                item = new ToolStripMenuItem(text)
                {
                    Name = name
                };

                var parentItem = menuItems[parent];
                parentItem.DropDownItems.Add(item);
                menuItems.Add(name, item);
            }

            return item;
        }
    }
}
