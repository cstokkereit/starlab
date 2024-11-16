namespace StarLab.Application.Workspace
{
    public interface ICollapsible
    {
        bool Expanded { get; }

        void Collapse();

        void CollapseAll();

        void Expand();

        void ExpandAll();
    }
}
