using StarLab.Application.Data.Import;
using System.Diagnostics;

namespace StarLab.Data.Import
{
    /// <summary>
    /// TODO
    /// </summary>
    public class ImportDefinitionBuilder : IDelimitedTextImportDefinitionBuilder, IFixedWidthImportDefinitionBuilder
    {
        private ImportDefinition? importDefinition = null;

        private ImportDefinitionBuilder(FileTypes fileType, string delimiter, string textDelimiter)
        {
            importDefinition = new ImportDefinition(fileType, delimiter, textDelimiter);
        }

        private ImportDefinitionBuilder(FileTypes fileType)
        {
            importDefinition = new ImportDefinition(fileType);
        }

        public static IDelimitedTextImportDefinitionBuilder GetInstance(string delimiter, string textDelimiter)
        {
            return new ImportDefinitionBuilder(FileTypes.DelimitedText, delimiter, textDelimiter);
        }

        public static IDelimitedTextImportDefinitionBuilder GetInstance(string delimiter)
        {
            return new ImportDefinitionBuilder(FileTypes.DelimitedText, delimiter, string.Empty);
        }

        public static IFixedWidthImportDefinitionBuilder GetInstance()
        {
            return new ImportDefinitionBuilder(FileTypes.FixedWidthText);
        }

        /// <summary>
        /// Adds a compound field to the import definition.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="format">A format <see cref="string"/> that defines how the values in the component fields will be combined.</param>
        /// <param name="components">The indices of the component fields.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        public IImportDefinitionBuilder AddCompoundField(string name, string format, int[] components)
        {
            Debug.Assert(importDefinition != null);

            importDefinition.AddCompoundField(name, format, components);

            return this;
        }

        /// <summary>
        /// Adds a compound field to the import definition.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="components">The indices of the component fields.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        public IImportDefinitionBuilder AddCompoundField(string name, int[] components)
        {
            Debug.Assert(importDefinition != null);

            importDefinition.AddCompoundField(name, components);

            return this;
        }

        public IDelimitedTextImportDefinitionBuilder AddField(int index, string name, DataTypes dataType)
        {
            Debug.Assert(importDefinition != null);

            importDefinition.AddField(index, name, dataType);

            return this;
        }

        public IFixedWidthImportDefinitionBuilder AddField(int index, string name, int width, DataTypes dataType)
        {
            Debug.Assert(importDefinition != null);

            importDefinition.AddField(index, name, width, dataType);

            return this;
        }

        /// <summary>
        /// Builds the import definition.
        /// </summary>
        /// <returns>The specified <see cref="IImportDefinition"/>.</returns>
        public IImportDefinition Build()
        {
            Debug.Assert(importDefinition != null);

            var retval = importDefinition;
            importDefinition = null;

            return retval;
        }

        public IFixedWidthImportDefinitionBuilder ExcludeField(int index, int width)
        {
            Debug.Assert(importDefinition != null);

            importDefinition.ExcludeField(index, width);

            return this;
        }
    }
}
