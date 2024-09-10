using StarLab.Commands;

namespace StarLab.UI.Controls
{
    public class MenuStrip : System.Windows.Forms.MenuStrip
    {
        #region Member Variables

        private Dictionary<string, ToolStripMenuItem> menuItems = new Dictionary<string, ToolStripMenuItem>(); // A dictionary containing the menu items indexed by name.

        #endregion

        #region IMenuView Members

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
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="image">The menu item image.</param>
        public void AddMenuItem(string name, string text, Image image)
        {
            var item = AddItem(name, text);

            if (item != null)
            {
                item.Image = image;
            }
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
        /// <param name="parent">The name of the parent menu item.</param>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="image">The menu item image.</param>
        public void AddMenuItem(string parent, string name, string text, Image image)
        {
            var item = AddItem(parent, name, text);

            if (item != null)
            {
                item.Image = image;
            }
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="command">The command to invoke when the menu item is clicked.</param>
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
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="image">The menu item image.</param>
        /// <param name="command">The command to invoke when the menu item is clicked.</param>
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
        /// <param name="command">The command to invoke when the menu item is clicked.</param>
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
        /// <param name="parent">The name of the parent menu item.</param>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="image">The menu item image.</param>
        /// <param name="command">The command to invoke when the menu item is clicked.</param>
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
        public void AddSeparator(string parent)
        {
            if (menuItems.ContainsKey(parent))
            {
                ToolStripMenuItem parentItem = menuItems[parent];
                parentItem.DropDownItems.Add(new ToolStripSeparator());
            }
        }

        /// <summary>
        /// Adds a separator to the menu.
        /// </summary>
        public void AddSeparator()
        {
            // Do Nothing
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        private ToolStripMenuItem AddItem(string name, string text)
        {
            ToolStripMenuItem? item = null;

            if (!menuItems.ContainsKey(name))
            {
                item = new ToolStripMenuItem(text)
                {
                    Name = name
                };

                menuItems.Add(name, item);
                Items.Add(item);
            }

            if(item == null)
            {
                throw new Exception();
            }

            return item;
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        private ToolStripMenuItem AddItem(string parent, string name, string text)
        {
            ToolStripMenuItem? item = null;

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

            if (item == null)
            {
                throw new Exception();
            }

            return item;
        }

        #endregion
    }
}
