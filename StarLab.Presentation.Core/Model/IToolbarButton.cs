using StarLab.Commands;

namespace StarLab.Presentation.Model
{
    public interface IToolbarButton
    {
        ICommand Command { get; }

        Image Image { get; }

        string Name { get; }

        string Tooltip { get; }
    }
}
