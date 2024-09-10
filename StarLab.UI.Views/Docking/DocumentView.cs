using StarLab.Presentation;

namespace StarLab.UI.Docking
{
    public class DocumentView : DockableView
    {
        public DocumentView(IViewContext context, IControlView content, IPresenterFactory presenterFactory)
            : base(context, content, presenterFactory)
        {
            HideOnClose = false;
        }
    }
}
