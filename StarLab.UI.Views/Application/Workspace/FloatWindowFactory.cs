using WeifenLuo.WinFormsUI.Docking;

namespace StarLab.Application.Workspace
{
    public class FloatWindowFactory : DockPanelExtender.IFloatWindowFactory
    {
        public FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane, Rectangle bounds)
        {
            return CreateCustomFloatWindow(dockPanel, pane, bounds);
        }

        public FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane)
        {
            return CreateCustomFloatWindow(dockPanel, pane, new Rectangle());
        }

        public int DefaultHeight { get; private set; }

        public int DefaultWidth { get; private set; }

        public void SetWindowSize(int height, int width)
        {
            DefaultHeight = height;
            DefaultWidth = width;
        }



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
