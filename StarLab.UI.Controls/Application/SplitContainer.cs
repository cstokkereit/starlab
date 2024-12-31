using StarLab.Commands;

namespace StarLab.Application
{
    /// <summary>
    /// TODO
    /// </summary>
    public partial class SplitContainer : UserControl
    {
        private readonly Dictionary<string, Control> views = new Dictionary<string, Control>();

        public SplitContainer()
        {
            InitializeComponent();

            splitContainer.Panel1Collapsed = true;
        }

        public SplitterPanel Panel1 => splitContainer.Panel1;

        public SplitterPanel Panel2 => splitContainer.Panel2;

        /// <summary>
        /// Adds a button to the tool bar.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        /// <param name="tooltip">The tooltip text.</param>
        /// <param name="image">The image to use for the button.</param>
        /// <param name="command">The command to invoke when the button is clicked.</param>
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
        /// 
        /// </summary>
        /// <param name="control"></param>
        /// <param name="panel"></param>
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

            MinimumSize = GetMinimumSize();

            control.Dock = DockStyle.Fill;
            control.Visible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
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
        /// 
        /// </summary>
        /// <param name="name"></param>
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

        private Size GetMinimumSize()
        {
            var size1 = GetMinimumSize(splitContainer.Panel1.Controls);
            var size2 = GetMinimumSize(splitContainer.Panel2.Controls);

            Size size = new Size();

            switch (splitContainer.Orientation)
            {
                case Orientation.Horizontal:
                    size = new Size(size1.Width > size2.Width ? size1.Width : size2.Width, size1.Height + size2.Height);
                    splitContainer.Panel1MinSize = size1.Height;
                    splitContainer.Panel2MinSize = size2.Height;
                    break;

                case Orientation.Vertical:
                    size = new Size(size1.Width + size2.Width, size1.Height > size2.Height ? size1.Height : size2.Height);
                    splitContainer.Panel1MinSize = size1.Width;
                    splitContainer.Panel2MinSize = size2.Width;
                    break;
            }

            return size;
        }

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
