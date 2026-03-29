using StarLab.Application;
using StarLab.Presentation;
using StarLab.Shared.Properties;

namespace StarLab.UI
{
    /// <summary>
    /// A controller that can be used to display commonly used dialog boxes.
    /// </summary>
    internal static class DialogController
    {
        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified owner and options.
        /// </summary>
        /// <param name="owner">The <see cref="IView"/> that will own the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="type">An <see cref="InteractionType"/> that specifies the type of message being displayed.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public static InteractionResult ShowMessage(IView owner, string caption, string message, InteractionType type, InteractionResponses responses)
        {
            if (owner is Form form) return MessageBox.Show(form, message, caption, responses, type);
            
            throw new ArgumentException(string.Format(Resources.UnexpectedArgumentType, typeof(Form).Name, owner.GetType().Name), nameof(owner));
        }

        /// <summary>
        /// Displays an <see cref="OpenFileDialog"/> with the specified owner and options.
        /// </summary>
        /// <param name="owner">The <see cref="IView"/> that will own the modal dialog box.</param>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <returns>The filename selected in the dialog.</returns>
        public static string ShowOpenFileDialog(IView owner, string title, string filter)
        {
            var filename = string.Empty;

            if (owner is Form form)
            {
                var dialog = new OpenFileDialog
                {
                    AddExtension = false,
                    CheckFileExists = false,
                    CheckPathExists = false,
                    Filter = filter,
                    Multiselect = false,
                    Title = title,
                    ValidateNames = true
                };

                var result = dialog.ShowDialog(form);

                if (result == DialogResult.OK) filename = dialog.FileName;
            }

            return filename;
        }

        /// <summary>
        /// Displays a <see cref="SaveFileDialog"/> with the specified owner and options.
        /// </summary>
        /// <param name="owner">The <see cref="IView"/> that will own the modal dialog box.</param>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <param name="extension">The default file extension.</param>
        /// <returns>The filename selected in the dialog.</returns>
        public static string ShowSaveFileDialog(IView owner, string title, string filter, string extension)
        {
            var filename = string.Empty;

            if (owner is Form form)
            {
                var dialog = new SaveFileDialog
                {
                    AddExtension = true,
                    CheckFileExists = true,
                    CheckPathExists = true,
                    DefaultExt = extension,
                    Filter = filter,
                    OverwritePrompt = true,
                    Title = title,
                    ValidateNames = true
                };

                var result = dialog.ShowDialog(form);

                if (result == DialogResult.OK) filename = dialog.FileName;
            }

            return filename;
        }
    }
}
