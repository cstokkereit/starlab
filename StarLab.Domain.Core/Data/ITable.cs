namespace StarLab.Data
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
        /// Gets the name of the table.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a flag indicating that the values from all fields in this table will be returned by the query.
        /// </summary>
        bool SelectAll { get; }

        /// <summary>
        /// Adds an <see cref="IField"/> to the table.
        /// </summary>
        /// <param name="field">The <see cref="IField"/> being added.</param>
        /// <returns>A reference to this <see cref="ITable"/> object to allow fluent modification of the table.</returns>
        ITable AddField(IField field);
    }
}
