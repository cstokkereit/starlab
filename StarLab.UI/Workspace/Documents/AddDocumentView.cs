using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace.Documents;
using Stratosoft.Commands;

namespace StarLab.UI.Workspace.Documents
{
    /// <summary>
    /// TODO
    /// </summary>
    public partial class AddDocumentView : UserControl, IAddDocumentView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AddDocumentView)); // The logger that will be used for writing log messages.

        private readonly IAddDocumentViewPresenter presenter; // The presenter that controls the view.

        private readonly SplitViewPanels panel; // The panel that will contain the view.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="parent"></param>
        /// <param name="factory"></param>
        public AddDocumentView(IChildViewConfiguration config, IViewConfiguration parent, IViewFactory factory)
        {
            InitializeComponent();

            Name = Views.ADD_DOCUMENT;

            panel = (SplitViewPanels)config.Panel;

            presenter = (IAddDocumentViewPresenter)factory.CreatePresenter(parent, this);
        }

        /// <summary>
        /// Gets the <see cref="IChildViewController"> that controls this view.
        /// </summary>
        public IChildViewController Controller => (IChildViewController)presenter;

        /// <summary>
        /// 
        /// </summary>
        public SplitViewPanels Panel => panel;

        /// <summary>
        /// 
        /// </summary>
        public string DocumentType => "ColourMagnitudeChartView"; // TODO

        /// <summary>
        /// 
        /// </summary>
        public string DocumentName
        {
            get { return textName.Text; }
            set { textName.Text = value; }
        }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="image"></param>
        public void AddImage(string key, Image image)
        {
            imageList.Images.Add(image);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        public void AttachAddButtonCommand(ICommand command)
        {
            if (command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(buttonAdd);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
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

        }
    }
}
