namespace StarLab.Application.Data.Import
{
    /// <summary>
    /// A builder that can be used to fluently construct an import definition.
    /// </summary>
    public interface IImportDefinitionBuilder
    {
        /// <summary>
        /// Adds a compound field to the import definition.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="format">A format <see cref="string"/> that defines how the values in the component fields will be combined.</param>
        /// <param name="components">The indices of the component fields.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        IImportDefinitionBuilder AddCompoundField(string name, string format, int[] components);

        /// <summary>
        /// Adds a compound field to the import definition.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <param name="components">The indices of the component fields.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        IImportDefinitionBuilder AddCompoundField(string name, int[] components);

        /// <summary>
        /// Builds the import definition.
        /// </summary>
        /// <returns>The specified <see cref="IImportDefinition"/>.</returns>
        IImportDefinition Build();
    }
}
