using StarLab.Presentation;

namespace StarLab.UI.Docking
{
    public class ToolView : DockableView
    {
        public ToolView(IViewContext context, IControlView content, IPresenterFactory presenterFactory)
            : base(context, content, presenterFactory)
        {
            HideOnClose = true;
        }
    }
}
