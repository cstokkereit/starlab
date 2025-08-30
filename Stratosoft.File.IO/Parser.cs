namespace Stratosoft.File.IO
{
    /// <summary>
    /// Represents a file parser that can be used to extract the individual data values from a file.
    /// </summary>
    public abstract class Parser : IDisposable
    {
        private TextReader? reader; // The TextReader used to read the data from the file.

        /// <summary>
        /// Initialises a new instance of the <see cref="Parser"/> class.
        /// </summary>
        /// <param name="reader">A <see cref="StreamReader"/> that reads characters from a byte stream in a particular encoding.</param>
        public Parser(StreamReader reader)
        {
            this.reader = reader;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Parser"/> class.
        /// </summary>
        /// <param name="filename">The path to the file containing the data.</param>
        public Parser(string filename)
            : this(new StreamReader(filename)) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="Parser"/> class.
        /// </summary>
        /// <param name="stream">A <see cref="Stream"/> that provides a generic view of a sequence of bytes.</param>
        public Parser(Stream stream)
            : this(new StreamReader(stream)) { }

        /// <summary>
        /// Releases all resources used by the <see cref="Parser"/> object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
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
        /// Releases all resources used by the <see cref="Parser"/> object.
        /// </summary>
        /// <param name="disposing"><see cref="true"/> if called by my code; <see cref="false"/> otherwise.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && reader != null)
            {
                reader.Dispose();
                reader = null;
            }
        }

        /// <summary>
        /// Reads a line of data from the file.
        /// </summary>
        /// <returns>A <see cref="string"/> containing the line of data or <see cref="null"/> if all lines have been read.</returns>
        protected string? ReadLine()
        {
            if (reader == null) throw new InvalidOperationException(); // TODO

            return reader.ReadLine();
        }
    }
}
