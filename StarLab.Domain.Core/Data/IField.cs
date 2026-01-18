namespace StarLab.Data
{
    /// <summary>
    /// Represents a data field that is part of a query.
    /// </summary>
    public interface IField : IQueryFragment
    {
        /// <summary>
        /// Gets the table qualified field name.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Gets the field name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the name of the table that contains the field.
        /// </summary>
        string Table { get; }
    }
}
