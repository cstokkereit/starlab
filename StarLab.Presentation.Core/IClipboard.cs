namespace StarLab.Presentation
{
    /// <summary>
    /// Represents the clipboard.
    /// </summary>
    public interface IClipboard
    {
        /// <summary>
        /// Returns true if the clipboard currently contains data; false otherwise.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Clears the clipboard.
        /// </summary>
        void Clear();

        /// <summary>
        /// Retrieves text from the clipboard.
        /// </summary>
        /// <returns>The text retrieved from the clipboard.</returns>
        string GetText();

        /// <summary>
        /// Clears the clipboard then adds the specified text to the clipboard.
        /// </summary>
        /// <param name="text">The text to add to the clipboard.</param>
        void SetText(string text);
    }
}
