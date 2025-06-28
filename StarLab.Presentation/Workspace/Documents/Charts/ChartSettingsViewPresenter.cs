using AutoMapper;
using StarLab.Application;
using Stratosoft.Commands;

using ImageResources = StarLab.Presentation.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IChartSettingsView"/>.
    /// </summary>
    internal class ChartSettingsViewPresenter : ChildViewPresenter<IChartSettingsView, IDocumentController>, IChartSettingsViewPresenter, IChartSettingsController
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ChartSettingsViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IChartSettingsView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="configuration">The <see cref="IConfigurationProvider"/> that will be used to get configuration information.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public ChartSettingsViewPresenter(IChartSettingsView view, ICommandManager commands, IUseCaseFactory factory, Configuration.IApplicationConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(view, commands, factory, configuration, mapper, events) { }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public override string Name => throw new NotImplementedException();

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public override void Initialise(IApplicationController controller)
        {
            if (!Initialised)
            {
                base.Initialise(controller);

                ParentController.AddToolbarButton(Constants.ShowSettings, StringResources.Settings, ImageResources.Settings, GetCommand(ParentController, Actions.ShowSplitContent, View.Name));
                View.AttachCancelButtonCommand(GetCommand(ParentController, Actions.HideSplitContent, View.Name));

                var chain = GetCommandChain();
                chain.Add(GetCommand(this, Actions.ApplySettings));
                chain.Add(GetCommand(ParentController, Actions.HideSplitContent, View.Name));

                View.AttachOKButtonCommand(chain);

                View.Initialise(controller);
            }
        }
    }
}
