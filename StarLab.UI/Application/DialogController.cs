namespace StarLab.Application
{
    /// <summary>
    /// TODO
    /// </summary>
    internal static class DialogController
    {
        /// <summary>
        /// Displays a message box with the specified text, caption, buttons and icon.
        /// </summary>
        /// <param name="owner">The view that owns the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="buttons">A <see cref="MessageBoxButtons"/> that specifies which buttons to include on the meeage box.</param>
        /// <param name="icon">A <see cref="MessageBoxIcon"/> that specifies the icon to include on the meeage box.</param>
        /// <returns>A <see cref="DialogResult"/> that identifies the button that was clicked.</returns>
        public static DialogResult ShowMessage(IView owner, string caption, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            if (owner is Form form)
                return MessageBox.Show(form, message, caption, buttons, icon);

            throw new ArgumentException(nameof(owner));
        }

        /// <summary>
        /// Displays a message box with the specified text, caption and icon.
        /// </summary>
        /// <param name="owner">The view that owns the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="icon">A <see cref="MessageBoxIcon"/> that specifies the icon to include on the meeage box.</param>
        public static void ShowMessage(IView owner, string caption, string message, MessageBoxIcon icon)
        {
            ShowMessage(owner, caption, message, MessageBoxButtons.OK, icon);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="title"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="title"></param>
        /// <param name="filter"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
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
