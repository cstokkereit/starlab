using StarLab.Shared.Properties;

namespace Stratosoft.File.IO
{
    /// <summary>
    /// A class that can be used to parse a data file.
    /// </summary>
    public class FileParser : IFileParser
    {
        private readonly Dictionary<string, int>? map; // Maps the field names to their respective array indices.

        private readonly Parser parser; // The file parser that will be used to read the data from the file.

        private string[] data = []; // The data values from a line in the data file.

        /// <summary>
        /// Initialises a new instance of the <see cref="FileParser"/> class.
        /// </summary>
        /// <param name="parser">The <see cref="Parser"/> that will be used to read the data from the file.</param>
        /// <param name="map">A <see cref="Dictionary{string, int}"/> that maps the field names to their respective array indices.</param>
        public FileParser(Parser parser, Dictionary<string, int> map)
        {
            this.parser = parser;
            this.map = map;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="FileParser"/> class.
        /// </summary>
        /// <param name="parser">The <see cref="Parser"/> that will be used to read the data from the file.</param>
        public FileParser(Parser parser)
        {
            this.parser = parser;
        }

        /// <summary>
        /// Returns <see cref="true"/> if the end of the file has been reached; <see cref="false"/> otherwise.
        /// </summary>
        public bool EOF { get; private set; }

        /// <summary>
        /// Releases all resources used by the <see cref="FileParser"/> object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the value of the field with the specified index.
        /// </summary>
        /// <param name="index">The index of the required field.</param>
        /// <returns>A string representing the specified field value.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public string GetValue(int index)
        {
            if (EOF) throw new InvalidOperationException(Resources.EndOfFile);

            return data[index].Trim();
        }

        /// <summary>
        /// Gets the value of the field with the specified name.
        /// </summary>
        /// <param name="field">The name of the required field.</param>
        /// <returns>A string representing the specified field value.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public string GetValue(string field)
        {
            if (map == null) throw new InvalidOperationException(Resources.FieldMapNotSet);

            return GetValue(map[field]);
        }

        /// <summary>
        /// Parses the next line of data from the catalogue file. If no data is found the <see cref="EOF"/> property will be set to <see cref="true"/>.
        /// </summary>
        public void Parse()
        {
            data = parser.Parse();

            if (data.Length == 0)
            {
                EOF = true;
            }
        }

        /// <summary>
        /// Releases all resources used by the <see cref="FileParser"/> object.
        /// </summary>
        /// <param name="disposing"><see cref="true"/> if called by my code; <see cref="false"/> otherwise.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && parser != null) parser.Dispose();
        }
    }
}
