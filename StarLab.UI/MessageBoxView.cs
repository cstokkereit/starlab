using StarLab.Application;
using StarLab.Presentation;

namespace StarLab.UI
{
    /// <summary>
    /// A <see cref="Form"/> that implements a message box.
    /// </summary>
    public partial class MessageBoxView : Form, IMessageBoxView
    {
        // TODO - Some of these constants should be moved to a static class (when we implement another view that needs them)
        private const int TOOLBOX_HEIGHT = 40;

        private const int IMAGE_HEIGHT = 24;

        private const int IMAGE_MARGIN = 3;

        private const int TEXT_HEIGHT = 15;

        private const int MARGIN = 15;

        private readonly IMessageBoxViewPresenter presenter; // The presenter that controls the view.

        private readonly string id; // The view ID.

        private InteractionResult result; // The value that will be returned by the ShowModal method when the message box is closed.

        private Image? image; // The image that identifies the type of message being displayed.

        /// <summary>
        /// Initialises a new instance of the <see cref="MessageBoxView"> class.
        /// </summary>
        /// <param name="name">The name of the dialog.</param>
        /// <param name="factory">An <see cref="IViewFactory"/> that will be used to create the presenter.</param>
        public MessageBoxView(string name, IViewFactory factory)
        {
            InitializeComponent();

            Name = name;
            id = name;

            presenter = (IMessageBoxViewPresenter)factory.CreatePresenter(this);
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
        /// Configures one of the message box response buttons.
        /// </summary>
        /// <param name="index">The index of the button.</param>
        /// <param name="caption">The button caption.</param>
        /// <param name="result">The <see cref="InteractionResult"/> that will be returned by the <see cref="ShowModal"/> method when the button is clicked.</param>
        /// <exception cref="ArgumentException"></exception>
        public void ConfigureButton(int index, string caption, InteractionResult result)
        {
            Button? button = null;

            switch (index)
            {
                case 0:
                    button = buttonLeft;
                    break;

                case 1:
                    button = buttonCentre;
                    break;

                case 2:
                    button = buttonRight;
                    break;

                default:
                    throw new ArgumentException(); // TODO
            }

            if (button != null)
            {
                button.Visible = true;
                button.Text = caption;
                button.Tag = result;
            }
        }

        /// <summary>
        /// Configures the the message box.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message to display.</param>
        /// <param name="image">An <see cref="Image"/> that identifies the type of message being displayed.</param>
        public void ConfigureDialog(string caption, string message, Image image)
        {
            buttonRight.Visible = false;
            buttonCentre.Visible = false;
            buttonLeft.Visible = false;

            labelMessage.Text = message;

            var size = labelMessage.GetPreferredSize(labelMessage.Size);

            var minimumHeight = TOOLBOX_HEIGHT + 3 * MARGIN + IMAGE_HEIGHT + buttonCentre.Height;
            var requiredHeight = TOOLBOX_HEIGHT + 3 * MARGIN + size.Height + buttonCentre.Height;

            if (requiredHeight <= minimumHeight)
            {
                labelMessage.Top = MARGIN + IMAGE_MARGIN + (IMAGE_HEIGHT - TEXT_HEIGHT) / 2;
                Height = minimumHeight;
            }
            else
            {
                labelMessage.Top = MARGIN;
                Height = requiredHeight;
            }

            this.image = image;

            Text = caption;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="view"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Show(IView view)
        {
            throw new InvalidOperationException();
        }


        /// <summary>
        /// Shows the message box as a modal dialog with the specified owner.
        /// </summary>
        /// <param name="owner">The <see cref="IView"/> that owns the message box.</param>
        /// <returns>The <see cref="InteractionResult"/> corresponding to the button that was clicked.</returns>
        /// <exception cref="ArgumentException"></exception>
        public InteractionResult ShowModal(IView owner)
        {
            if (owner is IWin32Window window)
            {
                ShowDialog(window);

                return result;
            }

            throw new ArgumentException(); // TODO
        }

        /// <summary>
        /// TODO 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public string ShowOpenFileDialog(string title, string filter)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="title"></param>
        /// <param name="filter"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public string ShowSaveFileDialog(string title, string filter, string extension)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Event handler for the <see cref="Form.OnPaint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="PaintEventArgs"/> that provides context for the event.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (image != null) e.Graphics.DrawImage(image, MARGIN, MARGIN);

            base.OnPaint(e);
        }

        /// <summary>
        /// Event handler for the <see cref="Button.OnClick"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag != null)
            {
                result =  (InteractionResult)button.Tag;

                Close();
            }
        }
    }
}
