namespace StarLab.Application
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
            if (owner is Form form) return GetResult(MessageBox.Show(form, message, caption, GetButtons(responses), GetIcon(type)));

            throw new ArgumentException(nameof(owner));
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified owner and options.
        /// </summary>
        /// <param name="owner">The <see cref="IView"/> that will own the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public static InteractionResult ShowMessage(IView owner, string caption, string message, InteractionResponses responses)
        {
            if (owner is Form form) return GetResult(MessageBox.Show(form, message, caption, GetButtons(responses)));

            throw new ArgumentException(nameof(owner));
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified owner and options.
        /// </summary>
        /// <param name="owner">The <see cref="IView"/> that will own the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public static InteractionResult ShowMessage(IView owner, string caption, string message)
        {
            if (owner is Form form) return GetResult(MessageBox.Show(form, message, caption));

            throw new ArgumentException(nameof(owner));
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

        /// <summary>
        /// Converts the <see cref="InteractionResponses"/> enum value provided to the equivalent <see cref="MessageBoxButtons"/> enum value.
        /// </summary>
        /// <param name="responses">The <see cref="InteractionResponses"/> enum value to be converted.</param>
        /// <returns>A <see cref="MessageBoxButtons"/> enum value.</returns>
        /// <exception cref="ArgumentException"></exception>
        private static MessageBoxButtons GetButtons(InteractionResponses responses)
        {
            switch (responses)
            {
                case InteractionResponses.OK:
                    return MessageBoxButtons.OK;

                case InteractionResponses.OKCancel:
                    return MessageBoxButtons.OKCancel;

                case InteractionResponses.YesNo:
                    return MessageBoxButtons.YesNo;

                case InteractionResponses.YesNoCancel:
                    return MessageBoxButtons.YesNoCancel;

                default:
                    throw new ArgumentException(nameof(responses)); // TODO
            }
        }

        /// <summary>
        /// Converts the <see cref="InteractionType"/> enum value provided to the equivalent <see cref="MessageBoxIcon"/> enum value.
        /// </summary>
        /// <param name="type">The <see cref="InteractionType"/> enum value to be converted.</param>
        /// <returns>A <see cref="MessageBoxIcon"/> enum value.</returns>
        /// <exception cref="ArgumentException"></exception>
        private static MessageBoxIcon GetIcon(InteractionType type)
        {
            switch (type)
            {
                case InteractionType.Error:
                    return MessageBoxIcon.Error;

                case InteractionType.Info:
                    return MessageBoxIcon.Information;

                case InteractionType.Question:
                    return MessageBoxIcon.Question;

                case InteractionType.Warning:
                    return MessageBoxIcon.Warning;

                default:
                    throw new ArgumentException(nameof(type)); // TODO
            }
        }

        /// <summary>
        /// Converts the <see cref="DialogResult"/> enum value provided to the equivalent <see cref="InteractionResult"/> enum value.
        /// </summary>
        /// <param name="result">The <see cref="DialogResult"/> enum value to be converted.</param>
        /// <returns>An <see cref="InteractionResult"/> enum value.</returns>
        /// <exception cref="ArgumentException"></exception>
        private static InteractionResult GetResult(DialogResult result)
        {
            switch (result)
            {
                case DialogResult.Cancel:
                    return InteractionResult.Cancel;

                case DialogResult.No:
                    return InteractionResult.No;

                case DialogResult.OK:
                    return InteractionResult.OK;

                case DialogResult.Yes:
                    return InteractionResult.Yes;

                default:
                    throw new ArgumentException(nameof(result)); // TODO
            }
        }
    }
}
