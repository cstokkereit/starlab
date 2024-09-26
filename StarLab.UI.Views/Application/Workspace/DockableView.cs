using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Presentation;
using StarLab.Presentation.Model;
using WeifenLuo.WinFormsUI.Docking;

namespace StarLab
{
    /// <summary>
    /// The base class for all <see cref="DockContent"/> views.
    /// </summary>
    public partial class DockableView : DockContent, IDockableView
    {
        private readonly IDockableViewPresenter presenter;

        private readonly IControlView content;

        public DockableView(IControlView content, IPresenterFactory factory)
        {
            InitializeComponent();

            presenter = factory.CreatePresenter(this);

            DefaultLocation = string.Empty;

            this.content = content;

            AttachEventHandlers();
        }

        public DockableView(IDocument document, IControlView content, IPresenterFactory factory)
        {
            InitializeComponent();

            presenter = factory.CreatePresenter(this, document);

            DefaultLocation = string.Empty;

            this.content = content;

            AttachEventHandlers();
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

        #region IDockableView Members

        public IViewController Controller => presenter;

        public string DefaultLocation { get; set; }

        public virtual string ID => Name;

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public void Initialise(IApplicationController controller)
        {
            presenter.Initialise(controller);

            if (content is Control control)
            {
                control.Dock = DockStyle.Fill;
                Controls.Add(control);
            }

            content.Initialise(controller);

            //MinimumSize = content.MinimumSize;
        }

        /// <summary>
        /// Shows the specified view.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        public void Show(IView view)
        {
            if (view is Form form)
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
            return MessageBox.Show(this, message, caption, buttons, icon);
        }

        /// <summary>
        /// Displays a message box with the specified text, caption and icon.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="icon">A <see cref="MessageBoxIcon"/> that specifies the icon to include on the meeage box.</param>
        public void ShowMessage(string caption, string message, MessageBoxIcon icon)
        {
            ShowMessage(message, caption, MessageBoxButtons.OK, icon);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string ShowOpenFileDialog(string title, string filter)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        #endregion

        protected override string GetPersistString()
        {
            return ID;
        }

        #region Private Members

        /// <summary>
        /// 
        /// </summary>
        private void AttachEventHandlers()
        {
            DockStateChanged += OnDockStateChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        private void DetachEventHandlers()
        {
            DockStateChanged -= OnDockStateChanged;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDockStateChanged(object? sender, EventArgs? e)
        {
            if (DockState != DockState.Hidden && DockState != DockState.Unknown)
            {
                presenter.Location = DockState.ToString();
            }
        }

        private void OnFormClosed(object? sender, EventArgs? e)
        {
            DetachEventHandlers();
        }

        private void OnFormShown(object? sender, EventArgs? e)
        {

        }
    }
}
