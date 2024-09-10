using StarLab.Application.UseCases;

namespace StarLab.Presentation
{
    public class Controller
    {
        private readonly IUseCaseFactory factory;

        public Controller(IUseCaseFactory factory)
        {
            this.factory = factory;
        }

        protected IUseCaseFactory UseCaseFactory { get => factory; }
    }
}
