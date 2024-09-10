using AutoMapper;
using StarLab.Application.UseCases;
using StarLab.Presentation.Docking;
using StarLab.Presentation.Events;
using StarLab.Presentation.Model;
using StarLab.Shared.Properties;

namespace StarLab.Presentation
{
    public class PresenterFactory : IPresenterFactory
    {
        private readonly PresenterTypeResolver types = new PresenterTypeResolver();

        private readonly IUseCaseFactory useCaseFactory;

        private readonly IConfiguration configuration;

        private readonly IEventAggregator events;

        private readonly IMapper mapper;

        public PresenterFactory(IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
        {
            this.useCaseFactory = useCaseFactory;
            this.configuration = configuration;
            this.events = events;
            this.mapper = mapper;
        }

        public IControlViewPresenter CreatePresenter(IControlView view, string presenterTypeName)
        {
            IControlViewPresenter? presenter = null;

            var type = Type.GetType(presenterTypeName);

            if (type != null)
            {
                presenter = Activator.CreateInstance(type, new object[] { view, useCaseFactory, configuration, mapper, events }) as IControlViewPresenter;
            }

            if (presenter == null)
            {
                throw new Exception(string.Format(Resources.MessageCouldNotBeCreated, presenterTypeName));
            }

            return presenter;
        }

        public IControlViewPresenter CreatePresenter(IControlView view)
        {
            IControlViewPresenter? presenter = null;

            var typeName = types.Resolve(view.Name);

            var type = Type.GetType(typeName);

            if (type != null)
            {
                presenter = Activator.CreateInstance(type, new object[] { view, useCaseFactory, configuration, mapper, events }) as IControlViewPresenter;
            }

            if (presenter == null)
            {
                throw new Exception(string.Format(Resources.MessageCouldNotBeCreated, typeName));
            }

            return presenter;
        }

        public IDockableViewPresenter CreatePresenter(IControlView content, IDockableView view)
        {
            return new DockableViewPresenter(view, useCaseFactory, configuration, mapper, events);
        }

        public IDockableViewPresenter CreatePresenter(IDockableView view, IDocument document)
        {
            return new DockableViewPresenter(view, document, useCaseFactory, configuration, mapper, events);
        }

        public IFormViewPresenter CreatePresenter(IView view)
        {
            IFormViewPresenter? presenter = null;

            var typeName = types.Resolve(view.Name);

            var type = Type.GetType(typeName);

            if (type != null)
            {
                presenter = Activator.CreateInstance(type, new object[] { view, useCaseFactory, configuration, mapper, events }) as IFormViewPresenter;
            }

            if (presenter == null)
            {
                throw new Exception(string.Format(Resources.MessageCouldNotBeCreated, typeName));
            }

            return presenter;
        }

        public ISplitViewPresenter CreatePresenter(ISplitView view)
        {
            return new SplitViewPresenter(view, useCaseFactory, configuration, mapper, events);
        }
    }
}
