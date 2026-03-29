using StarLab.Application;

using ImageResources = StarLab.Presentation.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.UI
{
    /// <summary>
    /// A <see cref="Form"/> that implements a message box.
    /// </summary>
    public partial class MessageBox : Form
    {
        // TODO - Some of these constants should be moved to a static class (when we implement another view that needs them)
        private const int TOOLBOX_HEIGHT = 40;

        private const int IMAGE_HEIGHT = 24;

        private const int IMAGE_MARGIN = 3;

        private const int TEXT_HEIGHT = 15;

        private const int MARGIN = 15;

        private InteractionResult result; // The value that will be returned by the Show method when the message box is closed.

        private Image? image; // The image that identifies the type of message being displayed.

        /// <summary>
        /// Initialises a new instance of the <see cref="MessageBox"> class.
        /// </summary>
        private MessageBox(string message, string caption, InteractionResponses responses, InteractionType type)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;

            ConfigureButtons(responses);
            ConfigureLabel(message);
            ConfigureImage(type);

            Text = caption;
        }

        /// <summary>
        /// Shows the form as a modal dialog centered on the specified owner. 
        /// </summary>
        /// <param name="owner">The window that owns the message box.</param>
        /// <param name="message">The message box content.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <param name="type">An <see cref="InteractionType"/> that sepecifes the type of message being displayed.</param>
        /// <returns>An <see cref="InteractionResult"/> that indicates which of the available responses was chosen.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static InteractionResult Show(IWin32Window? owner, string message, string caption, InteractionResponses responses, InteractionType type)
        {
            ArgumentNullException.ThrowIfNull(owner, nameof(owner));

            var dialog = new MessageBox(message, caption, responses, type);

            return dialog.Show(owner);
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
        /// Configures one of the message box response buttons.
        /// </summary>
        /// <param name="index">The index of the button.</param>
        /// <param name="caption">The button caption.</param>
        /// <param name="result">The <see cref="InteractionResult"/> that will be returned by the <see cref="ShowModal"/> method when the button is clicked.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void ConfigureButton(int index, string caption, InteractionResult result)
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
                    throw new ArgumentOutOfRangeException(nameof(result));
            }

            if (button != null)
            {
                button.Visible = true;
                button.Text = caption;
                button.Tag = result;
            }
        }

        /// <summary>
        /// Configures the buttons that correspond to the available responses.
        /// </summary>
        /// <param name="responses">An <see cref="InteractionResponses"/ that specifies the available responses.></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void ConfigureButtons(InteractionResponses responses)
        {
            buttonRight.Visible = false;
            buttonCentre.Visible = false;
            buttonLeft.Visible = false;

            switch (responses)
            {
                case InteractionResponses.OK:
                    ConfigureButton(2, StringResources.OK, InteractionResult.OK);
                    break;

                case InteractionResponses.OKCancel:
                    ConfigureButton(1, StringResources.OK, InteractionResult.OK);
                    ConfigureButton(2, StringResources.Cancel, InteractionResult.Cancel);
                    break;

                case InteractionResponses.YesNo:
                    ConfigureButton(1, StringResources.Yes, InteractionResult.Yes);
                    ConfigureButton(2, StringResources.No, InteractionResult.No);
                    break;

                case InteractionResponses.YesNoCancel:
                    ConfigureButton(0, StringResources.Yes, InteractionResult.Yes);
                    ConfigureButton(1, StringResources.No, InteractionResult.No);
                    ConfigureButton(2, StringResources.Cancel, InteractionResult.Cancel);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(responses));
            }
        }

        /// <summary>
        /// Sets the image according to the type of message being displayed.
        /// </summary>
        /// <param name="type">An <see cref="InteractionType"/> the specifies the type of message being displayed.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void ConfigureImage(InteractionType type)
        {
            switch (type)
            {
                case InteractionType.Error:
                    image = ImageResources.Critical;
                    break;

                case InteractionType.Info:
                    image = ImageResources.Information;
                    break;

                case InteractionType.Question:
                    image = ImageResources.Question;
                    break;

                case InteractionType.Warning:
                    image = ImageResources.Warning;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }

        /// <summary>
        /// Sets the label text and updates the size of the message box so that it fits nicely.
        /// </summary>
        /// <param name="message">The message box content.</param>
        private void ConfigureLabel(string message)
        {
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
                result = (InteractionResult)button.Tag;

                Close();
            }
        }

        /// <summary>
        /// Shows the form as a modal dialog centered on the specified owner. 
        /// NOTE - This function hides the <see cref="Form.Show(IWin32Window? owner)"/> method.
        /// </summary>
        /// <param name="owner">The window that owns the message box.</param>
        /// <returns>An <see cref="InteractionResult"/> that indicates which of the available responses was chosen.</returns>
        private new InteractionResult Show(IWin32Window? owner)
        {
            ShowDialog(owner);

            return result;
        }
    }
}
