using StarLab.Commands;
using StarLab.Presentation;
using StarLab.UI.Controls;

namespace StarLab.UI.Workspaces.WorkspaceExplorer
{
    internal class DocumentMenuManager : MenuStateManager, IMenuManager
    {
        #region IMenuManager Members

        public void AddMenuItem(string name, string text)
        {
            throw new NotImplementedException();
        }

        public void AddMenuItem(string parent, string name, string text)
        {
            throw new NotImplementedException();
        }

        public void AddMenuItem(string name, string text, Image image)
        {
            throw new NotImplementedException();
        }

        public void AddMenuItem(string name, string text, ICommand command)
        {
            throw new NotImplementedException();
        }

        public void AddMenuItem(string parent, string name, string text, Image image)
        {
            throw new NotImplementedException();
        }

        public void AddMenuItem(string parent, string name, string text, ICommand command)
        {
            throw new NotImplementedException();
        }

        public void AddMenuItem(string name, string text, Image image, ICommand command)
        {
            throw new NotImplementedException();
        }

        public void AddMenuItem(string parent, string name, string text, Image image, ICommand command)
        {
            throw new NotImplementedException();
        }

        public void AddMenuSeparator(string parent)
        {
            throw new NotImplementedException();
        } 

        #endregion

        protected override void CreateMenu(Controls.ContextMenuStrip menuStrip, TreeNode node)
        {
            menuStrip.AddMenuItem("The", "The");
        }

        protected override bool IsTargetNode(TreeNode node)
        {
            return (string)node.Tag == Constants.DOCUMENT;
        }
    }
}
