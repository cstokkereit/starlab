using StarLab.Presentation;
using Stratosoft.Commands;

namespace StarLab.UI.Controls
{
    /// <summary>
    /// TODO
    /// </summary>
    public class NodeMenuManager : IMenuManager
    {
        private readonly List<AddMenuItemCommand> commands = new List<AddMenuItemCommand>(); //

        private readonly string target; //

        private readonly string name; //

        private ContextMenuStrip? menuStrip; //

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="target"></param>
        public NodeMenuManager(string name, string target)
        {
            this.target = target;
            this.name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        public NodeMenuManager(string target)
            : this(target, target) { }

        /// <summary>
        /// 
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        public void AddMenuItem(string name, string text)
        {
            if (menuStrip != null) commands.Add(new AddTopLevelMenuItemCommand(menuStrip, name, text));
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        public void AddMenuItem(string parent, string name, string text)
        {
            if (menuStrip != null) commands.Add(new AddChildMenuItemCommand(menuStrip, parent, name, text));
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="image">The menu item <see cref="Image"/>.</param>
        public void AddMenuItem(string name, string text, Image image)
        {
            if (menuStrip != null) commands.Add(new AddTopLevelMenuItemCommand(menuStrip, name, text, image));
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="command">The <see cref="ICommand"/> that will be invoked when the menu item is clicked.</param>
        public void AddMenuItem(string name, string text, ICommand command)
        {
            if (menuStrip != null) commands.Add(new AddTopLevelMenuItemCommand(menuStrip, name, text, command));
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
            if (menuStrip != null) commands.Add(new AddChildMenuItemCommand(menuStrip, parent, name, text, image));
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
            if (menuStrip != null) commands.Add(new AddChildMenuItemCommand(menuStrip, parent, name, text, command));
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
            if (menuStrip != null) commands.Add(new AddTopLevelMenuItemCommand(menuStrip, name, text, image, command));
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
            if (menuStrip != null) commands.Add(new AddChildMenuItemCommand(menuStrip, parent, name, text, image, command));
        }

        /// <summary>
        /// Adds a separator to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        public void AddMenuSeparator(string parent)
        {
            if (menuStrip != null) commands.Add(new AddSeparatorCommand(menuStrip, parent));
        }

        /// <summary>
        /// Adds a separator to the menu.
        /// </summary>
        public void AddMenuSeparator()
        {
            if (menuStrip != null) commands.Add(new AddSeparatorCommand(menuStrip));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public virtual void Update(TreeNode node)
        {
            if (menuStrip != null && IsTargetNode(node))
            {
                menuStrip.Clear();

                foreach (var command in commands)
                {
                    command.Execute();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuStrip"></param>
        internal void AttachContextMenuStrip(ContextMenuStrip? menuStrip)
        {
            this.menuStrip = menuStrip;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool IsTargetNode(TreeNode node)
        {
            if (node != null)
                return (string)node.Tag == target && node.Name == Name;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        private abstract class AddMenuItemCommand
        {
            protected ContextMenuStrip menuStrip;
            protected ICommand? command;
            protected string? parent;
            protected Image? image;
            protected string? name;
            protected string? text;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="menuStrip"></param>
            public AddMenuItemCommand(ContextMenuStrip menuStrip)
            {
                this.menuStrip = menuStrip;
            }

            /// <summary>
            /// 
            /// </summary>
            public abstract void Execute();
        }

        /// <summary>
        /// 
        /// </summary>
        private class AddTopLevelMenuItemCommand : AddMenuItemCommand
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="menuStrip"></param>
            /// <param name="name"></param>
            /// <param name="text"></param>
            /// <param name="image"></param>
            /// <param name="command"></param>
            public AddTopLevelMenuItemCommand(ContextMenuStrip menuStrip, string name, string text, Image? image, ICommand? command)
                : base(menuStrip)
            {
                this.command = command;
                this.image = image;
                this.name = name;
                this.text = text;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="menuStrip"></param>
            /// <param name="name"></param>
            /// <param name="text"></param>
            /// <param name="command"></param>
            public AddTopLevelMenuItemCommand(ContextMenuStrip menuStrip, string name, string text, ICommand? command)
                : this(menuStrip, name, text, null, command) { }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="menuStrip"></param>
            /// <param name="name"></param>
            /// <param name="text"></param>
            /// <param name="image"></param>
            public AddTopLevelMenuItemCommand(ContextMenuStrip menuStrip, string name, string text, Image? image)
                : this(menuStrip, name, text, image, null) { }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="menuStrip"></param>
            /// <param name="name"></param>
            /// <param name="text"></param>
            public AddTopLevelMenuItemCommand(ContextMenuStrip menuStrip, string name, string text)
                : this(menuStrip, name, text, null, null) { }

            /// <summary>
            /// 
            /// </summary>
            public override void Execute()
            {
                if (name != null && text != null)
                {
                    if (command != null)
                    {
                        if (image != null)
                            menuStrip.AddMenuItem(name, text, image, command);
                        else
                            menuStrip.AddMenuItem(name, text, command);
                    }
                    else
                    {
                        if (image != null)
                            menuStrip.AddMenuItem(name, text, image);
                        else
                            menuStrip.AddMenuItem(name, text);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private class AddChildMenuItemCommand : AddMenuItemCommand
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="menuStrip"></param>
            /// <param name="parent"></param>
            /// <param name="name"></param>
            /// <param name="text"></param>
            /// <param name="image"></param>
            /// <param name="command"></param>
            public AddChildMenuItemCommand(ContextMenuStrip menuStrip, string parent, string name, string text, Image? image, ICommand? command)
                : base(menuStrip)
            {
                this.command = command;
                this.parent = parent;
                this.image = image;
                this.name = name;
                this.text = text;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="menuStrip"></param>
            /// <param name="parent"></param>
            /// <param name="name"></param>
            /// <param name="text"></param>
            /// <param name="command"></param>
            public AddChildMenuItemCommand(ContextMenuStrip menuStrip, string parent, string name, string text, ICommand? command)
                : this(menuStrip, parent, name, text, null, command) { }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="menuStrip"></param>
            /// <param name="parent"></param>
            /// <param name="name"></param>
            /// <param name="text"></param>
            /// <param name="image"></param>
            public AddChildMenuItemCommand(ContextMenuStrip menuStrip, string parent, string name, string text, Image? image)
                : this(menuStrip, parent, name, text, image, null) { }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="menuStrip"></param>
            /// <param name="parent"></param>
            /// <param name="name"></param>
            /// <param name="text"></param>
            public AddChildMenuItemCommand(ContextMenuStrip menuStrip, string parent, string name, string text)
                : this(menuStrip, parent, name, text, null, null) { }

            /// <summary>
            /// 
            /// </summary>
            public override void Execute()
            {
                if (name != null && parent != null && text != null)
                {
                    if (command != null)
                    {
                        if (image != null)
                            menuStrip.AddMenuItem(parent, name, text, image, command);
                        else
                            menuStrip.AddMenuItem(parent, name, text, command);
                    }
                    else
                    {
                        if (image != null)
                            menuStrip.AddMenuItem(parent, name, text, image);
                        else
                            menuStrip.AddMenuItem(parent, name, text);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private class AddSeparatorCommand : AddMenuItemCommand
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="menuStrip"></param>
            /// <param name="parent"></param>
            public AddSeparatorCommand(ContextMenuStrip menuStrip, string? parent)
                : base(menuStrip) { }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="menuStrip"></param>
            public AddSeparatorCommand(ContextMenuStrip menuStrip)
                : this(menuStrip, null) { }

            /// <summary>
            /// 
            /// </summary>
            public override void Execute()
            {
                if (parent != null)
                    menuStrip.AddMenuSeparator(parent);
                else
                    menuStrip.AddMenuSeparator();
            }
        }
    }
}
