using WeifenLuo.WinFormsUI.Docking;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A custom <see cref="FloatWindow"/> that includes the maximise and minimise buttons.
    /// </summary>
    public class SizeableFloatWindow : FloatWindow
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="SizeableFloatWindow"/> class.
        /// </summary>
        /// <param name="dockPanel">The <see cref="DockPanel"/> that will contain the <see cref="SizeableFloatWindow"/>.</param>
        /// <param name="pane">The <see cref="DockPane"/> that will contain the <see cref="SizeableFloatWindow"/>.</param>
        public SizeableFloatWindow(DockPanel dockPanel, DockPane pane)
            : base(dockPanel, pane)
        {
            FormBorderStyle = FormBorderStyle.Sizable;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="SizeableFloatWindow"/> class.
        /// </summary>
        /// <param name="dockPanel">The <see cref="DockPanel"/> that will contain the <see cref="SizeableFloatWindow"/>.</param>
        /// <param name="pane">The <see cref="DockPane"/> that will contain the <see cref="SizeableFloatWindow"/>.</param>
        /// <param name="bounds">A <see cref="Rectangle"/> that specifies the size and location of the <see cref="SizeableFloatWindow"/>.</param>
        public SizeableFloatWindow(DockPanel dockPanel, DockPane pane, Rectangle bounds)
            : base(dockPanel, pane, bounds)
        {
            FormBorderStyle = FormBorderStyle.Sizable;
        }
    }
}
