namespace StarLab.UI.Controls
{
    /// <summary>
    /// Extends the <see cref="System.Windows.Forms.TreeView"/> control.
    /// </summary>
    public class TreeView : System.Windows.Forms.TreeView
    {
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

            base.OnMouseDown(e);
        }
    }
}
