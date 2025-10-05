using log4net;
using StarLab.Presentation;
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

            var view = new DocumentView(document);
            var presenter = factory.CreatePresenter(document, view);

            var children = definitions[document.View].ChildViewDefinitions;

            foreach (var child in children)
            {
                view.Attach(CreateView(child));
            }

            return view;
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
        /// Creates the application view.
        /// </summary>
        /// <param name="text">The view text.</param>
        /// <returns>The required <see cref="IView"/>.</returns>
        private IView CreateApplicationView(string text)
        {
            var view = new ApplicationView(text);
            var presenter = factory.CreatePresenter(view);
            return view;
        }

        /// <summary>
        /// Creates a dialog view.
        /// </summary>
        /// <param name="definition">An <see cref="IViewDefinition"/> that holds the information required to construct the view.</param>
        /// <param name="text">The view text.</param>
        /// <returns>The required <see cref="IView"/>.</returns>
        private IView CreateDialogView(ViewDefinition definition, string text)
        {
            var view = new DialogView(definition.Name, text, definition);
            var presenter = factory.CreatePresenter(view);

            view.Attach(CreateView(definition.ChildViewDefinitions[0]));

            return view;
        }

        /// <summary>
        /// Creates a message box view.
        /// </summary>
        /// <param name="text">The view name.</param>
        /// <returns>The required <see cref="IView"/>.</returns>
        private IView CreateMessageBoxView(string name)
        {
            var view = new MessageBoxView(name);
            var presenter = factory.CreatePresenter(view);
            return view;
        }

        /// <summary>
        /// Creates a tool view.
        /// </summary>
        /// <param name="definition">An <see cref="IViewDefinition"/> that holds the information required to construct the view.</param>
        /// <param name="text">The view text.</param>
        /// <returns>The required <see cref="IView"/>.</returns>
        private IView CreateToolView(ViewDefinition definition, string text)
        {
            var view = new ToolView(definition.Name, text, definition);
            var presenter = factory.CreatePresenter(view);

            view.Attach(CreateView(definition.ChildViewDefinitions[0]));

            return view;
        }

        /// <summary>
        /// Creates the specified <see cref="IChildView"/>.
        /// </summary>
        /// <param name="definition">An <see cref="IViewDefinition"/> that holds the information required to construct the view.</param>
        /// <returns>The specified <see cref="IChildView"/>.</returns>
        private IChildView CreateView(IViewDefinition definition)
        {
            var view = (IChildView)CreateInstance(definition.TypeName);
            var presenter = factory.CreatePresenter(definition, view);

            return view;
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
                    view = CreateApplicationView(text);
                    break;

                case ViewTypes.Dialog:
                    view = CreateDialogView(definition, text);
                    break;

                case ViewTypes.MessageBox:
                    view = CreateMessageBoxView(definition.Name);
                    break;

                case ViewTypes.Tool:
                    view = CreateToolView(definition, text);
                    break;

                default:
                    throw new Exception(string.Format(string.Format(Resources.UnexpectedViewType, definition.ViewType)));
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
            AddViewDefinition(new ViewDefinition(Views.ColourMagnitudeChart, ViewTypes.Document).AddChild(Views.ChartSettings, "StarLab.UI.Workspace.Documents.Charts.ChartSettingsView, StarLab.UI")
                                                                                                .AddChild(Views.Chart, "StarLab.UI.Workspace.Documents.Charts.ChartView, StarLab.UI"));
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
