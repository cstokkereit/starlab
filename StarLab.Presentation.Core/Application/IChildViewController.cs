namespace StarLab.Application
{
    public interface IChildViewController : IController
    {
        void Attach(IViewController parentController);

        void Initialise(IApplicationController controller);

        void Run(IInteractionContext context);
    }
}
