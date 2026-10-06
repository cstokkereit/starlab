namespace StarLab.Application.Data
{
    /// <summary>
    /// Represents a table that is part of a query.
    /// </summary>
    public interface ITable : IQueryFragment
    {
        /// <summary>
        /// Gets an <see cref="IEnumerable{IField}"/> containing the fields in the table.
        /// </summary>
        IEnumerable<IField> Fields { get; }

        /// <summary>
        /// Determines whether the specified field exists.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>true if the field exists; false otherwise.</returns>
        bool HasField(string name);

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a flag indicating that the values from all fields in this table will be returned by the query.
        /// </summary>
        bool SelectAll { get; }

        /// <summary>
        /// Adds a field with the specified name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>A reference to this <see cref="ITable"/> object to allow fluent modification of the table.</returns>
        ITable AddField(string name);
    }
}
