using AutoMapper;
using StarLab.Application.UseCases;
using StarLab.Commands;
using StarLab.Presentation.Events;

using ImageResources = StarLab.Presentation.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Presentation.Charts
{
    internal class ChartSettingsViewPresenter : ControlViewPresenter<IChartSettingsView>, IChartSettingsViewPresenter, IChartSettingsController
    {
        public ChartSettingsViewPresenter(IChartSettingsView view, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(view, useCaseFactory, configuration, mapper, events) { }

        public void AttachCommands(IApplicationController controller, ISplitViewController viewController)
        {
            CreateToolbarButton(string.Format(Constants.SHOW_SETTINGS, View.Name), StringResources.Settings, ImageResources.Settings, controller.GetCommand(CreateAction(viewController, Verbs.EXPAND, View.Name)));

            View.AttachCancelButtonCommand(controller.GetCommand(CreateAction(viewController, Verbs.COLLAPSE, View.Name)));
            View.AttachOKButtonCommand(GetOKButtonCommand(controller, viewController));
        }

        public override void Initialise(IApplicationController controller)
        {
            base.Initialise(controller);
        }

        private ICommand GetOKButtonCommand(IApplicationController controller, ISplitViewController viewController)
        {
            List<ICommand> commands =
            [
                controller.GetCommand(CreateAction<IChartSettingsController>(this, Verbs.APPLY)),
                controller.GetCommand(CreateAction(viewController, Verbs.COLLAPSE, View.Name)) // Could chain commands - Commnd(ChartSettingsController.ApplyChanges) -> Command(SplitViewController.Collapse)
            ];

            return controller.CreateAggregateCommand(commands, View.Name);
        }
    }
}
