using AutoMapper;
using StarLab.Commands;

using ImageResources = StarLab.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Application.Workspace.Documents.Charts
{
    internal class ChartSettingsViewPresenter : ChildViewPresenter<IChartSettingsView, IDocumentController>, IChartSettingsViewPresenter, IChartSettingsController
    {
        private IDocumentController? parentController;

        public ChartSettingsViewPresenter(IChartSettingsView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(view, commands, useCaseFactory, configuration, mapper, events) { }

        public override string Name => throw new NotImplementedException();

        public override void Attach(IViewController parentController)
        {
            base.Attach(parentController);

            parentController = (IDocumentController)parentController;

            //parentController.AddToolbarButton(Constants.SHOW_SETTINGS, StringResources.Settings, ImageResources.Settings, GetCommand(parentController, Actions.SHOW_SPLIT_CONTENT, View.Name));
            //View.AttachCancelButtonCommand(GetCommand(parentController, Actions.HIDE_SPLIT_CONTENT, View.Name));

            //var chain = GetCommandChain();
            //chain.Add(GetCommand(this, Actions.APPLY_SETTINGS));
            //chain.Add(GetCommand(parentController, Actions.HIDE_SPLIT_CONTENT, View.Name));

            //View.AttachOKButtonCommand(chain);
        }
    }
}
