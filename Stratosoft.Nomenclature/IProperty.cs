namespace Stratosoft.Nomenclature
{
    /// <summary>
    /// Represents a property of a term.
    /// </summary>
    public interface IProperty
    {
        /// <summary>
        /// Gets the property description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the property ID.
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// Gets the property name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the ID of the parent term.
        /// </summary>
        Guid TermID { get; }
    }
}
