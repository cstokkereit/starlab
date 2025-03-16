using log4net;
using StarLab.Commands;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A <see cref="Form"/> that is the main application window.
    /// </summary>
    public sealed partial class ApplicationView : Form, IApplicationView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ApplicationView)); // The logger that will be used for writing log messages.

        private readonly IApplicationViewPresenter presenter; // The presenter that controls the view.

        private readonly string id; // The view ID.

        /// <summary>
        /// Initialises a new instance of the <see cref="ApplicationView"/> class.
        /// </summary>
        /// <param name="text">The window text.</param>
        /// <param name="factory">An <see cref="IViewFactory"/> that will be used to create the presenter.</param>
        public ApplicationView(string text, IViewFactory factory)
        {
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));
            ArgumentException.ThrowIfNullOrEmpty(text, nameof(text));

            InitializeComponent();

            Name = Views.WORKSPACE;
            id = Views.WORKSPACE;
            Text = text;

            presenter = (IApplicationViewPresenter)factory.CreatePresenter(Views.WORKSPACE, this);

            dockPanel.Theme = new VS2015LightTheme();

            dockPanel.Theme.Extender.FloatWindowFactory = new FloatWindowFactory();
        }

        /// <summary>
        /// Gets the <see cref="IViewController"> that controls this view.
        /// </summary>
        public IViewController Controller => (IViewController)presenter;

        /// <summary>
        /// Gets or sets a flag that determines whether the dialog box will be hidden or unloaded when it is closed.
        /// </summary>
        public bool HideOnClose { get; set; }

        /// <summary>
        /// Gets the view ID.
        /// </summary>
        public string ID => id;

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        public void AddMenuItem(string name, string text)
        {
            menuStrip.AddMenuItem(name, text);
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        public void AddMenuItem(string parent, string name, string text)
        {
            menuStrip.AddMenuItem(parent, name, text);
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="image">The menu item <see cref="Image"/>.</param>
        public void AddMenuItem(string name, string text, Image image)
        {
            menuStrip.AddMenuItem(name, text, image);
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="command">The <see cref="ICommand"/> that will be invoked when the menu item is clicked.</param>
        public void AddMenuItem(string name, string text, ICommand command)
        {
            menuStrip.AddMenuItem(name, text, command);
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="image">The menu item <see cref="Image"/>.</param>
        public void AddMenuItem(string parent, string name, string text, Image image)
        {
            menuStrip.AddMenuItem(parent, name, text, image);
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="command">The <see cref="ICommand"/> that will be invoked when the menu item is clicked.</param>
        public void AddMenuItem(string parent, string name, string text, ICommand command)
        {
            menuStrip.AddMenuItem(parent, name, text, command);
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="image">The menu item <see cref="Image"/>.</param>
        /// <param name="command">The <see cref="ICommand"/> that will be invoked when the menu item is clicked.</param>
        public void AddMenuItem(string name, string text, Image image, ICommand command)
        {
            menuStrip.AddMenuItem(name, text, image, command);
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="image">The menu item <see cref="Image"/>.</param>
        /// <param name="command">The <see cref="ICommand"/> that will be invoked when the menu item is clicked.</param>
        public void AddMenuItem(string parent, string name, string text, Image image, ICommand command)
        {
            menuStrip.AddMenuItem(parent, name, text, image, command);
        }

        /// <summary>
        /// Adds a separator to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        public void AddMenuSeparator(string parent)
        {
            menuStrip.AddMenuSeparator(parent);
        }

        /// <summary>
        /// Adds a separator to the menu.
        /// </summary>
        public void AddMenuSeparator()
        {
            menuStrip.AddMenuSeparator();
        }

        /// <summary>
        /// Adds a button to the tool bar.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        /// <param name="tooltip">The tooltip text.</param>
        /// <param name="image">The <see cref="Image"/> to use for the button.</param>
        /// <param name="command">The <see cref="ICommand"/> to invoke when the button is clicked.</param>
        public void AddToolbarButton(string name, string tooltip, Image image, ICommand command)
        {
            toolStrip.AddButton(name, tooltip, image, command);
        }

        /// <summary>
        /// Closes the currently selected document.
        /// </summary>
        public void CloseActiveDocument()
        {
            if (dockPanel.ActiveDocument != null) dockPanel.ActiveDocument.DockHandler.Hide();
        }

        /// <summary>
        /// Closes all documents.
        /// </summary>
        public void CloseAll()
        {
            List<IDockContent> documents = new List<IDockContent>(dockPanel.Contents);

            foreach (var document in documents)
            {
                document.DockHandler.DockPanel = null;
            }
        }

        /// <summary>
        /// Generates an XML representation of the workspace including the size, state and location of each of the dockable windows it contains.
        /// </summary>
        /// <returns>An XML representation of the workspace.</returns>
        public string GetLayout()
        {
            var layout = string.Empty;

            using (var stream = new MemoryStream())
            {
                dockPanel.SaveAsXml(stream, Encoding.UTF8);

                stream.Position = 0;

                using (var reader = new StreamReader(stream))
                {
                    layout = reader.ReadToEnd();
                }
            }

            return layout;
        }

        /// <summary>
        /// Uses the layout provided to set the size, state and location of each of the dockable windows within the workspace.
        /// </summary>
        /// <param name="layout">An XML representation of the workspace.</param>
        public void SetLayout(string layout)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(layout)))
            {
                dockPanel.LoadFromXml(stream, new DeserializeDockContent(config =>
                {
                    return (IDockContent)presenter.CreateView(config);
                }));
            }
        }

        /// <summary>
        /// Shows the specified view.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        public void Show(IView view)
        {
            if (view is DockContent dockable)
            {
                dockable.Show(dockPanel);
            }
            else if (view is Form form)
            {
                form.ShowDialog(this);
            }
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified options.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="type">An <see cref="InteractionType"/> that specifies the type of message being displayed.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractnResult"/> that identifies the button that was clicked.</returns>
        public InteractionResult ShowMessage(string caption, string message, InteractionType type, InteractionResponses responses)
        {
            return DialogController.ShowMessage(this, caption, message, type, responses);
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified options.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message, InteractionResponses responses)
        {
            return DialogController.ShowMessage(this, caption, message, responses);
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified options.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message)
        {
            return DialogController.ShowMessage(this, caption, message);
        }

        /// <summary>
        /// Displays an <see cref="OpenFileDialog"/> with the specified options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <returns>The filename selected in the dialog.</returns>
        public string ShowOpenFileDialog(string title, string filter)
        {
            return DialogController.ShowOpenFileDialog(this, title, filter);
        }

        /// <summary>
        /// Displays a <see cref="SaveFileDialog"/> with the specified options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <param name="extension">The default file extension.</param>
        /// <returns>The filename selected in the dialog.</returns>
        public string ShowSaveFileDialog(string title, string filter, string extension)
        {
            return DialogController.ShowSaveFileDialog(this, title, filter, extension);
        }

        /// <summary>
        /// Event handler for the <see cref="DockPanel.ActiveDocumentChanged"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void DockPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {
            UpdateActiveDocument();
        }

        /// <summary>
        /// Event handler for the <see cref="DockPanel.DockContentRemoved"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">A <see cref="DockContentEventArgs"/> that provides context for the event.</param>
        private void DockPanel_DockContentRemoved(object sender, DockContentEventArgs e)
        {
            UpdateActiveDocument();
        }

        /// <summary>
        /// Event handler for the <see cref="Form.FormClosing"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">A <see cref="FormClosingEventArgs"/> that provides context for the event.</param>
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            presenter.ViewClosing(e);
        }

        /// <summary>
        /// Updates the presenter following a change to the active document.
        /// </summary>
        private void UpdateActiveDocument()
        {
            if (dockPanel.ActiveDocument is IDockableView view)
            {
                presenter.SetActiveDocument(view.ID);
            }
            else
            {
                presenter.ClearActiveDocument();
            }
        }
    }
}
