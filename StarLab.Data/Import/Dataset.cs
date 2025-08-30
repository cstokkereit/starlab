using StarLab.Application.Data.Import;
using Stratosoft.File.IO;

namespace StarLab.Data.Import
{
    /// <summary>
    /// TODO
    /// </summary>
    internal class Dataset : IDataset
    {
        private readonly List<IDataField> fields = new List<IDataField>();

        private readonly IFileParser parser;

        public Dataset(string filename, IImportDefinition importDefinition)
        {
            parser = GetParser(filename, importDefinition);

            foreach (var compoundField in importDefinition.CompoundFields)
            {
                fields.Add(new CompoundDataField(compoundField, parser));
            }

            foreach (var field in importDefinition.Fields)
            {
                fields.Add(new DataField(field, parser));
            }
        }

        public bool EOF => parser.EOF;

        public IEnumerable<IDataField> Fields => fields;

        /// <summary>
        /// Releases all resources used by the <see cref="Parser"/> object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public void MoveNext()
        {
            parser.Parse();
        }

        /// <summary>
        /// Releases all resources used by the <see cref="Dataset"/> object.
        /// </summary>
        /// <param name="disposing"><see cref="true"/> if called by my code; <see cref="false"/> otherwise.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && parser != null) parser.Dispose();
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
