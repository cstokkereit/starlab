namespace Stratosoft.File.IO
{
    public interface IFileParser : IDisposable
    {
        /// <summary>
        /// Returns <see cref="true"/> if the end of the file has been reached; <see cref="false"/> otherwise.
        /// </summary>
        bool EOF { get; }

        /// <summary>
        /// Gets the value of the field with the specified index.
        /// </summary>
        /// <param name="index">The index of the required field.</param>
        /// <returns>A string representing the specified field value.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        string GetValue(int index);

        /// <summary>
        /// Gets the value of the field with the specified name.
        /// </summary>
        /// <param name="field">The name of the required field.</param>
        /// <returns>A string representing the specified field value.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        string GetValue(string field);

        /// <summary>
        /// Parses the next line of data from the file. If no data is found the <see cref="EOF"/> property will be set to <see cref="true"/>.
        /// </summary>
        void Parse();
    }
}
