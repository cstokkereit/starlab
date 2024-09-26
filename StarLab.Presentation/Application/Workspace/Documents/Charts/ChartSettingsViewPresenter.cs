using AutoMapper;
using StarLab.Application.Events;
using StarLab.Commands;

using ImageResources = StarLab.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Application.Workspace.Documents.Charts
{
    internal class ChartSettingsViewPresenter : ControlViewPresenter<IChartSettingsView>, IChartSettingsViewPresenter, IChartSettingsController
    {
        public ChartSettingsViewPresenter(IChartSettingsView view, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(view, useCaseFactory, configuration, mapper, events) { }

        public void AttachCommands(IApplicationController controller, ISplitViewController viewController)
        {
            CreateToolbarButton(string.Format(Constants.SHOW_SETTINGS, View.Name), StringResources.Settings, ImageResources.Settings, AppController.GetCommand(viewController, Verbs.EXPAND, View.Name));

            View.AttachCancelButtonCommand(AppController.GetCommand(viewController, Verbs.COLLAPSE, View.Name));
            View.AttachOKButtonCommand(GetOKButtonCommand(viewController));
        }

        public override void Initialise(IApplicationController controller)
        {
            base.Initialise(controller);
        }

        private ICommand GetOKButtonCommand(ISplitViewController viewController)
        {
            List<ICommand> commands =
            [
                AppController.GetCommand(this, Verbs.APPLY),
                AppController.GetCommand(viewController, Verbs.COLLAPSE, View.Name) // Could chain commands - Commnd(ChartSettingsController.ApplyChanges) -> Command(SplitViewController.Collapse)
            ];

            return AppController.CreateAggregateCommand(commands, View.Name);
        }
    }
}
