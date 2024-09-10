using StarLab.Commands;
using StarLab.Presentation;
using StarLab.Presentation.Docking;
using StarLab.Presentation.Workspaces;
using StarLab.UI.Docking;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace StarLab.UI.Workspaces
{
    /// <summary>
    /// 
    /// </summary>
    public partial class WorkspaceView : View, IWorkspaceView
    {
        private readonly IWorkspaceViewPresenter presenter;

        /// <summary>
        /// Initialises a new instance of the <see cref="WorkspaceView"/> class.
        /// </summary>
        /// <param name="presenterFactory">An <see cref="IPresenterFactory"/> that is used to create the <see cref="IPresenter"/> that controls this view.</param>
        public WorkspaceView(IPresenterFactory presenterFactory)
        {
            InitializeComponent();

            presenter = (IWorkspaceViewPresenter)presenterFactory.CreatePresenter(this);

            dockPanel.Theme = new VS2015LightTheme();

            dockPanel.Theme.Extender.FloatWindowFactory = new FloatWindowFactory();
        }

        #region IMainView Members

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

        public void CloseAll()
        {
            List<IDockContent> documents = new List<IDockContent>(dockPanel.Documents);

            foreach (var document in documents)
            {
                document.DockHandler.Close();
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

        public void Initialise(IApplicationController controller, IDockableViewFactory factory)
        {
            Initialise(controller);

            presenter.Initialise(controller, factory);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public override void Initialise(IApplicationController controller)
        {
            base.Initialise(controller);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        public void Show(IDockableView view)
        {
            if (view is DockableView dockable)
            {
                dockable.Show(dockPanel);
            }
        }

        #endregion

        #region Event Handlers
        
        private void dockPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (dockPanel.ActiveDocument is IDockableView view)
            {
                presenter.SetActiveDocument(view.Name);
            }
        }

        private void dockPanel_ContentRemoved(object sender, DockContentEventArgs e)
        {
            if (dockPanel.DocumentsCount == 0)
            {
                presenter.ClearActiveDocument();
            }
        } 

        #endregion
    }
}
