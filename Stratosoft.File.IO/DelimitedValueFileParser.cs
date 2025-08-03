using System.Text;

namespace Stratosoft.File.IO
{
    /// <summary>
    /// A class that can be used to import data from a delimited text file.
    /// </summary>
    public sealed class DelimitedValueFileParser : FileParser
    {
        private readonly string textDelimiter; // The delimiter used to enclose text.

        private readonly string delimiter; // The delimiter used to separate the data.

        /// <summary>
        /// Initialises a new instance of the <see cref="DelimitedValueFileParser"/> class.
        /// </summary>
        /// <param name="filename">The path to the file containing the delimited data.</param>
        /// <param name="delimiter">A <see cref="string"/> specifying the delimiter used to separate the data.</param>
        /// <param name="textDelimiter">A <see cref="string"/> specifying the delimiter used to enclose text.</param>
        public DelimitedValueFileParser(string filename, string delimiter, string textDelimiter)
            : base(filename)
        {
            this.textDelimiter = textDelimiter;
            this.delimiter = delimiter;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="DelimitedValueFileParser"/> class.
        /// </summary>
        /// <param name="filename">The  path to the file containing the delimited data.</param>
        /// <param name="delimiter">A <see cref="char"/> specifying the delimiter used to separate the data.</param>
        /// <param name="textDelimiter">A <see cref="char"/> specifying the delimiter used to enclose text.</param></param>
        public DelimitedValueFileParser(string filename, char delimiter, char textDelimiter)
            : this(filename, delimiter.ToString(), textDelimiter.ToString()) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="DelimitedValueFileParser"/> class.
        /// </summary>
        /// <param name="filename">The  path to the file containing the delimited data.</param>
        /// <param name="delimiter">A <see cref="string"/> specifying the delimiter used to separate the data.</param>
        public DelimitedValueFileParser(string filename, string delimiter)
            : this(filename, delimiter, string.Empty) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="DelimitedValueFileParser"/> class.
        /// </summary>
        /// <param name="filename">The  path to the file containing the delimited data.</param>
        /// <param name="delimiter">A <see cref="char"/> specifying the delimiter used to separate the data.</param>
        public DelimitedValueFileParser(string filename, char delimiter)
            : this(filename, delimiter.ToString()) { }

        /// <summary>
        /// Parses a single row from the file and returns a <see cref="string"/> array containg the data.
        /// </summary>
        /// <returns>A <see cref="string"/> array containing the data.</returns>
        public override string[] Parse()
        {
            string[] data;

            string? line = ReadLine();

            if (line != null)
            {
                data = GetData(line, !string.IsNullOrEmpty(textDelimiter));
            }
            else
            {
                data = [];
            }

            return data;
        }

        /// <summary>
        /// Parses the specified number of rows from the file and returns a <see cref="List{string[]}"/>. Each element in the list holds a <see cref="string"/> array containg the data from a single row of data.
        /// </summary>
        /// <param name="count">The number of rows to parse.</param>
        /// <returns>A <see cref="List{string[]}"/> containing the data from the specified rows in the file.</returns>
        public override List<string[]> Parse(int count)
        {
            var textDelimited = !string.IsNullOrEmpty(textDelimiter);

            var data = new List<string[]>();

            for (int n = 0; n < count; n++)
            {
                string? line = ReadLine();

                if (line != null)
                {
                    data.Add(GetData(line, textDelimited));
                }
                else
                {
                    break;
                }
            }

            return data;
        }

        /// <summary>
        /// Parses the entire file and returns a <see cref="List{string[]}"/>. Each element in the list holds a <see cref="string"/> array containg the data from a single row of data.
        /// </summary>
        /// <returns>A <see cref="List{string[]}"/> containing the data from all of the rows in the file.</returns>
        public override List<string[]> ParseAll()
        {
            var textDelimited = !string.IsNullOrEmpty(textDelimiter);

            var data = new List<string[]>();

            string? line;

            while ((line = ReadLine()) != null)
            {
                data.Add(GetData(line, textDelimited));
            }

            return data;
        }

        /// <summary>
        /// Either adds the contents of the buffer to the growing list of values and clears the buffer or appends a delimiter at the start of a new text value.
        /// </summary>
        /// <param name="isText"><see cref="true"/> if the buffer contains a completed text value that can be added to the list.</param>
        /// <param name="buffer">A <see cref="StringBuilder"/> that contains the growing text value.</param>
        /// <param name="data">A <see cref="List{string}"/> that contains the data values.</param>
        /// <returns><see cref="true"/> if a text value is currently being generated; <see cref="false"/> otherwise.</returns>
        private bool AddValue(bool isText, StringBuilder buffer, List<string> data)
        {
            if (isText)
            {
                data.Add(buffer.ToString());
                buffer.Clear();
            }
            else
            {
                buffer.Append(delimiter);
            }

            return !isText;
        }

        /// <summary>
        /// Either adds the value provided to the growing list of values or appends it to the start of a new text value.
        /// </summary>
        /// <param name="value">The value being added.</param>
        /// <param name="buffer">A <see cref="StringBuilder"/> that contains the growing text value.</param>
        /// <param name="data">A <see cref="List{string}"/> that contains the data values.</param>
        /// <returns><see cref="true"/> if a text value is currently being generated; <see cref="false"/> otherwise.</returns>
        private bool AddValue(string value, StringBuilder buffer, List<string> data)
        {
            bool inText = false;

            if (value.StartsWith(textDelimiter))
            {
                if (value.EndsWith(textDelimiter))
                {
                    data.Add(value.Replace(textDelimiter, string.Empty));
                }
                else
                {
                    buffer.Append(value.Replace(textDelimiter, string.Empty) + delimiter);
                    inText = true;
                }
            }
            else if (value.EndsWith(textDelimiter))
            {
                buffer.Append(value.Replace(textDelimiter, string.Empty));
                data.Add(buffer.ToString());
                buffer.Clear();
            }

            return inText;
        }

        /// <summary>
        /// Either adds the value provided to the growing list of values or appends it to the text value in the buffer.
        /// </summary>
        /// <param name="isText"><see cref="true"/> to append the value provided to the text value in buffer.</param>
        /// <param name="value">The value being added.</param>
        /// <param name="buffer">A <see cref="StringBuilder"/> that contains the growing text value.</param>
        /// <param name="data">A <see cref="List{string}"/> that contains the data values.</param>
        private void AddValue(bool isText, string value, StringBuilder buffer, List<string> data)
        {
            if (isText)
            {
                buffer.Append(value + delimiter);
            }
            else
            {
                data.Add(value);
            }
        }

        /// <summary>
        /// Splits a line of text containing delimited data into the individual values. Delimited text values that happen to contain the value delimiter will not be split.
        /// </summary>
        /// <param name="line">A <see cref="string"/> that contains a single line of delimited values from the data file.</param>
        /// <param name="textDelimited"><see cref="true"/> if the line contains delimited text values.</param>
        /// <returns>A <see cref="string[]"/> containing the individual values.</returns>
        private string[] GetData(string line, bool textDelimited)
        {
            string[] values = line.Split(delimiter);

            if (textDelimited && line.Contains(textDelimiter))
            {
                var buffer = new StringBuilder();
                var data = new List<string>();
                var isText = false;

                for (int n = 0; n < values.Length; n++)
                {
                    var value = values[n];

                    if (value.Equals(textDelimiter))
                    {
                        isText = AddValue(isText, buffer, data);
                    }
                    else if (value.Contains(textDelimiter))
                    {
                        isText = AddValue(value, buffer, data);
                    }
                    else
                    {
                        AddValue(isText, value, buffer, data);
                    }
                }

                values = data.ToArray();
            }

            return values;
        }
    }
}
