using StarLab.Application;

namespace StarLab.Application.Workspace.Documents
{
    public interface IContent
    {
        string Name { get; }

        SplitViewPanels Panel { get; }

        string View { get; }
    }
}
