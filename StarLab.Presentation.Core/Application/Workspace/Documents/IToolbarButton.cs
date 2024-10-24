using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents
{
    public interface IToolbarButton
    {
        ICommand Command { get; }

        Image Image { get; }

        string Name { get; }

        string Tooltip { get; }
    }
}
