﻿using WeifenLuo.WinFormsUI.Docking;

namespace StarLab.Application.Workspace
{
    public class SizeableFloatWindow : FloatWindow
    {
        public SizeableFloatWindow(DockPanel dockPanel, DockPane pane)
            : base(dockPanel, pane)
        {
            FormBorderStyle = FormBorderStyle.Sizable;
        }

        public SizeableFloatWindow(DockPanel dockPanel, DockPane pane, Rectangle bounds)
            : base(dockPanel, pane, bounds)
        {
            FormBorderStyle = FormBorderStyle.Sizable;
        }
    }
}
