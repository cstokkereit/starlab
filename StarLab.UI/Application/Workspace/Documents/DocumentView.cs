using log4net;
using StarLab.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace StarLab.Application.Workspace.Documents
{
    public sealed partial class DocumentView : DockContent, IDocumentView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DocumentView));

        private readonly IDockableViewPresenter presenter;

        private readonly string id;

        public DocumentView(IDocument document, IViewFactory viewFactory, IPresenterFactory presenterFactory)
        {
            InitializeComponent();
            
            try
            {
                presenter = presenterFactory.CreatePresenter(this, document);
            }
            catch (Exception e)
            {
                log.Fatal(e.Message, e);
                throw;
            }

            Name = document.Name;
            Text = document.Name;

            id = document.ID;

            foreach (var content in document.Contents)
            {
                var view = viewFactory.CreateControlView(content.View);

                if (view is Control control)
                    splitContainer.AddControl(control, content.Panel);
            }
        }

        public IViewController Controller => (IViewController)presenter;

        public string ID => id;

        public void AddToolbarButton(string name, string tooltip, Image image, ICommand command)
        {
            splitContainer.AddToolbarButton(name, tooltip, image, command);
        }

        public void HideSplitContent(string name)
        {
            splitContainer.HideSplitContent(name);
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public void Initialise(IApplicationController controller)
        {
            presenter.Initialise(controller);

            if (presenter is IDocumentController parentController)
            {
                foreach (var control in splitContainer.Panel1.Controls)
                {
                    if (control is IFormContent<IDocumentController> content)
                    {
                        content.Initialise(controller, parentController);
                    }
                }

                foreach (var control in splitContainer.Panel2.Controls)
                {
                    if (control is IFormContent<IDocumentController> content)
                    {
                        content.Initialise(controller, parentController);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dockPanel"></param>
        public new void Show(DockPanel dockPanel)
        {
            if (DockState == DockState.Hidden || DockState == DockState.Unknown)
            {
                //Height = presenter.Height;
                //Width = presenter.Width;
            }

            Show(dockPanel, (DockState)Enum.Parse(DockState.GetType(), presenter.Location));
        }

        /// <summary>
        /// Shows the specified view.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        public void Show(IView view)
        {
            if (view is Form form) form.ShowDialog(this);
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

        public void ShowSplitContent(string name)
        {
            splitContainer.ShowSplitContent(name);
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

        protected override string GetPersistString()
        {
            return ID;
        }


        //        /// <summary>
        //        /// 
        //        /// </summary>
        //        private void AttachEventHandlers()
        //        {
        //            DockStateChanged += OnDockStateChanged;
        //        }

        //        /// <summary>
        //        /// 
        //        /// </summary>
        //        private void DetachEventHandlers()
        //        {
        //            DockStateChanged -= OnDockStateChanged;
        //        }

        //        /// <summary>
        //        /// 
        //        /// </summary>
        //        /// <param name="sender"></param>
        //        /// <param name="e"></param>
        //        private void OnDockStateChanged(object? sender, EventArgs? e)
        //        {
        //            if (DockState != DockState.Hidden && DockState != DockState.Unknown)
        //            {
        //                presenter.Location = DockState.ToString();
        //            }
        //        }

        //        private void OnFormClosed(object? sender, EventArgs? e)
        //        {
        //            DetachEventHandlers();
        //        }

        //        private void OnFormShown(object? sender, EventArgs? e)
        //        {

        //        }
    }
}