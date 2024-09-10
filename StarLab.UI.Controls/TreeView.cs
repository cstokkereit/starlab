using System.ComponentModel;

namespace StarLab.UI.Controls
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

        protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                e.CancelEdit = true;

                var labelEditAargs = new NodeLabelEditEventArgs(e.Node, e.Label);
                ValidateLabelEdit?.Invoke(this, labelEditAargs);

                if (e.Node != null)
                {
                    if (labelEditAargs.CancelEdit)
                    {
                        editedText = e.Node.Text;
                        e.Node.BeginEdit();
                    }
                    else
                    {
                        var displayTextArgs = new NodeRequestTextEventArgs(e.Node, e.Label);
                        RequestDisplayText?.Invoke(this, displayTextArgs);

                        if (!displayTextArgs.Cancel) e.Node.Text = displayTextArgs.Label;
                    }
                }
            }

            base.OnAfterLabelEdit(e);
        }

        protected override void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
        {

            if (e.Node != null)
            {
                var args = new NodeRequestTextEventArgs(e.Node, editedText ?? e.Node.Text);

                if (editedText == null) RequestEditText?.Invoke(this, args);

                editedText = null;

                if (args.Cancel) e.CancelEdit = true;

                if (!e.CancelEdit)
                {
                    var handle = NativeMethods.SendMessage(this.Handle, NativeMethods.TVM_GETEDITCONTROL, IntPtr.Zero, IntPtr.Zero);
                    
                    if (handle != IntPtr.Zero) NativeMethods.SendMessage(handle, NativeMethods.WM_SETTEXT, IntPtr.Zero, args.Label);
                }
            }

            base.OnBeforeLabelEdit(e);
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
