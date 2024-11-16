using log4net;
using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents
{
    public partial class AddDocumentView : UserControl, IAddDocumentView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AddDocumentView));

        private readonly IAddDocumentViewPresenter presenter;

        public AddDocumentView(IPresenterFactory factory)
        {
            InitializeComponent();

            Name = Views.ADD_DOCUMENT;

            try
            {
                presenter = (IAddDocumentViewPresenter)factory.CreatePresenter(this);
            }
            catch (Exception e)
            {
                log.Fatal(e.Message, e);
                throw;
            }
        }

        public IChildViewController Controller => (IChildViewController)presenter;

        public string DocumentType => "Chart";

        public string DocumentName
        {
            get { return textName.Text; }
            set { textName.Text = value; }
        }

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

        public void AddImage(string key, Image image)
        {
            imageList.Images.Add(image);
        }

        public void AttachAddButtonCommand(ICommand command)
        {
            if (command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(buttonAdd);
            }
        }

        public void AttachCancelButtonCommand(ICommand command)
        {
            if (command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(buttonCancel);
            }
        }

        public void Initialise(IApplicationController controller)
        {
            
        }
    }
}
