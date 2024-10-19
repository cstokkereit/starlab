namespace StarLab.Application
{
    public interface IFormContent<T> where T : IController
    {
        void Initialise(IApplicationController controller, T parentController);
    }
}
