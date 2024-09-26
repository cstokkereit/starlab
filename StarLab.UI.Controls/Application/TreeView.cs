using System.ComponentModel;

namespace StarLab.Application
{
    public class TreeView : System.Windows.Forms.TreeView
    {
        private ContextMenuManager menuManager = new ContextMenuManager();

        private string? editedText;

        [Category("Behavior")]
        public event EventHandler<NodeRequestTextEventArgs>? RequestDisplayText;

        [Category("Behavior")]
        public event EventHandler<NodeRequestTextEventArgs>? RequestEditText;

        [Category("Behavior")]
        public event EventHandler<NodeLabelEditEventArgs>? ValidateLabelEdit;

        public ContextMenuManager ContextMenuManager => menuManager;

        public new ContextMenuStrip? ContextMenuStrip
        {
            get => (ContextMenuStrip?)base.ContextMenuStrip;

            set
            {
                if (value != null)
                {
                    menuManager.AttachContextMenuStrip(value);

                    value.Opening += OnMenuStripOpening;
                }

                base.ContextMenuStrip = value;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var node = GetNodeAt(e.X, e.Y);

                if (node != null) SelectedNode = node;
            }

            base.OnMouseDown(e);
        }

        private void OnMenuStripOpening(object? sender, CancelEventArgs? e)
        {
            menuManager.Update(SelectedNode);
        }
    }
}
