using StarLab.Application.Data.Import;
using Stratosoft.File.IO;

namespace StarLab.Data.Import
{
    /// <summary>
    /// An implementation of <see cref="IDataset"/> that is backed by a fixed width or delimited text file.
    /// </summary>
    public sealed class FileBackedDataset : IDataset
    {
        private readonly List<IDataField> fields = new List<IDataField>(); // A list containing the available fields.

        private readonly IFileParser parser; // The file parser that extracts the field values from the data file.

        /// <summary>
        /// Initialises a new instance of the <see cref="FileBackedDataset"/> class.
        /// </summary>
        /// <param name="filename">The path to the data file.</param>
        /// <param name="importDefinition">An <see cref="IImportDefinition"/> that specifies the file format and identifies the fields that need to be imported.</param>
        public FileBackedDataset(string filename, IImportDefinition importDefinition)
        {
            parser = GetParser(filename, importDefinition);

            foreach (var compoundField in importDefinition.CompoundFields)
            {
                fields.Add(new FileBackedCompoundDataField(compoundField, parser));
            }

            foreach (var field in importDefinition.Fields)
            {
                fields.Add(new FileBackedDataField(field, parser));
            }
        }

        /// <summary>
        /// A flag that indicates that the current row index is before the start of the dataset.
        /// </summary>
        public bool BOF => throw new NotImplementedException();

        /// <summary>
        /// A flag that indicates that the current row index is beyond the end of the dataset.
        /// </summary>
        public bool EOF => parser.EOF;

        /// <summary>
        /// Gets an <see cref="IEnumerable{IDataField}"/> that contains the available data fields.
        /// </summary>
        public IEnumerable<IDataField> Fields => fields;

        /// <summary>
        /// Releases all resources used by the <see cref="Parser"/> object.
        /// </summary>
        public void Dispose()
        {
            if (parser != null) parser.Dispose();

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the value of the field with the specified index.
        /// </summary>
        /// <param name="index">The index of the field.</param>
        /// <returns>An <see cref="object"/> that holds the value of the field with the specified index.</returns>
        public object GetValue(int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the value of the field with the specified name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>An <see cref="object"/> that holds the value of the field with the specified name.</returns>
        public object GetValue(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the value of the specified field.
        /// </summary>
        /// <param name="field">The <see cref="IDataField"/> that contains the required value.</param>
        /// <returns>An <see cref="object"/> that holds the value of the field with the specified name.</returns>
        public object GetValue(IDataField field)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Moves the pointer to the specified row index.
        /// </summary>
        /// <param name="index">The new row index.</param>
        public void Move(int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Moves the pointer to the start of the dataset.
        /// </summary>
        public void MoveFirst()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Moves the pointer to the end of the dataset.
        /// </summary>
        public void MoveLast()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Moves the pointer to the next row of data.
        /// </summary>
        public void MoveNext()
        {
            parser.Parse();
        }

        /// <summary>
        /// Moves the pointer to the previous row of data.
        /// </summary>
        public void MovePrevious()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates a <see cref="Dictionary{string, int}}"/> that contains the indices of the fields that need to be imported indexed by name.
        /// </summary>
        /// <param name="importDefinition">An <see cref="IImportDefinition"/> that specifies the file format and identifies the fields that need to be imported.</param>
        /// <returns>A <see cref="Dictionary{string, int}}"/> that contains the indices of the fields that need to be imported indexed by name.</returns>
        private Dictionary<string, int> BuildMap(IImportDefinition importDefinition)
        {
            var map = new Dictionary<string, int>();

            foreach (var field in importDefinition.Fields)
            {
                map.Add(field.Name, field.Index);
            }

            return map;
        }

        /// <summary>
        /// Generates an <see cref="int[]"/> that contains the widths of the fields in a fixed width data file.
        /// </summary>
        /// <param name="importDefinition">An <see cref="IImportDefinition"/> that specifies the file format and identifies the fields that need to be imported.</param>
        /// <returns>An <see cref="int[]"/> that contains the widths of the fields in a fixed width data file.</returns>
        private int[] GetFieldWidths(IImportDefinition importDefinition)
        {
            var widths = new int[importDefinition.Fields.Count];

            foreach (var field in importDefinition.Fields)
            {
                widths[field.Index] = field.Width;
            }

            return widths;
        }

        /// <summary>
        /// Gets an implementation of the <see cref="IFileParser"/> interface that is appropriate for the format of the data file.
        /// </summary>
        /// <param name="filename">The path to the data file.</param>
        /// <param name="importDefinition">An <see cref="IImportDefinition"/> that specifies the file format and identifies the fields that need to be imported.</param>
        /// <returns>An <see cref="IFileParser"/> that can be used to extract the field values from the data file.</returns>
        /// <exception cref="ArgumentException"></exception>
        private IFileParser GetParser(string fileName, IImportDefinition importDefinition)
        {
            Parser? parser = null;

            switch (importDefinition.FileType)
            {
                case FileTypes.DelimitedText:
                    parser = new DelimitedValueParser(fileName, importDefinition.Delimiter, importDefinition.TextDelimiter);
                    break;

                case FileTypes.FixedWidthText:
                    parser = new FixedWidthValueParser(fileName, GetFieldWidths(importDefinition));
                    break;

                default:
                    throw new ArgumentException(); // TODO
            }

            var map = BuildMap(importDefinition);

            return new FileParser(parser, map);
        }
    }
}
