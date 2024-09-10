using AutoMapper;
using StarLab.Application.UseCases;
using StarLab.Presentation.Docking;
using StarLab.Presentation.Events;
using StarLab.Presentation.Model;

namespace StarLab.Presentation
{
    public class DockableViewPresenter : Presenter, IDockableViewPresenter
    {
        private readonly IDockableView view;

        public DockableViewPresenter(IDockableView view, IDocument document, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(useCaseFactory, configuration, mapper, events)
        {
            Location = view.DefaultLocation;

            this.view = view;
        }

        public DockableViewPresenter(IDockableView view, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(useCaseFactory, configuration, mapper, events)
        {
            Location = view.DefaultLocation;

            this.view = view;
        }

        public string Location { get; set; }

        public override string Name => view.Name + Constants.CONTROLLER;

        public override void Initialise(IApplicationController controller)
        {
            base.Initialise(controller);
        }

        public void Refresh()
        {

        }
    }
}
