using StarLab.Application;

namespace StarLab.Presentation.Model
{
    public interface IContent
    {
        string Name { get; }

        SplitViewPanels Panel { get; }

        string View { get; }
    }
}
