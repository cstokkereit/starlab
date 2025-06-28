using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents;
using Stratosoft.Commands;

namespace StarLab.UI.Workspace.Documents
{
    /// <summary>
    /// A <see cref="UserControl"/> that implements the behaviour that is specific to the Add Document dialog.
    /// </summary>
    public partial class AddDocumentView : UserControl, IAddDocumentView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AddDocumentView)); // The logger that will be used for writing log messages.

        private readonly IAddDocumentViewPresenter presenter; // The presenter that controls the view.

        private readonly SplitViewPanels panel; // The panel that will contain the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentView"/> class.
        /// </summary>
        /// <param name="definition">An <see cref="IViewDefinition"/> that holds the information required to construct this view.</param>
        /// <param name="factory">An <see cref="IViewFactory"/> that will be used to create the presenter.</param>
        /// <summary>
        public AddDocumentView(IViewDefinition definition, IViewFactory factory)
        {
            ArgumentNullException.ThrowIfNull(definition, nameof(definition));
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));

            InitializeComponent();

            Name = Views.AddDocument;

            panel = (SplitViewPanels)definition.Panel;

            presenter = (IAddDocumentViewPresenter)factory.CreatePresenter(this);
        }

        /// <summary>
        /// Gets the <see cref="IChildViewController"> that controls this view.
        /// </summary>
        public IChildViewController Controller => (IChildViewController)presenter;

        /// <summary>
        /// Gets the preferred panel, if any, in which to display the view.
        /// </summary>
        public SplitViewPanels Panel => panel;

        /// <summary>
        /// Gets the <see cref="DocumentType"/>.
        /// </summary>
        public string DocumentType => "ColourMagnitudeChartView"; // TODO - Other document/chart types

        /// <summary>
        /// Gets or sets the document name.
        /// </summary>
        public string DocumentName
        {
            get { return textName.Text; }
            set { textName.Text = value; }
        }

        /// <summary>
        /// TODO - Implement this properly and provide a summary.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        /// <param name="imageKey"></param>
        public void AddDocument(string key, string text, string imageKey)
        {
            //listDocumentTypes.Items.Add(key, text, imageKey);

            var header = new ColumnHeader();
            //header.Text = "Column 1";

            listDocumentTypes.Columns.Add(header);
            
            listDocumentTypes.Items.Add(key, text, 0);

            listDocumentTypes.Items.Add("HR Diagram - 1");
            listDocumentTypes.Items.Add("HR Diagram - 2");
            listDocumentTypes.Items.Add("HR Diagram - 3");
            listDocumentTypes.Items.Add("HR Diagram - 4");
            listDocumentTypes.Items.Add("HR Diagram - 5");

            listDocumentTypes.Columns[0].Width = listDocumentTypes.Width;

            listDocumentTypes.View = View.Details;
        }

        /// <summary>
        /// Adds the <see cref="Image"/> provided to the <see cref="ImageList"/> used by the document types <see cref="ListView"/>.
        /// </summary>
        /// <param name="key">The key used to identify the image being added.</param>
        /// <param name="image">The <see cref="Image"/> being added.</param>
        public void AddImage(string key, Image image)
        {
            imageList.Images.Add(image);
        }

        /// <summary>
        /// Attaches the 'Add' button to the the <see cref="ICommand"/> provided.
        /// </summary>
        /// <param name="command">The <see cref="ICommand"/> that will be executed when the 'Add' button is clicked.</param>
        public void AttachAddButtonCommand(ICommand command)
        {
            if (command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(buttonAdd);
            }
        }

        /// <summary>
        /// Attaches the 'Cancel' button to the the <see cref="ICommand"/> provided.
        /// </summary>
        /// <param name="command">The <see cref="ICommand"/> that will be executed when the 'Cancel' button is clicked.</param>
        public void AttachCancelButtonCommand(ICommand command)
        {
            if (command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(buttonCancel);
            }
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public void Initialise(IApplicationController controller)
        {
            // TODO - Add implementation if necessary.
        }
    }
}
