namespace StarLab.Presentation.Model
{
    public interface IFolder
    {
        bool Expanded { get; }

        bool IsNew { get; }

        string Key { get; }

        string Name { get; }

        string ParentKey { get; }

        void Collapse();

        void CollapseAll();

        void Expand();

        void ExpandAll();
    }
}
