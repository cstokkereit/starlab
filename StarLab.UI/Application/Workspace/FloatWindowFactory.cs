using WeifenLuo.WinFormsUI.Docking;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A factory that will be used to create instances of the <see cref="SizeableFloatWindow"> class.
    /// </summary>
    public class FloatWindowFactory : DockPanelExtender.IFloatWindowFactory
    {
        /// <summary>
        /// Creates an instance of the <see cref="FloatWindow"/> class with the specified size and location.
        /// </summary>
        /// <param name="dockPanel">The <see cref="DockPanel"/> that will contain the <see cref="FloatWindow"/>.</param>
        /// <param name="pane">The <see cref="DockPane"/> that will contain the <see cref="FloatWindow"/>.</param>
        /// <param name="bounds">A <see cref="Rectangle"/> that specifies the size and location of the <see cref="FloatWindow"/>.</param>
        /// <returns>The specified <see cref="FloatWindow"/>.</returns>
        public FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane, Rectangle bounds)
        {
            return CreateCustomFloatWindow(dockPanel, pane, bounds);
        }

        /// <summary>
        /// Creates an instance of the <see cref="FloatWindow"/> class with the specified size and location.
        /// </summary>
        /// <param name="dockPanel">The <see cref="DockPanel"/> that will contain the <see cref="FloatWindow"/>.</param>
        /// <param name="pane">The <see cref="DockPane"/> that will contain the <see cref="FloatWindow"/>.</param>
        /// <returns>The specified <see cref="FloatWindow"/>.</returns>
        public FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane)
        {
            return CreateCustomFloatWindow(dockPanel, pane, new Rectangle());
        }

        /// <summary>
        /// Gets the default height.
        /// </summary>
        public int DefaultHeight { get; private set; }

        /// <summary>
        /// Gets the default width.
        /// </summary>
        public int DefaultWidth { get; private set; }

        /// <summary>
        /// Sets the size of the <see cref="SizeableFloatWindow"/>.
        /// </summary>
        /// <param name="height">The new height.</param>
        /// <param name="width">The new width.</param>
        public void SetWindowSize(int height, int width)
        {
            DefaultHeight = height;
            DefaultWidth = width;
        }

        /// <summary>
        /// Creates an instance of the <see cref="SizeableFloatWindow"/> class with the specified size and location.
        /// </summary>
        /// <param name="dockPanel">The <see cref="DockPanel"/> that will contain the <see cref="SizeableFloatWindow"/>.</param>
        /// <param name="pane">The <see cref="DockPane"/> that will contain the <see cref="SizeableFloatWindow"/>.</param>
        /// <param name="bounds">A <see cref="Rectangle"/> that specifies the size and location of the <see cref="SizeableFloatWindow"/>.</param>
        /// <returns>The specified <see cref="SizeableFloatWindow"/>.</returns>
        private FloatWindow CreateCustomFloatWindow(DockPanel dockPanel, DockPane pane, Rectangle bounds)
        {
            FloatWindow window;

            if (bounds.Height > 0 && bounds.Width > 0)
            {
                window = new SizeableFloatWindow(dockPanel, pane, bounds);
            }
            else
            {
                window = new SizeableFloatWindow(dockPanel, pane);
            }

            if (DefaultHeight > 0) window.Height = DefaultHeight;

            return window;
        }
    }
}
