namespace StarLab.Application.Workspace
{
    public interface IFolder : ICollapsible
    {
        bool IsNew { get; }

        string Key { get; }

        string Name { get; }

        string ParentKey { get; }
    }
}
