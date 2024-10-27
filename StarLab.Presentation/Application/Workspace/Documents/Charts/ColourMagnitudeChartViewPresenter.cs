using AutoMapper;
using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents.Charts
{
    internal class ColourMagnitudeChartViewPresenter : ControlViewPresenter<IChartView>, IChartViewPresenter
    {
        public ColourMagnitudeChartViewPresenter(IChartView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(view, commands, useCaseFactory, configuration, mapper, events)
        {

        }

        public override string Name => throw new NotImplementedException();

        public override void Initialise(IApplicationController controller)
        {
            base.Initialise(controller);

            //View.MinimumSize = new Size(200, 200);
        }
    }
}
