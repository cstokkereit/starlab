using AutoMapper;
using StarLab.Application.Configuration;
using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents.Charts
{
    internal class ColourMagnitudeChartViewPresenter : ChildViewPresenter<IChartView, IDocumentController>, IChartViewPresenter
    {
        public ColourMagnitudeChartViewPresenter(IChartView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfigurationService configuration, IMapper mapper, IEventAggregator events)
            : base(view, commands, useCaseFactory, configuration, mapper, events)
        {

        }

        public override string Name => throw new NotImplementedException();

        public override void Initialise(IApplicationController controller)
        {
            if (!Initialised)
            {
                base.Initialise(controller);

                //View.MinimumSize = new Size(200, 200);
            }
        }
    }
}
