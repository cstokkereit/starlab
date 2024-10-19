using AutoMapper;
using StarLab.Application.Events;
using StarLab.Commands;

using ImageResources = StarLab.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Application.Workspace.Documents.Charts
{
    internal class ChartSettingsViewPresenter : ControlViewPresenter<IChartSettingsView>, IChartSettingsViewPresenter, IChartSettingsController
    {
        public ChartSettingsViewPresenter(IChartSettingsView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(view, commands, useCaseFactory, configuration, mapper, events) { }

        public override string Name => throw new NotImplementedException();

        public void Initialise(IApplicationController appController, IDocumentController docController)
        {
            base.Initialise(appController);

            docController.AddToolbarButton(string.Format(Constants.SHOW_SETTINGS, View.Name), StringResources.Settings, ImageResources.Settings, GetCommand(docController, Actions.SHOW_SPLIT_CONTENT, View.Name));

            View.AttachCancelButtonCommand(GetCommand(docController, Actions.HIDE_SPLIT_CONTENT, View.Name));

            var chain = GetCommandChain();
            chain.Add(GetCommand(this, Actions.APPLY_SETTINGS));
            chain.Add(GetCommand(docController, Actions.HIDE_SPLIT_CONTENT, View.Name));

            View.AttachOKButtonCommand(chain);
        }

        public override void Initialise(IApplicationController controller)
        {
            base.Initialise(controller);
        }
    }
}
