using AutoMapper;
using StarLab.Application.Configuration;
using StarLab.Commands;

using ImageResources = StarLab.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Application.Workspace.Documents.Charts
{
    internal class ChartSettingsViewPresenter : ChildViewPresenter<IChartSettingsView, IDocumentController>, IChartSettingsViewPresenter, IChartSettingsController
    {
        public ChartSettingsViewPresenter(IChartSettingsView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfigurationService configuration, IMapper mapper, IEventAggregator events)
            : base(view, commands, useCaseFactory, configuration, mapper, events) { }

        public override string Name => throw new NotImplementedException();

        public override void Initialise(IApplicationController controller)
        {
            if (!Initialised)
            {
                base.Initialise(controller);

                ParentController.AddToolbarButton(Constants.SHOW_SETTINGS, StringResources.Settings, ImageResources.Settings, GetCommand(ParentController, Actions.SHOW_SPLIT_CONTENT, View.Name));
                View.AttachCancelButtonCommand(GetCommand(ParentController, Actions.HIDE_SPLIT_CONTENT, View.Name));

                var chain = GetCommandChain();
                chain.Add(GetCommand(this, Actions.APPLY_SETTINGS));
                chain.Add(GetCommand(ParentController, Actions.HIDE_SPLIT_CONTENT, View.Name));

                View.AttachOKButtonCommand(chain);

                View.Initialise(controller);
            }
        }
    }
}
