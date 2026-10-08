using StarLab.Application.Data;
using StarLab.Application.Data.Import;
using StarLab.Shared;
using Stratosoft.File.IO;

namespace StarLab.Data.Import
{
    /// <summary>
    /// An implementation of <see cref="IForwardOnlyDataset"/> that is backed by a fixed width or delimited text file.
    /// </summary>
    public sealed class FileBackedDataset : IForwardOnlyDataset
    {
        private readonly Dictionary<string, FileBackedCompoundDataField> compoundFields = new Dictionary<string, FileBackedCompoundDataField>(); // A dictionary containing the compound fields indexed by name.

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

            if (importDefinition.HeaderRows > 0)
            {
                for (int n = 0; n < importDefinition.HeaderRows; n++)
                {
                    parser.Parse();
                }
            }

            foreach (var field in importDefinition.Fields)
            {
                if (field.Include) fields.Add(new DataField(field));
            }

            foreach (var field in importDefinition.CompoundFields)
            {
                var compoundField = new FileBackedCompoundDataField(field);
                compoundFields.Add(compoundField.Name, compoundField);
                fields.Add(compoundField);
            }

            EOF = false;
            BOF = true;
        }

        /// <summary>
        /// The finaliser will only called if the <see cref="Dispose"/> method has not been called.
        /// </summary>
        ~FileBackedDataset()
        {
            Dispose(false);
        }

        /// <summary>
        /// A flag that indicates that the current row index is before the start of the dataset.
        /// </summary>
        public bool BOF { get; private set; }

        /// <summary>
        /// A flag that indicates that the current row index is beyond the end of the dataset.
        /// </summary>
        public bool EOF { get; private set; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{IDataField}"/> that contains the available data fields.
        /// </summary>
        public IEnumerable<IDataField> Fields => fields;

        /// <summary>
        /// Releases all resources used by the <see cref="FileBackedDataset"/> object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the value of the field with the specified index.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="index">The index of the field.</param>
        /// <returns>The value of the field with the specified index.</returns>
        public T GetValue<T>(int index)
        {
            if (fields[index] is FileBackedCompoundDataField field)
            {
                return (T)Convert.ChangeType(field.GetValue(parser), typeof(T));
            }

            return (T)Convert.ChangeType(parser.GetValue(fields[index].Index), typeof(T));
        }

        /// <summary>
        /// Gets the value of the field with the specified name.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field with the specified name.</returns>
        public T GetValue<T>(string name)
        {
            if (compoundFields.TryGetValue(name, out FileBackedCompoundDataField? field))
            {
                return (T)Convert.ChangeType(field.GetValue(parser), typeof(T));
            }

            return (T)Convert.ChangeType(parser.GetValue(name), typeof(T));
        }

        /// <summary>
        /// Gets the value of the specified field.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="field">The <see cref="IDataField"/> that contains the required value.</param>
        /// <returns>The value of the specified <see cref="IDataField">.</returns>
        public T GetValue<T>(IDataField field)
        {
            if (field is FileBackedCompoundDataField compound)
            {
                return (T)Convert.ChangeType(compound.GetValue(parser), typeof(T));
            }

            return (T)Convert.ChangeType(parser.GetValue(field.Index), typeof(T));
        }

        /// <summary>
        /// Moves the pointer to the next row of data.
        /// </summary>
        public void MoveNext()
        {
            parser.Parse();

            EOF = parser.EOF;

            BOF = false;
        }

        /// <summary>
        /// Releases all resources used by the <see cref="FileBackedDataset"/> object.
        /// </summary>
        /// <param name="disposing">true if called by my code; false otherwise</param>
        private void Dispose(bool disposing)
        {
            if (disposing && parser != null) parser.Dispose();
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
                if (field.Include) map.Add(field.Name, field.Index);
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
                    throw new ArgumentException(ExceptionMessages.UnrecognisedFileType);
            }

            var map = BuildMap(importDefinition);

            return new FileParser(parser, map);
        }
    }
}
