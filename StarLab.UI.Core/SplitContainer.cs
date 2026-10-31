using StarLab.Presentation;
using Stratosoft.Commands;

namespace StarLab.UI
{
    /// <summary>
    /// A <see cref="UserControl"/> that combines a <see cref="SplitContainer"/> with a <see cref="ToolStrip"/>.
    /// </summary>
    public partial class SplitContainer : UserControl, IToolbarManager
    {
        private readonly Dictionary<SplitViewPanels, PanelManager> panels = new Dictionary<SplitViewPanels, PanelManager>(); // A dictionary containing the managers that control the panels indexed by location.

        /// <summary>
        /// Initialises a new instance of the <see cref="SplitContainer"/> class.
        /// </summary>
        public SplitContainer()
        {
            InitializeComponent();

            panels[SplitViewPanels.Panel1] = new PanelManager(container.Panel1);
            panels[SplitViewPanels.Panel2] = new PanelManager(container.Panel2);

            container.FixedPanel = FixedPanel.Panel1;
            container.Panel1Collapsed = true;
        }

        /// <summary>
        /// Adds a button to the tool bar.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        /// <param name="tooltip">The tooltip text.</param>
        /// <param name="image">The button image.</param>
        /// <param name="command">The command to invoke when the button is clicked.</param>
        public void AddToolbarButton(string name, string tooltip, Image? image, ICommand command)
        {
            var button = new ToolStripButton(image)
            {
                ToolTipText = tooltip
            };

            toolStrip.Items.Add(button);

            if (button != null && command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(button);
            }
        }

        /// <summary>
        /// Adds a control to the specified splitter panel.
        /// </summary>
        /// <param name="control">The control to be added.</param>
        /// <param name="panel">The panel that the control should be added to.</param>
        public void AddControl(Control control, SplitViewPanels panel)
        {
            panels[panel].Add(control);

            var size1 = GetMinimumSize(container.Panel1.Controls);
            var size2 = GetMinimumSize(container.Panel2.Controls);

            switch (container.Orientation)
            {
                case Orientation.Horizontal:
                    container.Panel1MinSize = size1.Height;
                    container.Panel2MinSize = size2.Height;
                    break;

                case Orientation.Vertical:
                    container.Panel1MinSize = size1.Width;
                    container.Panel2MinSize = size2.Width;
                    break;
            }

            MinimumSize = GetMinimumSize(size1, size2);
        }

        /// <summary>
        /// Hides the specified control.
        /// </summary>
        /// <param name="name">The name of the control being hidden.</param>
        public void HideSplitContent(string name)
        {
            if (panels[SplitViewPanels.Panel1].Contains(name))
            {
                panels[SplitViewPanels.Panel1].Remove(name);
                container.Panel1Collapsed = true;
            }
            else if (panels[SplitViewPanels.Panel2].Contains(name))
            {
                panels[SplitViewPanels.Panel2].Remove(name);
                container.Panel2Collapsed = true;
            }
        }

        /// <summary>
        /// Shows the specified <see cref="Control"/>.
        /// </summary>
        /// <param name="name">The name of the control being shown.</param>
        public void ShowSplitContent(string name)
        {
            if (panels[SplitViewPanels.Panel1].Contains(name))
            {
                panels[SplitViewPanels.Panel1].ShowContent(name);
                container.Panel1Collapsed = false;
            }
            else if (panels[SplitViewPanels.Panel2].Contains(name))
            {
                panels[SplitViewPanels.Panel2].ShowContent(name);
                container.Panel2Collapsed = false;
            }

            container.SplitterDistance = container.Panel1MinSize;
        }

        /// <summary>
        /// Calculates the smallest rectangle that satisfies the minimum size requirements for all of the controls in the collection provided.
        /// </summary>
        /// <param name="controls">A collection containing the controls.</param>
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
        /// Calculates the smallest rectangle that satisfies the minimum size requirements for all of the controls in the left and right or top and bottom panels.
        /// </summary>
        /// <param name="size1">The left or top panel depending on the orientation of the splitter bar.</param>
        /// <param name="size2">The right or bottom panel depending on the orientation of the splitter bar.</param>
        /// <returns>A <see cref="Size"/> struct that specifies the minimum height and width.</returns>
        private Size GetMinimumSize(Size size1, Size size2)
        {
            Size size = new Size();

            switch (container.Orientation)
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
        private void OnPaint(object sender, PaintEventArgs e)
        {
            switch (container.Orientation)
            {
                case Orientation.Horizontal:
                    e.Graphics.DrawLine(Pens.DarkGray, 0, container.SplitterDistance + (container.SplitterWidth / 2),
                    container.Width, container.SplitterDistance + (container.SplitterWidth / 2));
                    break;

                case Orientation.Vertical:
                    e.Graphics.DrawLine(Pens.DarkGray, container.SplitterDistance + (container.SplitterWidth / 2), 0,
                    container.SplitterDistance + (container.SplitterWidth / 2), container.Height);
                    break;
            }
        }

        /// <summary>
        /// A class that manages the controls that can be shown in a splitter panel.
        /// </summary>
        private class PanelManager
        {
            private readonly Dictionary<string, Control> controls = new Dictionary<string, Control>(); // A dictionary containing the managed controls indexed by name.

            private readonly SplitterPanel panel; // The splitter panel control that contains the managed controls.

            private Control? control; // The active control.

            /// <summary>
            /// Initialises a new instance of the <see cref="PanelManager"/> class.
            /// </summary>
            /// <param name="panel">The managed splitter panel.</param>
            public PanelManager(SplitterPanel panel)
            {
                this.panel = panel;
            }

            /// <summary>
            /// Adds a control to the managed splitter panel.
            /// </summary>
            /// <param name="control">The control to add.</param>
            public void Add(Control control)
            {
                controls.Add(control.Name, control);

                if (controls.Count == 1)
                {
                    control.Dock = DockStyle.Fill;
                    panel.Controls.Add(control);
                    this.control = control;
                }
            }

            /// <summary>
            /// Determines whether the specified control is managed by this instance.
            /// </summary>
            /// <param name="name">The name of the control.</param>
            /// <returns><c>true</c> if the control is managed by this instance; <c>false</c> otherwise.</returns>
            public bool Contains(string name)
            {
                return controls.ContainsKey(name);
            }

            /// <summary>
            /// Removes the specified control from the splitter panel.
            /// </summary>
            /// <param name="name">The name of the control.</param>
            public void Remove(string name)
            {
                panel.Controls.Remove(control);
            }

            /// <summary>
            /// Shows the specified control.
            /// </summary>
            /// <param name="name">The name of the control.</param>
            public void ShowContent(string name)
            {
                if (control != null) panel.Controls.Remove(control);

                if (controls.TryGetValue(name, out Control? content))
                {
                    content.Dock = DockStyle.Fill;
                    panel.Controls.Add(content);
                    control = content;
                }
            }
        }
    }
}
