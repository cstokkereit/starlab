using AutoMapper;
using StarLab.Application.UseCases;
using StarLab.Presentation.Events;

namespace StarLab.Presentation.Charts
{
    internal class ColourMagnitudeChartViewPresenter : ControlViewPresenter<IChartView>, IChartViewPresenter
    {
        public ColourMagnitudeChartViewPresenter(IChartView view, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(view, useCaseFactory, configuration, mapper, events)
        {

        }

        public override void Initialise(IApplicationController applicationController)
        {
            View.MinimumSize = new Size(200, 200);
        }
    }
}
