using StarLab.Presentation;

namespace StarLab.Application.Workspace
{
    public class ToolView : DockableView
    {
        private readonly IDockableViewPresenter presenter;

        public ToolView(string name, IControlView content, IPresenterFactory factory)
            : base(content, factory)
        {
            Name = name;
        }
    }
}
