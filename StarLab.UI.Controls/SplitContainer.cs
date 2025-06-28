using StarLab.Presentation;
using Stratosoft.Commands;

namespace StarLab.UI.Controls
{
    /// <summary>
    /// A <see cref="UserControl"/> that combines a <see cref="System.Windows.Forms.SplitContainer"/> with a <see cref="ToolStrip"/>.
    /// </summary>
    public partial class SplitContainer : UserControl, IToolbarManager
    {
        private readonly Dictionary<string, Control> views = new Dictionary<string, Control>(); // A dictionary containing the contained controls indexed by name.

        /// <summary>
        /// Initialises a new instance of the <see cref="SplitContainer"/> class.
        /// </summary>
        public SplitContainer()
        {
            InitializeComponent();

            splitContainer.Panel1Collapsed = true;
        }

        /// <summary>
        /// Gets the left or top panel of the <see cref="SplitContainer"/> depending on the <see cref="Orientation"/>.
        /// </summary>
        public SplitterPanel Panel1 => splitContainer.Panel1;

        /// <summary>
        /// Gets the right or bottom panel of the <see cref="SplitContainer"/> depending on the <see cref="Orientation"/>.
        /// </summary>
        public SplitterPanel Panel2 => splitContainer.Panel2;

        /// <summary>
        /// Adds a button to the tool bar.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        /// <param name="tooltip">The tooltip text.</param>
        /// <param name="image">The <see cref="Image"> to use for the button.</param>
        /// <param name="command">The <see cref="ICommand"> to invoke when the button is clicked.</param>
        public void AddToolbarButton(string name, string tooltip, Image? image, ICommand command)
        {
            var button = new ToolStripButton(image);

            button.ToolTipText = tooltip;

            toolStrip.Items.Add(button);

            if (button != null && command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(button);
            }
        }

        /// <summary>
        /// Adds a <see cref="Control"/> to the specified <see cref="SplitterPanel">.
        /// </summary>
        /// <param name="control">The <see cref="Control"/> to be added.</param>
        /// <param name="panel">The panel that the control should be added to.</param>
        public void AddControl(Control control, SplitViewPanels panel)
        {
            switch (panel)
            {
                case SplitViewPanels.Panel1:
                    splitContainer.Panel1.Controls.Add(control);
                    break;

                case SplitViewPanels.Panel2:
                    splitContainer.Panel2.Controls.Add(control);
                    break;
            }

            views.Add(control.Name, control);

            var size1 = GetMinimumSize(splitContainer.Panel1.Controls);
            var size2 = GetMinimumSize(splitContainer.Panel2.Controls);

            switch (splitContainer.Orientation)
            {
                case Orientation.Horizontal:
                    splitContainer.Panel1MinSize = size1.Height;
                    splitContainer.Panel2MinSize = size2.Height;
                    break;

                case Orientation.Vertical:
                    splitContainer.Panel1MinSize = size1.Width;
                    splitContainer.Panel2MinSize = size2.Width;
                    break;
            }

            MinimumSize = GetMinimumSize(size1, size2);

            control.Dock = DockStyle.Fill;
            control.Visible = true;
        }

        /// <summary>
        /// Hides the specified <see cref="Control"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="Control"/> to hide.</param>
        public void HideSplitContent(string name)
        {
            if (views.ContainsKey(name))
            {
                if (splitContainer.Panel1.Controls.Contains(views[name]))
                {
                    splitContainer.Panel1Collapsed = true;
                }

                if (splitContainer.Panel2.Controls.Contains(views[name]))
                {
                    splitContainer.Panel2Collapsed = true;
                }
            }
        }

        /// <summary>
        /// Shows the specified <see cref="Control"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="Control"/> to show.</param>
        public void ShowSplitContent(string name)
        {
            if (views.ContainsKey(name))
            {
                if (splitContainer.Panel1.Controls.Contains(views[name]))
                {
                    splitContainer.Panel1Collapsed = false;
                }

                if (splitContainer.Panel2.Controls.Contains(views[name]))
                {
                    splitContainer.Panel2Collapsed = false;
                }

                // TODO - Show hide as appropriate if more than one view in a panel
            }
        }

        /// <summary>
        /// Gets the minimum <see cref="Size"/> that satisfies the minimum size requirements for all of the controls in the collection provided.
        /// </summary>
        /// <param name="controls">A <see cref="System.Windows.Forms.ControlCollection"/> that contains the controls.</param>
        /// <returns>A <see cref="Size"/> struct that specifies the minimum height and width.</returns>
        private Size GetMinimumSize(ControlCollection controls)
        {
            var size = new Size(0, 0);

            foreach (UserControl control in controls)
            {
                if (size.Height < control.MinimumSize.Height) size.Height = control.MinimumSize.Height;
                if (size.Width < control.MinimumSize.Width) size.Width = control.MinimumSize.Width;
            }

            return size;
        }

        /// <summary>
        /// Gets the minimum <see cref="Size"/> that satisfies the minimum size requirements of the controls in the left and right or top and bottom panels.
        /// </summary>
        /// <param name="size1">The left or top panel depending on the orientation of the splitter bar.</param>
        /// <param name="size2">The right or bottom panel depending on the orientation of the splitter bar.</param>
        /// <returns>A <see cref="Size"/> struct that specifies the minimum height and width.</returns>
        private Size GetMinimumSize(Size size1, Size size2)
        {
            Size size = new Size();

            switch (splitContainer.Orientation)
            {
                case Orientation.Horizontal:
                    size = new Size(size1.Width > size2.Width ? size1.Width : size2.Width, size1.Height + size2.Height);
                    break;

                case Orientation.Vertical:
                    size = new Size(size1.Width + size2.Width, size1.Height > size2.Height ? size1.Height : size2.Height);
                    break;
            }

            return size;
        }

        /// <summary>
        /// Event handler for the <see cref="Control.Paint"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">A <see cref="PaintEventArgs"/> that provides context for the event.</param>
        private void SplitContainer_Paint(object sender, PaintEventArgs e)
        {
            switch (splitContainer.Orientation)
            {
                case Orientation.Horizontal:
                    e.Graphics.DrawLine(Pens.DarkGray, 0, splitContainer.SplitterDistance + (splitContainer.SplitterWidth / 2),
                    splitContainer.Width, splitContainer.SplitterDistance + (splitContainer.SplitterWidth / 2));
                    break;

                case Orientation.Vertical:
                    e.Graphics.DrawLine(Pens.DarkGray, splitContainer.SplitterDistance + (splitContainer.SplitterWidth / 2), 0,
                    splitContainer.SplitterDistance + (splitContainer.SplitterWidth / 2), splitContainer.Height);
                    break;
            }
        }
    }
}
