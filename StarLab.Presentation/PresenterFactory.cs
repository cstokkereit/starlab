using AutoMapper;
using Castle.Windsor;
using log4net;
using StarLab.Application;
using StarLab.Presentation.Workspace;
using StarLab.Presentation.Workspace.Documents;
using StarLab.Shared.Properties;
using Stratosoft.Commands;
using System.Diagnostics;

namespace StarLab.Presentation
{
    /// <summary>
    /// A factory for creating <see cref="IPresenter"/>s.
    /// </summary>
    public class PresenterFactory : Factory, IPresenterFactory
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PresenterFactory)); // The logger that will be used for writing log messages.

        private readonly Dictionary<string, string> types = new Dictionary<string, string>();  // A dictionary that holds the presenter type names indexed by view name.

        private readonly IApplicationSettings settings; // Provides access to the application configuration.

        private readonly IWindsorContainer container; // Used to resolve dependencies at run time.

        private readonly IEventAggregator events; // This can be used for subscribing to and publishing events.

        private readonly IUseCaseFactory factory; // This can be used to create use case interactors.

        private readonly IMapper mapper; // Copies data from model objects to data transfer objects and vice versa.

        /// <summary>
        /// Initialises a new instance of the <see cref="PresenterFactory"> class.
        /// </summary>
        /// <param name="container">An <see cref="IWindsorContainer"/> that will be used to resolve dependencies.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="settings">An <see cref="IApplicationSettings"/> that provides access to the application configuration.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public PresenterFactory(IWindsorContainer container, IUseCaseFactory factory, IApplicationSettings settings, IMapper mapper, IEventAggregator events)
        {   
            this.container = container ?? throw new ArgumentNullException(nameof(container));
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.events = events ?? throw new ArgumentNullException(nameof(events));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            InitialiseDialogViewPresenterTypes();
            InitialiseDocumentViewPresenterTypes();
            InitialiseToolViewPresenterTypes();
        }

        /// <summary>
        /// Creates an <see cref="IPresenter"/> to control the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> that the presenter will control.</param>
        /// <returns>An <see cref="IPresenter"/> that can be used to control the <see cref="IView"/> provided.</returns>
        public IPresenter CreatePresenter(IView view)
        {
            var commands = container.Resolve<ICommandManager>();

            IPresenter presenter;

            switch (view.GetType().Name)
            {
                case Views.Application:
                    presenter = new ApplicationViewPresenter((IApplicationView)view, commands, factory, settings, mapper, events);
                    break;

                case Views.Dialog:
                    presenter = new DialogViewPresenter((IDialogView)view, commands, factory, settings, mapper, events);
                    break;

                case Views.MessageBox:
                    presenter = new MessageBoxViewPresenter((IMessageBoxView)view, commands, factory, settings, mapper, events);
                    break;

                case Views.Tool:
                    presenter = new ToolViewPresenter((IDockableView)view, commands, factory, settings, mapper, events);
                    break;

                default:
                    throw new ArgumentException(string.Format(string.Format(Resources.UnexpectedViewType, view.GetType().Name)));
            }

            return presenter;
        }

        /// <summary>
        /// Creates an <see cref="IDockableViewPresenter"/> to control the <see cref="IDocumentView"/> provided.
        /// </summary>
        /// <param name="document">An <see cref="IDocument"/> that the view represents.</param>
        /// <param name="view">The <see cref="IDocumentView"/> that the presenter will control.</param>
        /// <returns>An <see cref="IDockableViewPresenter"/> that can be used to control the <see cref="IDocumentView"/> provided.</returns>
        public IDockableViewPresenter CreatePresenter(IDocument document, IDocumentView view)
        {
            return new DocumentViewPresenter(view, document, container.Resolve<ICommandManager>(), factory, settings, mapper, events);
        }

        /// <summary>
        /// Creates an <see cref="IChildViewPresenter"/> to control the <see cref="IChildView"/> provided.
        /// </summary>
        /// <param name="definition">An <see cref="IViewDefinition"/> that holds the information used to construct the view.</param>
        /// <param name="view">The <see cref="IChildView"/> that the presenter will control.</param>
        /// <returns>An <see cref="IChildViewPresenter"/> that can be used to control the <see cref="IChildView"/> provided.</returns>
        public IChildViewPresenter CreatePresenter(IViewDefinition definition, IChildView view)
        {
            Debug.Assert(types.ContainsKey(definition.Name)); // If this assertion fails you will need to create the required view defintion.

            return (IChildViewPresenter)CreateInstance(types[definition.Name], new object[] { view, container.Resolve<ICommandManager>(), factory, settings, mapper, events });
        }

        /// <summary>
        /// Creates an <see cref="IChildViewPresenter"/> to control the <see cref="IChildView"/> provided.
        /// </summary>
        /// <param name="child">The <see cref="IChildView"/> that the presenter will control.</param>
        /// <returns>An <see cref="IChildViewPresenter"/> that can be used to control the <see cref="IChildView"/> provided.</returns>
        public IChildViewPresenter CreatePresenter(IChildView view)
        {
            //Debug.Assert(types.ContainsKey(view.Name)); // If this assertion fails you will need to create the required view defintion.

            return (IChildViewPresenter)CreateInstance(types[view.Name], new object[] { view, container.Resolve<ICommandManager>(), factory, settings, mapper, events });
        }

        /// <summary>
        /// Adds the dialog view presenter type names to the dictionary.
        /// </summary>
        private void InitialiseDialogViewPresenterTypes()
        {
            types.Add(Views.About, "StarLab.Presentation.Help.AboutViewPresenter, StarLab.Presentation");
            types.Add(Views.AddDocument, "StarLab.Presentation.Workspace.Documents.AddDocumentViewPresenter, StarLab.Presentation");
            types.Add(Views.Options, "StarLab.Presentation.Options.OptionsViewPresenter, StarLab.Presentation");
        }

        /// <summary>
        /// Adds the document view presenter type names to the dictionary.
        /// </summary>
        private void InitialiseDocumentViewPresenterTypes()
        {
            types.Add($"ColourMagnitudeChartView::{Views.ChartSettings}", "StarLab.Presentation.Workspace.Documents.Charts.ChartSettingsViewPresenter, StarLab.Presentation");
            types.Add($"ColourMagnitudeChartView::{Views.Chart}", "StarLab.Presentation.Workspace.Documents.Charts.ColourMagnitudeChartViewPresenter, StarLab.Presentation");
        }

        /// <summary>
        /// Adds the tool view presenter type names to the dictionary.
        /// </summary>
        private void InitialiseToolViewPresenterTypes()
        {
            types.Add(Views.WorkspaceExplorer, "StarLab.Presentation.Workspace.WorkspaceExplorer.WorkspaceExplorerViewPresenter, StarLab.Presentation");
        }
    }
}
