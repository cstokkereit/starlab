using StarLab.Commands;

namespace StarLab.Application
{
    public class NodeMenuManager : IMenuManager
    {
        private readonly List<AddMenuItemCommand> commands = new List<AddMenuItemCommand>();

        private readonly string target;

        private readonly string name;

        private ContextMenuStrip? menuStrip;

        public NodeMenuManager(string name, string target)
        {
            this.target = target;
            this.name = name;
        }

        public NodeMenuManager(string target)
            : this(target, target) { }

        public string Name => name;

        public void AddMenuItem(string name, string text)
        {
            if (menuStrip != null) commands.Add(new AddTopLevelMenuItemCommand(menuStrip, name, text));
        }

        public void AddMenuItem(string parent, string name, string text)
        {
            if (menuStrip != null) commands.Add(new AddChildMenuItemCommand(menuStrip, parent, name, text));
        }

        public void AddMenuItem(string name, string text, Image image)
        {
            if (menuStrip != null) commands.Add(new AddTopLevelMenuItemCommand(menuStrip, name, text, image));
        }

        public void AddMenuItem(string name, string text, ICommand command)
        {
            if (menuStrip != null) commands.Add(new AddTopLevelMenuItemCommand(menuStrip, name, text, command));
        }

        public void AddMenuItem(string parent, string name, string text, Image image)
        {
            if (menuStrip != null) commands.Add(new AddChildMenuItemCommand(menuStrip, parent, name, text, image));
        }

        public void AddMenuItem(string parent, string name, string text, ICommand command)
        {
            if (menuStrip != null) commands.Add(new AddChildMenuItemCommand(menuStrip, parent, name, text, command));
        }

        public void AddMenuItem(string name, string text, Image image, ICommand command)
        {
            if (menuStrip != null) commands.Add(new AddTopLevelMenuItemCommand(menuStrip, name, text, image, command));
        }

        public void AddMenuItem(string parent, string name, string text, Image image, ICommand command)
        {
            if (menuStrip != null) commands.Add(new AddChildMenuItemCommand(menuStrip, parent, name, text, image, command));
        }

        public void AddMenuSeparator(string parent)
        {
            if (menuStrip != null) commands.Add(new AddSeparatorCommand(menuStrip, parent));
        }

        public void AddMenuSeparator()
        {
            if (menuStrip != null) commands.Add(new AddSeparatorCommand(menuStrip));
        }

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

        internal void AttachContextMenuStrip(ContextMenuStrip? menuStrip)
        {
            this.menuStrip = menuStrip;
        }

        private bool IsTargetNode(TreeNode node)
        {
            if (node != null)
                return (string)node.Tag == target && node.Name == Name;

            return false;
        }

        private abstract class AddMenuItemCommand
        {
            protected ContextMenuStrip menuStrip;
            protected ICommand? command;
            protected string? parent;
            protected Image? image;
            protected string? name;
            protected string? text;

            public AddMenuItemCommand(ContextMenuStrip menuStrip)
            {
                this.menuStrip = menuStrip;
            }

            public abstract void Execute();
        }

        private class AddTopLevelMenuItemCommand : AddMenuItemCommand
        {
            public AddTopLevelMenuItemCommand(ContextMenuStrip menuStrip, string name, string text, Image? image, ICommand? command)
                : base(menuStrip)
            {
                this.command = command;
                this.image = image;
                this.name = name;
                this.text = text;
            }

            public AddTopLevelMenuItemCommand(ContextMenuStrip menuStrip, string name, string text, ICommand? command)
                : this(menuStrip, name, text, null, command) { }

            public AddTopLevelMenuItemCommand(ContextMenuStrip menuStrip, string name, string text, Image? image)
                : this(menuStrip, name, text, image, null) { }

            public AddTopLevelMenuItemCommand(ContextMenuStrip menuStrip, string name, string text)
                : this(menuStrip, name, text, null, null) { }

            public override void Execute()
            {
                if (name != null && text != null)
                {
                    if (command != null)
                    {
                        if (image != null)
                            menuStrip.AddMenuItem(name, text, image, command);
                        else
                            menuStrip.AddMenuItem(name, text, command); // Can this exist?
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

        private class AddChildMenuItemCommand : AddMenuItemCommand
        {
            public AddChildMenuItemCommand(ContextMenuStrip menuStrip, string parent, string name, string text, Image? image, ICommand? command)
                : base(menuStrip)
            {
                this.command = command;
                this.parent = parent;
                this.image = image;
                this.name = name;
                this.text = text;
            }

            public AddChildMenuItemCommand(ContextMenuStrip menuStrip, string parent, string name, string text, ICommand? command)
                : this(menuStrip, parent, name, text, null, command) { }

            public AddChildMenuItemCommand(ContextMenuStrip menuStrip, string parent, string name, string text, Image? image)
                : this(menuStrip, parent, name, text, image, null) { }

            public AddChildMenuItemCommand(ContextMenuStrip menuStrip, string parent, string name, string text)
                : this(menuStrip, parent, name, text, null, null) { }

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

        private class AddSeparatorCommand : AddMenuItemCommand
        {
            public AddSeparatorCommand(ContextMenuStrip menuStrip, string? parent)
                : base(menuStrip) { }

            public AddSeparatorCommand(ContextMenuStrip menuStrip)
                : this(menuStrip, null) { }

            public override void Execute()
            {
                if (parent != null)
                    menuStrip.AddSeparator(parent);
                else
                    menuStrip.AddSeparator();
            }
        }
    }
}
