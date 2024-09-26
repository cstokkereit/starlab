using StarLab.Application;

namespace StarLab.Presentation.Model
{
    public interface IContent
    {
        IReadOnlyList<IContent> Contents { get; }

        string Name { get; }

        SplitViewPanels Panel { get; }

        string View { get; }
    }
}
