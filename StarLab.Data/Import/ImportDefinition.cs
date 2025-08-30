using StarLab.Application.Data.Import;

namespace StarLab.Data.Import
{
    /// <summary>
    /// TODO
    /// </summary>
    internal class ImportDefinition : IImportDefinition
    {
        private const string DEFAULT_NAME = "Import Definition";

        private readonly List<ICompoundFieldDefinition> compoundFields = new List<ICompoundFieldDefinition>();

        private readonly List<IFieldDefinition> fields = new List<IFieldDefinition>();

        private readonly List<string> fieldNames = new List<string>();

        private readonly string delimiter = string.Empty;

        private readonly FileTypes fileType;

        private readonly string textDelimiter = string.Empty;

        public ImportDefinition(FileTypes fileType, string delimiter, string textDelimiter)
        {
            ArgumentException.ThrowIfNullOrEmpty(delimiter);

            this.textDelimiter = textDelimiter;
            this.delimiter = delimiter;
            this.fileType = fileType;

            Name = DEFAULT_NAME;
        }

        public ImportDefinition(FileTypes fileType)
        {
            textDelimiter = string.Empty;
            delimiter = string.Empty;
            this.fileType = fileType;

            Name = DEFAULT_NAME;
        }

        public IReadOnlyList<ICompoundFieldDefinition> CompoundFields
        {
            get { return compoundFields; }
        }

        public IReadOnlyList<IFieldDefinition> Fields
        { 
            get
            {
                fields.Sort((f1, f2) => f1.Index.CompareTo(f2.Index));

                return fields;
            }
        }

        public string Delimiter => delimiter;

        public FileTypes FileType => fileType;

        public string Name { get; set; }

        public string TextDelimiter => textDelimiter;

        public void AddCompoundField(string name, string format, int[] components)
        {
            if (fieldNames.Contains(name)) throw new ArgumentException(); // TODO
 
            compoundFields.Add(new CompoundFieldDefinition(name, format, components));

            fieldNames.Add(name);
        }

        public void AddCompoundField(string name, int[] components)
        {
            if (fieldNames.Contains(name)) throw new ArgumentException(); // TODO

            compoundFields.Add(new CompoundFieldDefinition(name, components));

            fieldNames.Add(name);
        }

        public void AddField(int index, string name, int width, DataTypes dataType)
        {
            if (FileType == FileTypes.DelimitedText) throw new InvalidOperationException();

            if (fieldNames.Contains(name)) throw new ArgumentException(); // TODO

            fields.Add(new FieldDefinition(index, name, width, dataType));

            fieldNames.Add(name);
        }

        public void AddField(int index, string name, DataTypes dataType)
        {
            if (FileType == FileTypes.FixedWidthText) throw new InvalidOperationException();

            if (fieldNames.Contains(name)) throw new ArgumentException(); // TODO

            fields.Add(new FieldDefinition(index, name, dataType));

            fieldNames.Add(name);
        }

        public void ExcludeField(int index, int width)
        {
            fields.Add(new FieldDefinition(index, width));
        }
    }
}
