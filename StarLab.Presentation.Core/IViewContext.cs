using StarLab.Presentation.Model;

namespace StarLab.Presentation
{
    public interface IViewContext
    {
        IContent Content { get; }

        string DefaultLocation { get; }

        string FullName { get; }

        string Name { get; }

        string Text { get; }

        string View { get; }
    }
}
