namespace Stratosoft.File.IO
{
    /// <summary>
    /// Represents a file parser that can be used to extract the individual data values from a file.
    /// </summary>
    public abstract class FileParser : IDisposable
    {
        private TextReader reader; // The TextReader used to read the data from the file.

        /// <summary>
        /// Initialises a new instance of the <see cref="FileParser"/> class.
        /// </summary>
        /// <param name="filename">The path to the file containing the data.</param>
        public FileParser(string filename)
        {
            reader = new StreamReader(filename);
        }

        /// <summary>
        /// Closes the <see cref="FileParser"/> and releases any system resources associated with it.
        /// </summary>
        public void Close()
        {
            reader.Close();
        }

        /// <summary>
        /// Releases all resources used by the <see cref="FileParser"/> object.
        /// </summary>
        public void Dispose()
        {
            reader.Dispose();
        }

        /// <summary>
        /// Parses a single row from the file and returns a <see cref="string"/> array containg the data.
        /// </summary>
        /// <returns>A <see cref="string"/> array containing the data.</returns>
        public abstract string[] Parse();

        /// <summary>
        /// Parses the specified number of rows from the file and returns a <see cref="List{string[]}"/>. Each element in the list holds a <see cref="string"/> array containg the data from a single row of data.
        /// </summary>
        /// <param name="count">The number of rows to parse.</param>
        /// <returns>A <see cref="List{string[]}"/> containing the data from the specified rows in the file.</returns>
        public abstract List<string[]> Parse(int count);

        /// <summary>
        /// Parses the entire file and returns a <see cref="List{string[]}"/>. Each element in the list holds a <see cref="string"/> array containg the data from a single row of data.
        /// </summary>
        /// <returns>A <see cref="List{string[]}"/> containing the data from all of the rows in the file.</returns>
        public abstract List<string[]> ParseAll();

        /// <summary>
        /// Reads a line of data from the file.
        /// </summary>
        /// <returns>A <see cref="string"/> containing the line of data or <see cref="null"/> if all lines have been read.</returns>
        protected string? ReadLine()
        {
            return reader.ReadLine();
        }
    }
}
