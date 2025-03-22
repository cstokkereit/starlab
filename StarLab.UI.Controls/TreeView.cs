using System.ComponentModel;

namespace StarLab.UI.Controls
{
    /// <summary>
    /// Extends the <see cref="System.Windows.Forms.TreeView"/> control. See http://cyotek.com/blog/extending-the-labeledit-functionality-of-a-treeview-to-include-validation
    /// </summary>
    public class TreeView : System.Windows.Forms.TreeView
    {
        private ContextMenuManager menuManager = new ContextMenuManager(); // A manager that configures the context menu that is displayed when a node is clicked.

        private bool showContextMenu = true; // A flag that controls the display of context menus.

        [Category("Behavior")]
        public event EventHandler<NodeRequestTextEventArgs>? RequestDisplayText;

        [Category("Behavior")]
        public event EventHandler<NodeRequestTextEventArgs>? RequestEditText;

        [Category("Behavior")]
        public event EventHandler<NodeLabelEditEventArgs>? ValidateLabelEdit;

        /// <summary>
        /// Gets the <see cref="ContextMenuManager"/> that configures the context menus.
        /// </summary>
        public ContextMenuManager ContextMenuManager => menuManager;

        /// <summary>
        /// Gets or sets the <see cref="ContextMenuStrip"/> used by the TreeView.
        /// </summary>
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

        /// <summary>
        /// Event handler for the <see cref="System.Windows.Forms.TreeView.OnEnter"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        protected override void OnEnter(EventArgs e)
        {
            showContextMenu = true;

            base.OnEnter(e);
        }

        /// <summary>
        /// Event handler for the <see cref="Control.OnMouseDown"/> event.
        /// </summary>
        /// <param name="e">A <see cref="MouseEventArgs"/> that provides context for the event.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var node = GetNodeAt(e.X, e.Y);

                if (node != null) SelectedNode = node;
            }

            if (showContextMenu)
            {
                menuManager.ShowContextMenu();
                showContextMenu = false;
            }

            base.OnMouseDown(e);
        }

        /// <summary>
        /// Event handler for the <see cref="ToolStripDropDown.OnOpening"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">A <see cref="CancelEventArgs"/> that provides context for the event.</param>
        private void OnMenuStripOpening(object? sender, CancelEventArgs? e)
        {
            menuManager.Update(SelectedNode);
        }
    }
}
