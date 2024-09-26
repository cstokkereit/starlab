namespace StarLab.Application
{
    public interface ISplitViewController : IController
    {
        void AddChild(IControlView child, SplitViewPanels panel);

        void Collapse(string view);

        void Expand(string view);
    }
}
