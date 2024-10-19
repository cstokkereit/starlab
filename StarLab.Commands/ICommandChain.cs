namespace StarLab.Commands
{
    public interface ICommandChain : ICommand, IComponentCommand
    {
        void Add(ICommand command);
    }
}
