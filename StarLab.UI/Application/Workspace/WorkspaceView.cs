using log4net;
using StarLab.Commands;
using StarLab.Shared.Properties;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class WorkspaceView : Form, IWorkspaceView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(WorkspaceView));

        private readonly IWorkspaceViewPresenter presenter;

        private readonly string id;

        /// <summary>
        /// Initialises a new instance of the <see cref="WorkspaceView"/> class.
        /// </summary>
        /// <param name="presenterFactory">An <see cref="IPresenterFactory"/> that is used to create the <see cref="IPresenter"/> that controls this view.</param>
        public WorkspaceView(IPresenterFactory factory)
        {
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));

            InitializeComponent();

            Text = Resources.StarLab;
            Name = Views.WORKSPACE;
            id = Views.WORKSPACE;

            try
            {
                presenter = factory.CreatePresenter(this);
            }
            catch (Exception e)
            {
                log.Fatal(e.Message, e);
                throw;
            }
            
            dockPanel.Theme = new VS2015LightTheme();

            dockPanel.Theme.Extender.FloatWindowFactory = new FloatWindowFactory();
        }

        public IViewController Controller => (IViewController)presenter;

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
        /// <param name="image">The menu item image.</param>
        public void AddMenuItem(string name, string text, Image image)
        {
            menuStrip.AddMenuItem(name, text, image);
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="command">The command to invoke when the menu item is clicked.</param>
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
        /// <param name="image">The menu item image.</param>
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
        /// <param name="command">The command to invoke when the menu item is clicked.</param>
        public void AddMenuItem(string parent, string name, string text, ICommand command)
        {
            menuStrip.AddMenuItem(parent, name, text, command);
        }

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="image">The menu item image.</param>
        /// <param name="command">The command to invoke when the menu item is clicked.</param>
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
        /// <param name="image">The menu item image.</param>
        /// <param name="command">The command to invoke when the menu item is clicked.</param>
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
            menuStrip.AddSeparator(parent);
        }

        /// <summary>
        /// Adds a separator to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        public void AddMenuSeparator()
        {
            menuStrip.AddSeparator();
        }

        /// <summary>
        /// Adds a button to the tool bar.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        /// <param name="tooltip">The tooltip text.</param>
        /// <param name="image">The image to use for the button.</param>
        /// <param name="command">The command to invoke when the button is clicked.</param>
        public void AddToolbarButton(string name, string tooltip, Image image, ICommand command)
        {
            toolStrip.AddButton(name, tooltip, image, command);
        }

        public void CloseActiveDocument()
        {
            if (dockPanel.ActiveDocument != null) dockPanel.ActiveDocument.DockHandler.Hide();
        }

        public void CloseAll()
        {
            List<IDockContent> documents = new List<IDockContent>(dockPanel.Contents);

            foreach (var document in documents)
            {
                document.DockHandler.DockPanel = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="layout"></param>
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
        /// Displays a message box with the specified text, caption, buttons and icon.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="buttons">A <see cref="MessageBoxButtons"/> that specifies which buttons to include on the meeage box.</param>
        /// <param name="icon">A <see cref="MessageBoxIcon"/> that specifies the icon to include on the meeage box.</param>
        /// <returns>A <see cref="DialogResult"/> that identifies the button that was clicked.</returns>
        public DialogResult ShowMessage(string caption, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return DialogController.ShowMessage(this, caption, message, buttons, icon);
        }

        /// <summary>
        /// Displays a message box with the specified text, caption and icon.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="icon">A <see cref="MessageBoxIcon"/> that specifies the icon to include on the meeage box.</param>
        public void ShowMessage(string caption, string message, MessageBoxIcon icon)
        {
            DialogController.ShowMessage(this, caption, message, icon);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string ShowOpenFileDialog(string title, string filter)
        {
            return DialogController.ShowOpenFileDialog(this, title, filter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="filter"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public string ShowSaveFileDialog(string title, string filter, string extension)
        {
            return DialogController.ShowSaveFileDialog(this, title, filter, extension);
        }

        private void dockPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {
            UpdateActiveDocument();
        }

        private void dockPanel_DockContentRemoved(object sender, DockContentEventArgs e)
        {
            UpdateActiveDocument();
        }

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

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            presenter.ViewClosing(e);
        }
    }
}
