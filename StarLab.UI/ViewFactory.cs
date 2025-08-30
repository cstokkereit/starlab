using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Workspace;
using StarLab.Presentation.Workspace.Documents;
using StarLab.Shared.Properties;
using StarLab.UI.Workspace;
using StarLab.UI.Workspace.Documents;
using System.Diagnostics;

namespace StarLab.UI
{
    /// <summary>
    /// A factory for creating <see cref="IView"/>s.
    /// </summary>
    public class ViewFactory : Factory, IViewFactory
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ViewFactory)); // The logger that will be used for writing log messages.

        private readonly Dictionary<string, ViewDefinition> definitions = new Dictionary<string, ViewDefinition>(); // A dictionary that holds the view definitions indexed by view name.

        private readonly IPresenterFactory factory; // A factory that will be used to create the use case presenters.

        /// <summary>
        /// Initialises a new instance of the <see cref="ViewFactory"/> class.
        /// </summary>
        /// <param name="factory">An <see cref="IPresenterFactory"/> that will be used to create use case presenters.</param>
        public ViewFactory(IPresenterFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));

            CreateViewDefinitions();
        }

        /// <summary>
        /// Creates an <see cref="IPresenter"/> to control the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> that the presenter will control.</param>
        /// <returns>An <see cref="IPresenter"/> that can be used to control the <see cref="IView"/> provided.</returns>
        public IPresenter CreatePresenter(IView view)
        {
            return factory.CreatePresenter(view);
        }

        /// <summary>
        /// Creates an <see cref="IDockableViewPresenter"/> to control the <see cref="IDocumentView"/> provided.
        /// </summary>
        /// <param name="document">An <see cref="IDocument"/> that the view represents.</param>
        /// <param name="view">The <see cref="IDocumentView"/> that the presenter will control.</param>
        /// <returns>An <see cref="IDockableViewPresenter"/> that can be used to control the <see cref="IDocumentView"/> provided.</returns>
        public IDockableViewPresenter CreatePresenter(IDocument document, IDocumentView view)
        {
            return factory.CreatePresenter(document, view);
        }

        /// <summary>
        /// Creates an <see cref="IChildViewPresenter"/> to control the <see cref="IChildView"/> provided.
        /// </summary>
        /// <param name="definition">An <see cref="IViewDefinition"/> that holds the information used to construct the view.</param>
        /// <param name="view">The <see cref="IChildView"/> that the presenter will control.</param>
        /// <returns>An <see cref="IChildViewPresenter"/> that can be used to control the <see cref="IView"/> provided.</returns>
        public IChildViewPresenter CreatePresenter(IViewDefinition definition, IChildView view)
        {
            return factory.CreatePresenter(definition, view);
        }

        /// <summary>
        /// Creates an <see cref="IChildViewPresenter"/> to control the <see cref="IChildView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IChildView"/> that the presenter will control.</param>
        /// <returns>An <see cref="IChildViewPresenter"/> that can be used to control the <see cref="IChildView"/> provided.</returns>
        public IChildViewPresenter CreatePresenter(IChildView view)
        {
            return factory.CreatePresenter(view);
        }

        /// <summary>
        /// Creates the specified <see cref="IView"/>.
        /// </summary>
        /// <param name="name">The name of the view.</param>
        /// <param name="text">The view text.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        public IView CreateView(string name, string text)
        {
            Debug.Assert(definitions.ContainsKey(name)); // If this assertion fails you will need to create the required view defintion.

            return CreateView(definitions[name], text);
        }

        /// <summary>
        /// Creates the specified <see cref="IView"/>.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"> that specifies the view to be created.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        public IView CreateView(IDocument document)
        {
            Debug.Assert(definitions.ContainsKey(document.View)); // If this assertion fails you will need to create the required view defintion.

            return new DocumentView(document, this, definitions[document.View]);
        }

        /// <summary>
        /// Creates the specified <see cref="IChildView"/>.
        /// </summary>
        /// <param name="definition">An <see cref="IViewDefinition"/> that holds the information required to construct the view.</param>
        /// <returns>The specified <see cref="IChildView"/>.</returns>
        public IChildView CreateView(IViewDefinition definition)
        {
            return (IChildView)CreateInstance(definition.TypeName, new object[] { definition, this });
        }

        /// <summary>
        /// Adds a new <see cref="ViewDefinition"/> to the dictionary.
        /// </summary>
        /// <param name="view">The <see cref="ViewDefinition"/> to add.</param>
        private void AddViewDefinition(ViewDefinition view)
        {
            definitions.Add(view.Name, view);
        }

        /// <summary>
        /// Creates the specified <see cref="IView"/>.
        /// </summary>
        /// <param name="definition">The <see cref="ViewDefinition"/> that specifies the view to be created.</param>
        /// <param name="text">The view text.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        /// <exception cref="Exception"></exception>
        private IView CreateView(ViewDefinition definition, string text)
        {
            IView view;

            switch (definition.ViewType)
            {
                case ViewTypes.Application:
                    view = new ApplicationView(Resources.StarLab, this);
                    break;

                case ViewTypes.Dialog:
                    view = new DialogView(definition.Name, text, this, definition);
                    break;

                case ViewTypes.MessageBox:
                    view = new MessageBoxView(definition.Name, this);
                    break;

                case ViewTypes.Tool:
                    view = new ToolView(definition.Name, text, this, definition);
                    break;

                default:
                    var message = string.Format(string.Format(Resources.UnexpectedViewType, definition.ViewType));
                    if (log.IsFatalEnabled) log.Fatal(message);
                    throw new Exception(message);
            }

            return view;
        }

        /// <summary>
        /// Creates the dialog view definitions and adds them to the dictionary.
        /// </summary>
        private void CreateDialogViewDefinitions()
        {
            AddViewDefinition(new ViewDefinition(Views.About, ViewTypes.Dialog).AddChild("StarLab.UI.Help.AboutView, StarLab.UI"));
            AddViewDefinition(new ViewDefinition(Views.AddDocument, ViewTypes.Dialog).AddChild("StarLab.UI.Workspace.Documents.AddDocumentView, StarLab.UI"));
            AddViewDefinition(new ViewDefinition(Views.Options, ViewTypes.Dialog).AddChild("StarLab.UI.Options.OptionsView, StarLab.UI"));
        }

        /// <summary>
        /// Creates the document view definitions and adds them to the dictionary.
        /// </summary>
        private void CreateDocumentViewDefinitions()
        {
            AddViewDefinition(new ViewDefinition("ColourMagnitudeChartView", ViewTypes.Document).AddChild(Views.ChartSettings, "StarLab.UI.Workspace.Documents.Charts.ChartSettingsView, StarLab.UI", 1)
                                                                                                .AddChild(Views.Chart, "StarLab.UI.Workspace.Documents.Charts.ChartView, StarLab.UI", 2));
        }

        /// <summary>
        /// Creates the tool view definitions and adds them to the dictionary.
        /// </summary>
        private void CreateToolViewDefinitions()
        {
            AddViewDefinition(new ViewDefinition(Views.WorkspaceExplorer, ViewTypes.Tool).AddChild("StarLab.UI.Workspace.WorkspaceExplorer.WorkspaceExplorerView, StarLab.UI"));
        }

        /// <summary>
        /// Creates the view definitions and adds them to the dictionary.
        /// </summary>
        private void CreateViewDefinitions()
        {
            AddViewDefinition(new ViewDefinition(Views.Application, ViewTypes.Application));
            AddViewDefinition(new ViewDefinition(Views.MessageBox, ViewTypes.MessageBox));

            CreateDialogViewDefinitions();
            CreateDocumentViewDefinitions();
            CreateToolViewDefinitions();
        }
    }
}
