using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents;
using StarLab.Shared;
using StarLab.Shared.Properties;
using Stratosoft.Commands;

namespace StarLab.UI.Workspace.Documents
{
    /// <summary>
    /// A <see cref="UserControl"/> that implements the behaviour that is specific to the Add Document dialog.
    /// </summary>
    public partial class AddDocumentView : UserControl, IAddDocumentView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AddDocumentView)); // The logger that will be used for writing log messages.

        private IAddDocumentViewPresenter? presenter; // The presenter that controls the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentView"/> class.
        /// </summary>
        public AddDocumentView()
        {
            InitializeComponent();

            Name = Views.AddDocument;
        }

        /// <summary>
        /// Gets the view ID.
        /// </summary>
        public string ID => Name;

        /// <summary>
        /// Gets the preferred panel, if any, in which to display the view.
        /// </summary>
        public SplitViewPanels Panel => SplitViewPanels.Any;

        /// <summary>
        /// Adds a document type to the list of available document types.
        /// </summary>
        /// <param name="key">The key that specifies the document type.</param>
        /// <param name="text">The display text for the document type.</param>
        /// <param name="imageKey">The key that specifies the image use to represent the document type.</param>
        public void AddDocumentType(string key, string text, string imageKey)
        {
            listDocumentTypes.Items.Add(key, text, imageKey);
            listDocumentTypes.Items[0].Selected = true;
        }

        /// <summary>
        /// Adds the <see cref="Image"/> provided to the <see cref="ImageList"/> used by the document types <see cref="ListView"/>.
        /// </summary>
        /// <param name="key">The key used to identify the image being added.</param>
        /// <param name="image">The <see cref="Image"/> being added.</param>
        public void AddImage(string key, Image image)
        {
            imageList.Images.Add(key, image);
        }

        /// <summary>
        /// Attaches the <see cref="IPresenter"/> that controls the view.
        /// </summary>
        /// <param name="presenter">The <see cref="IChildViewPresenter"/> that controls the view.</param>
        public void Attach(IChildViewPresenter presenter)
        {
            if (this.presenter != null) throw new InvalidOperationException(Resources.PresenterAlreadyAttached);

            this.presenter = (IAddDocumentViewPresenter)presenter;
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
        /// Clears the list containing the available document types.
        /// </summary>
        public void ClearDocumentTypes()
        {
            listDocumentTypes.Items.Clear();
        }

        /// <summary>
        /// Clears the list containing the images that represent the different document types.
        /// </summary>
        public void ClearImages()
        {
            imageList.Images.Clear();
        }

        /// <summary>
        /// Detaches the presenter that controls the view.
        /// </summary>
        public void Detach()
        {
            if (presenter != null)
            {
                var entry = $"{presenter.GetType().Name}({Name})";

                presenter = null;

                log.Debug(string.Format(LogEntries.PresenterDetached, entry));
            }
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        public void Initialise()
        {
            listDocumentTypes.Columns.Add(string.Empty);
            listDocumentTypes.Columns[0].Width = listDocumentTypes.Width;
            listDocumentTypes.View = View.Details;
        }

        /// <summary>
        /// Event handler for the <see cref="Button.Click"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that provides context for the event.</param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            presenter?.AddDocument(textName.Text, listDocumentTypes.SelectedItems[0].Name);
        }
    }
}
