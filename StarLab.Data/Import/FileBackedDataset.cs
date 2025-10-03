using StarLab.Application.Data.Import;
using StarLab.Domain;
using Stratosoft.File.IO;

namespace StarLab.Data.Import
{
    /// <summary>
    /// TODO
    /// </summary>
    internal sealed class FileBackedDataset : IDataset
    {
        private readonly List<IDataField> fields = new List<IDataField>();

        private readonly IFileParser parser;

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

        public bool BOF => throw new NotImplementedException();

        public bool EOF => parser.EOF;

        public IEnumerable<IDataField> Fields => fields;

        /// <summary>
        /// Releases all resources used by the <see cref="Parser"/> object.
        /// </summary>
        public void Dispose()
        {
            if (parser != null) parser.Dispose();

            GC.SuppressFinalize(this);
        }

        public object GetValue(int index)
        {
            throw new NotImplementedException();
        }

        public object GetValue(string name)
        {
            throw new NotImplementedException();
        }

        public object GetValue(IDataField field)
        {
            throw new NotImplementedException();
        }

        public void Move(int index)
        {
            throw new NotImplementedException();
        }

        public void MoveFirst()
        {
            throw new NotImplementedException();
        }

        public void MoveLast()
        {
            throw new NotImplementedException();
        }

        public void MoveNext()
        {
            parser.Parse();
        }

        public void MovePrevious()
        {
            throw new NotImplementedException();
        }

        private Dictionary<string, int> BuildMap(IImportDefinition importDefinition)
        {
            var map = new Dictionary<string, int>();

            foreach (var field in importDefinition.Fields)
            {
                map.Add(field.Name, field.Index);
            }

            return map;
        }

        private int[] GetFieldWidths(IImportDefinition importDefinition)
        {
            var widths = new int[importDefinition.Fields.Count];

            foreach (var field in importDefinition.Fields)
            {
                widths[field.Index] = field.Width;
            }

            return widths;
        }

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
