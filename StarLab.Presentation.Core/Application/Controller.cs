using StarLab.Application;

namespace StarLab.Application
{
    public abstract class Controller
    {
        private readonly IUseCaseFactory factory;

        public Controller(IUseCaseFactory factory)
        {
            this.factory = factory;
        }

        #region IController Members

        public string Name => GetName();

        #endregion

        protected IUseCaseFactory UseCaseFactory { get => factory; }

        protected abstract string GetName();
    }
}
