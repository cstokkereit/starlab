namespace StarLab.UI.Controls
{
    /// <summary>
    /// Extends the <see cref="System.Windows.Forms.TreeView"/> control.
    /// </summary>
    public class TreeView : System.Windows.Forms.TreeView
    {
        private int selectedImageIndex = -1; // The selected image index of the last node that was selected.

        /// <summary>
        /// Event handler for the <see cref="TreeView.AfterSelect"/> event.
        /// </summary>
        /// <param name="e">A <see cref="TreeViewEventArgs"/> that provides context for the event.</param>
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            if (!Focused)
            {
                selectedImageIndex = SelectedNode.SelectedImageIndex;
                SelectedNode.SelectedImageIndex = SelectedNode.ImageIndex;
            }

            base.OnAfterSelect(e);
        }

        /// <summary>
        /// Event handler for the <see cref="Control.OnGotFocus"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            if (selectedImageIndex != -1)
            {
                SelectedNode.SelectedImageIndex = selectedImageIndex;
                selectedImageIndex = -1;
            }

            base.OnGotFocus(e);
        }

        /// <summary>
        /// Event handler for the <see cref="Control.OnLostFocus"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        protected override void OnLostFocus(EventArgs e)
        {
            selectedImageIndex = SelectedNode.SelectedImageIndex;
            SelectedNode.SelectedImageIndex = SelectedNode.ImageIndex;

            base.OnLostFocus(e);
        }

        /// <summary>
        /// Event handler for the <see cref="Control.OnMouseDown"/> event.
        /// </summary>
        /// <param name="e">A <see cref="MouseEventArgs"/> that provides context for the event.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            var node = GetNodeAt(e.X, e.Y);

            if (node != null) SelectedNode = node;

            base.OnMouseDown(e);
        }
    }
}
