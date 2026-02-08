namespace Stratosoft.Nomenclature
{
    /// <summary>
    /// Represents a term from a nomenclature. 
    /// </summary>
    public interface ITerm
    {
        /// <summary>
        /// Gets the term description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the term  ID.
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// Gets the term name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the ID of the parent nomenclature. 
        /// </summary>
        Guid NomenclatureID { get; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{IProperty}"> containing the properties associated with this term.
        /// </summary>
        IEnumerable<IProperty> Properties { get; }

        /// <summary>
        /// Adds the <see cref="IProperty"/> provided to the term.
        /// </summary>
        /// <param name="property">The <see cref="IProperty"/> being added.</param>
        /// <exception cref="ArgumentException"></exception>
        void Add(IProperty property);

        /// <summary>
        /// Gets the specified <see cref="IProperty"/>.
        /// </summary>
        /// <param name="name">The name of the required <see cref="IProperty"/>.</param>
        /// <returns>The specified <see cref="IProperty"/>.</returns>
        IProperty GetProperty(string name);

        /// <summary>
        /// Removes the <see cref="IProperty"/> with the specified name from the term.
        /// </summary>
        /// <param name="name">The name of the <see cref="IProperty"/> being removed.</param>
        void Remove(string name);

        /// <summary>
        /// Removes the specified <see cref="IProperty"/> from the term.
        /// </summary>
        /// <param name="property">The <see cref="IProperty"/> being removed.</param>
        void Remove(IProperty property);
    }
}
