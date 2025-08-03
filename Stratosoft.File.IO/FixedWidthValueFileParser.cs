
namespace Stratosoft.File.IO
{
    /// <summary>
    /// A class that can be used to import data from a fixed width text file.
    /// </summary>
    public class FixedWidthValueFileParser : FileParser
    {
        private readonly bool trimWhitespace;

        private readonly int[] fieldWidths;

        /// <summary>
        /// Initialises a new instance of the <see cref="FixedWidthValueFileParser"/> class.
        /// </summary>
        /// <param name="filename">The path to the file containing the delimited data.</param>
        /// <param name="fieldWidths">An <see cref="int[]"/> containing the widths of the fields.</param>
        /// <param name="trimWhitespace">If <see cref="true"/> all leading and trailing white-space characters will be removed from the values as they are parsed.</param>
        public FixedWidthValueFileParser(string filename, int[] fieldWidths, bool trimWhitespace)
            : base(filename)
        { 
            this.trimWhitespace = trimWhitespace;
            this.fieldWidths = fieldWidths;
        }

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
                data = GetData(line);
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
            var data = new List<string[]>();

            for (int n = 0; n < count; n++)
            {
                string? line = ReadLine();

                if (line != null)
                {
                    data.Add(GetData(line));
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
            var data = new List<string[]>();

            string? line;

            while ((line = ReadLine()) != null)
            {
                data.Add(GetData(line));
            }

            return data;
        }

        /// <summary>
        /// Splits a line of text containing data into the individual values.
        /// </summary>
        /// <param name="line">A <see cref="string"/> that contains a single line of text from the data file.</param>
        /// <returns>A <see cref="string[]"/> containing the individual values.</returns>
        private string[] GetData(string line)
        {
            var values = new string[fieldWidths.Length];

            int start = 0;

            for (int n = 0; n < fieldWidths.Length; n++)
            {
                if (trimWhitespace)
                {
                    values[n] = line.Substring(start, fieldWidths[n]).Trim();
                }
                else
                {
                    values[n] = line.Substring(start, fieldWidths[n]);
                }
                    
                start += fieldWidths[n];
            }

            return values;
        }
    }
}
