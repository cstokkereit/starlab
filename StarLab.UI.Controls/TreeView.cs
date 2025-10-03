namespace StarLab.UI.Controls
{
    /// <summary>
    /// Extends the <see cref="System.Windows.Forms.TreeView"/> control.
    /// </summary>
    public class TreeView : System.Windows.Forms.TreeView
    {
        /// <summary>
        /// Event handler for the <see cref="Control.OnHandleCreated"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (!DesignMode && Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6)
            {
                Win32Api.SetWindowTheme(Handle, Win32Api.ExplorerTheme, null);
            }
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
