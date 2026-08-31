using StarLab.Presentation;

namespace StarLab.UI
{
    /// <summary>
    /// Implements the <see cref="IClipboard"/> interface providing access to the clipboard object.
    /// </summary>
    internal class Clipboard : IClipboard
    {
        /// <summary>
        /// Returns true if the clipboard currently contains data; false otherwise.
        /// </summary>
        public bool IsEmpty { get; private set; }

        /// <summary>
        /// Clears the clipboard.
        /// </summary>
        public void Clear()
        {
            System.Windows.Forms.Clipboard.Clear();

            IsEmpty = true;
        }

        /// <summary>
        /// Retrieves text from the clipboard.
        /// </summary>
        /// <returns>The text retrieved from the clipboard.</returns>
        public string GetText()
        {
            return System.Windows.Forms.Clipboard.GetText();
        }

        /// <summary>
        /// Clears the clipboard then adds the specified text to the clipboard.
        /// </summary>
        /// <param name="text">The text to add to the clipboard.</param>
        public void SetText(string text)
        {
            System.Windows.Forms.Clipboard.SetText(text);

            IsEmpty = false;
        }
    }
}
