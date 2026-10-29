using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Charts.Overlays;
using StarLab.Shared;
using StarLab.UI;
using Stratosoft.Commands;

namespace StarLab.UI.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// A <see cref="UserControl"/> that implements the behaviour that is specific to the settings panel used to manage the chart overlays.
    /// </summary>
    public partial class OverlayManagerView : ChildView, IOverlayManagerView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ChartSettingsView)); // The logger that will be used for writing log messages.

        private IOverlayManagerViewPresenter? presenter; // The presenter that controls the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="OverlayManagerView"> class.
        /// </summary>
        public OverlayManagerView()
            : base(ViewIDs.OverlayManager, SplitViewPanels.Panel1)
        {
            InitializeComponent();

            Name = ViewNames.OverlayManager;
        }

        /// <summary>
        /// Adds the specified overlay to the list of available overlays.
        /// </summary>
        /// <param name="name">The overlay name.</param>
        /// <param name="caption">The overlay caption.</param>
        /// <param name="shown">true if the overlay is currently shown on the chart; <c>false</c>.</param>
        public void AddOverlay(string name, string caption, bool shown)
        {
            var overlay = listOverlays.Items.Add(caption);

            overlay.Checked = shown;
            overlay.Tag = name;

            listOverlays.ItemChecked += OnItemChecked;
        }

        /// <summary>
        /// Attaches the <see cref="IChildViewPresenter"/> that controls the view.
        /// </summary>
        /// <param name="presenter">The <see cref="IChildViewPresenter"/> that controls the view.</param>
        public override void Attach(IChildViewPresenter presenter)
        {
            if (this.presenter != null) throw new InvalidOperationException(ExceptionMessages.PresenterAlreadyAttached);

            this.presenter = (IOverlayManagerViewPresenter)presenter;

            log.Debug(LogEntries.PresenterAttached(presenter.GetType()));
        }

        /// <summary>
        /// Attaches the <see cref="ICommand"/> provided to the Cancel button.
        /// </summary>
        /// <param name="command">The <see cref="ICommand"/> that will be executed when the Cancel button is clicked.</param>
        public void AttachCancelButtonCommand(ICommand command)
        {
            if (command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(buttonCancel);
            }
        }

        /// <summary>
        /// Attaches the <see cref="ICommand"/> provided to the OK button.
        /// </summary>
        /// <param name="command">The <see cref="ICommand"/> that will be executed when the OK button is clicked.</param>
        public void AttachOKButtonCommand(ICommand command)
        {
            if (command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(buttonOK);
            }
        }

        /// <summary>
        /// Clears the list of overlays.
        /// </summary>
        public void Clear()
        {
            listOverlays.Clear();
        }

        /// <summary>
        /// Detaches the presenter that controls the view.
        /// </summary>
        public override void Detach()
        {
            if (presenter != null)
            {
                var type = presenter.GetType();

                presenter = null;

                log.Debug(LogEntries.PresenterDetached(type));
            }
        }

        /// <summary>
        /// Event handler for the <see cref="ListView.ItemChecked"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="ItemCheckedEventArgs"/> that provides context for the event.</param>
        private void OnItemChecked(object? sender, ItemCheckedEventArgs e)
        {
            var item = e.Item;

            if (item.Focused && item.Tag is string name)
            {
                if (item.Checked)
                {
                    presenter?.AddOverlay(name);
                }
                else
                {
                    presenter?.RemoveOverlay(name);
                }
            }
        }
    }
}
