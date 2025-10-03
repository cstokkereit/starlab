using AutoMapper;
using log4net;
using StarLab.Application;
using StarLab.Presentation.Workspace.Documents.Charts;
using StarLab.Shared.Properties;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IDocumentView"/>.
    /// </summary>
    public sealed class DocumentViewPresenter : Presenter, IDockableViewPresenter, IDocumentController, ISubscriber<WorkspaceChangedEventArgs>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DocumentViewPresenter)); // The logger that will be used for writing log messages.

        private readonly IDocumentView view; // The view controlled by the presenter.

        private IDocument document; // The document that the view represents.

        /// <summary>
        /// Initialises a new instance of the <see cref="DocumentViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IDocumentView"/> controlled by this presenter.</param>
        /// <param name="document">The <see cref="IDocument"/> that the view represents.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="settings">An <see cref="IApplicationSettings"/> that provides access to the application configuration.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public DocumentViewPresenter(IDocumentView view, IDocument document, ICommandManager commands, IUseCaseFactory factory, IApplicationSettings settings, IMapper mapper, IEventAggregator events)
            : base(commands, factory, settings, mapper, events)
        {
            this.document = document;
            this.view = view;

            Location = Constants.Document;

            log.Debug(string.Format(Resources.InstanceCreated, nameof(DocumentViewPresenter)));
        }

        /// <summary>
        /// Gets or sets the current location of the view.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public override string Name => Controllers.GetDocumentControllerName(document.ID);

        /// <summary>
        /// Adds a button to the tool bar.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        /// <param name="tooltip">The tooltip text.</param>
        /// <param name="image">The <see cref="Image"> to use for the button.</param>
        /// <param name="command">The <see cref="ICommand"> to invoke when the button is clicked.</param>
        public void AddToolbarButton(string name, string tooltip, Image image, ICommand command)
        {
            view.AddToolbarButton(name, tooltip, image, command);
        }

        /// <summary>
        /// Closes the document window.
        /// </summary>
        public void Close()
        {
            view.HideOnClose = false;
            view.Close();
        }

        /// <summary>
        /// Gets the specified <see cref="IChildViewController"/> 
        /// </summary>
        /// <param name="name">The name of the required controller.</param>
        /// <returns>The required <see cref="IChildViewController"/>.</returns>
        public IChildViewController GetController(string name)
        {
            return view.GetController(name);
        }

        /// <summary>
        /// Hides the specified split content.
        /// </summary>
        /// <param name="name">The name of the content to be hidden.</param>
        public void HideSplitContent(string name)
        {
            view.HideSplitContent(name);
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public override void Initialise(IApplicationController controller)
        {
            if (!Initialised)
            {
                base.Initialise(controller);

                view.Initialise(controller);

                if (document.Chart != null)
                {
                    ((IChartController)GetController(Controllers.ChartController)).UpdateChart(document.Chart);
                }
            }
        }

        /// <summary>
        /// Event handler for the WorkspaceChangedEvent event.
        /// </summary>
        /// <param name="args">A <see cref="WorkspaceChangedEventArgs"/> that provides context for the event.</param>
        public void OnEvent(WorkspaceChangedEventArgs args)
        {
            UpdateDocument(args.Workspace.GetDocument(document.ID));
        }

        /// <summary>
        /// Shows the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        public void Show(IView view)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Shows the specified split content.
        /// </summary>
        /// <param name="name">The name of the content to be shown.</param>
        public void ShowSplitContent(string name)
        {
            if (name == Views.ChartSettings)
            {
                ((IChartSettingsController)GetController(Controllers.ChartSettingsController)).UpdateSettings(document);
            }
            
            view.ShowSplitContent(name);
        }

        /// <summary>
        /// Updates the <see cref="IDocument"/> that the document window represents.
        /// </summary>
        /// <param name="document">The new <see cref="IDocument"/>.</param>
        public void UpdateDocument(IDocument document)
        {
            ((IChartSettingsController)GetController(Controllers.ChartSettingsController)).UpdateSettings(document);

            ((IChartController)GetController(Controllers.ChartController)).UpdateChart(document.Chart);

            view.Name = document.Name;
            view.Text = document.Name;

            this.document = document;
        }
    }
}
