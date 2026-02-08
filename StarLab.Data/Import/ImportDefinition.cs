using StarLab.Application.Data.Import;

namespace StarLab.Data.Import
{
    /// <summary>
    /// TODO
    /// </summary>
    internal class ImportDefinition : IImportDefinition
    {
        private const string DEFAULT_NAME = "New Import Definition"; //

        private readonly List<ICompoundFieldDefinition> compoundFields = new List<ICompoundFieldDefinition>(); //

        private readonly List<IFieldDefinition> fields = new List<IFieldDefinition>(); //

        private readonly List<string> fieldNames = new List<string>(); //

        private readonly string delimiter = string.Empty; //

        private readonly FileTypes fileType; //

        private readonly string textDelimiter = string.Empty; //

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="delimiter"></param>
        /// <param name="textDelimiter"></param>
        public ImportDefinition(FileTypes fileType, string delimiter, string textDelimiter)
        {
            ArgumentException.ThrowIfNullOrEmpty(delimiter, nameof(delimiter));

            this.textDelimiter = textDelimiter;
            this.delimiter = delimiter;
            this.fileType = fileType;

            Name = DEFAULT_NAME;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="fileType"></param>
        public ImportDefinition(FileTypes fileType)
        {
            textDelimiter = string.Empty;
            delimiter = string.Empty;
            this.fileType = fileType;

            Name = DEFAULT_NAME;
        }

        /// <summary>
        /// TODO
        /// </summary>
        public IReadOnlyList<ICompoundFieldDefinition> CompoundFields
        {
            get { return compoundFields; }
        }

        /// <summary>
        /// TODO
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
        /// TODO
        /// </summary>
        public string Delimiter => delimiter;

        /// <summary>
        /// TODO
        /// </summary>
        public FileTypes FileType => fileType;

        /// <summary>
        /// TODO
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// TODO
        /// </summary>
        public string TextDelimiter => textDelimiter;

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="name"></param>
        /// <param name="format"></param>
        /// <param name="components"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddCompoundField(string name, string format, int[] components)
        {
            if (fieldNames.Contains(name)) throw new ArgumentException(); // TODO
 
            compoundFields.Add(new CompoundFieldDefinition(name, format, components));

            fieldNames.Add(name);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="name"></param>
        /// <param name="components"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddCompoundField(string name, int[] components)
        {
            if (fieldNames.Contains(name)) throw new ArgumentException(); // TODO

            compoundFields.Add(new CompoundFieldDefinition(name, components));

            fieldNames.Add(name);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <param name="width"></param>
        /// <param name="dataType"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void AddField(int index, string name, int width, DataTypes dataType)
        {
            if (FileType == FileTypes.DelimitedText) throw new InvalidOperationException();

            if (fieldNames.Contains(name)) throw new ArgumentException(); // TODO

            fields.Add(new FieldDefinition(index, name, width, dataType));

            fieldNames.Add(name);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <param name="dataType"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void AddField(int index, string name, DataTypes dataType)
        {
            if (FileType == FileTypes.FixedWidthText) throw new InvalidOperationException();

            if (fieldNames.Contains(name)) throw new ArgumentException(); // TODO

            fields.Add(new FieldDefinition(index, name, dataType));

            fieldNames.Add(name);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="index"></param>
        /// <param name="width"></param>
        public void ExcludeField(int index, int width)
        {
            fields.Add(new FieldDefinition(index, width));
        }
    }
}
