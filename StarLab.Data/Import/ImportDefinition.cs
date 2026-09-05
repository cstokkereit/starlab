using StarLab.Application.Data.Import;
using StarLab.Shared;

namespace StarLab.Data.Import
{
    /// <summary>
    /// Provides the information required to import data from a data file into the application.
    /// </summary>
    internal class ImportDefinition : IImportDefinition
    {
        private readonly List<ICompoundFieldDefinition> compoundFields = new List<ICompoundFieldDefinition>(); // A list containing the compound field definitions.

        private readonly List<IFieldDefinition> fields = new List<IFieldDefinition>(); // A list containing the field definitions.

        private readonly List<string> fieldNames = new List<string>(); // A list containing the names of the fields.

        private readonly string delimiter = string.Empty; // The delimiter used to separate fields in a delimited text file.

        private readonly FileTypes fileType; // The type of data file being imported.

        private readonly string textDelimiter = string.Empty; // The delimiter used to identify text fields in a delimited text file.

        /// <summary>
        /// Initialises a new instance of the <see cref="ImportDefinition"/> class.
        /// </summary>
        /// <param name="fileType">A <see cref="FileTypes"/> value specifying the type of data file being imported.</param>
        /// <param name="delimiter">The delimiter used to separate fields in a delimited text file.</param>
        /// <param name="textDelimiter">The delimiter used to identify text fields in a delimited text file.</param>
        public ImportDefinition(FileTypes fileType, string delimiter, string textDelimiter)
        {
            ArgumentException.ThrowIfNullOrEmpty(delimiter, nameof(delimiter));

            this.textDelimiter = textDelimiter;
            this.delimiter = delimiter;
            this.fileType = fileType;

            Name = Constants.NewImportDefinition;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ImportDefinition"/> class.
        /// </summary>
        /// <param name="fileType">A <see cref="FileTypes"/> value specifying the type of data file being imported.</param>
        public ImportDefinition(FileTypes fileType)
        {
            textDelimiter = string.Empty;
            delimiter = string.Empty;
            this.fileType = fileType;

            Name = Constants.NewImportDefinition;
        }

        /// <summary>
        /// Gets an <see cref="IReadOnlyList{ICompoundFieldDefinition}"/> containing the compound field definitions.
        /// </summary>
        public IReadOnlyList<ICompoundFieldDefinition> CompoundFields
        {
            get { return compoundFields; }
        }

        /// <summary>
        /// Gets an <see cref="IReadOnlyList{IFieldDefinition}"/> containing the field definitions.
        /// </summary>
        public IReadOnlyList<IFieldDefinition> Fields
        { 
            get
            {
                fields.Sort((f1, f2) => f1.Index.CompareTo(f2.Index));

                return fields;
            }
        }

        /// <summary>
        /// Specifies the delimiter used to separate fields in a delimited text file.
        /// </summary>
        public string Delimiter => delimiter;

        /// <summary>
        /// Specifies the type of data file being imported.
        /// </summary>
        public FileTypes FileType => fileType;

        /// <summary>
        /// Gets the name of the import definition.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Specifies the delimiter used to identify text fields in a delimited text file.
        /// </summary>
        public string TextDelimiter => textDelimiter;

        /// <summary>
        /// Adds a compound field to the import definition.
        /// </summary>
        /// <param name="name">The name of the compound field.</param>
        /// <param name="format">The format of the compound field.</param>
        /// <param name="components">The components of the compound field.</param>
        /// <exception cref="ArgumentException"></exception>
        public void AddCompoundField(string name, string format, int[] components)
        {
            if (fieldNames.Contains(name)) throw new InvalidOperationException(ExceptionMessages.FieldAlreadyAdded(name));
 
            compoundFields.Add(new CompoundFieldDefinition(name, format, components));

            fieldNames.Add(name);
        }

        /// <summary>
        /// Adds a compound field to the import definition.
        /// </summary>
        /// <param name="name">The name of the compound field.</param>
        /// <param name="components">The components of the compound field.</param>
        /// <exception cref="ArgumentException"></exception>
        public void AddCompoundField(string name, int[] components)
        {
            if (fieldNames.Contains(name)) throw new InvalidOperationException(ExceptionMessages.FieldAlreadyAdded(name));

            compoundFields.Add(new CompoundFieldDefinition(name, components));

            fieldNames.Add(name);
        }

        /// <summary>
        /// Adds a field to the import definition.
        /// </summary>
        /// <param name="index">The index of the field.</param>
        /// <param name="name">The name of the field.</param>
        /// <param name="width">The width of the field.</param>
        /// <param name="dataType">The data type of the field.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void AddField(int index, string name, int width, DataTypes dataType)
        {
            if (FileType == FileTypes.DelimitedText) throw new InvalidOperationException(ExceptionMessages.WidthNotApplicable);

            if (fieldNames.Contains(name)) throw new InvalidOperationException(ExceptionMessages.FieldAlreadyAdded(name));

            if (index < 0) throw new ArgumentException(ExceptionMessages.InvalidFieldIndex);

            fields.Add(new FieldDefinition(index, name, width, dataType));

            fieldNames.Add(name);
        }

        /// <summary>
        /// Adds a field to the import definition.
        /// </summary>
        /// <param name="index">The index of the field.</param>
        /// <param name="name">The name of the field.</param>
        /// <param name="dataType">The data type of the field.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void AddField(int index, string name, DataTypes dataType)
        {
            if (FileType == FileTypes.FixedWidthText) throw new InvalidOperationException(ExceptionMessages.WidthRequired);

            if (fieldNames.Contains(name)) throw new InvalidOperationException(ExceptionMessages.FieldAlreadyAdded(name));

            if (index < 0) throw new ArgumentException(ExceptionMessages.InvalidFieldIndex);

            fields.Add(new FieldDefinition(index, name, dataType));

            fieldNames.Add(name);
        }

        /// <summary>
        /// Adds an excluded field to the import definition.
        /// </summary>
        /// <param name="index">The index of the field to exclude.</param>
        /// <param name="width">The width of the field to exclude.</param>
        public void ExcludeField(int index, int width)
        {
            fields.Add(new FieldDefinition(index, width));
        }
    }
}
