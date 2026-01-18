namespace StarLab.Data
{
    /// <summary>
    /// A data field that forms part of a database query.
    /// </summary>
    public class FieldFragment : IQueryFragment, IField
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="FieldFragment"/> class.
        /// </summary>
        /// <param name="field">An <see cref="IField"/> that is acting as a template for the field.</param>
        /// <param name="table">The name of the table that contains the field.</param>
        public FieldFragment(IField field, string table)
        {
            Name = field.Name;
            Table = table;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="FieldFragment"/> class.
        /// </summary>
        /// <param name="table">The name of the table that contains the field.</param>
        /// <param name="name">The name of the field.</param>
        public FieldFragment(string table, string name)
        {
            Table = table;
            Name = name;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="FieldFragment"/> class.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        public FieldFragment(string name)
        {
            Table = string.Empty;
            Name = name; 
        }

        /// <summary>
        /// Gets the table qualified field name.
        /// </summary>
        public string FullName => $"{Table}.{Name}";

        /// <summary>
        /// Gets the field name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the name of the table that contains the field.
        /// </summary>
        public string Table { get; }

        /// <summary>
        /// Converts the value of the current <see cref="FieldFragment"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="FieldFragment"/> object.</returns>
        public override string ToString()
        {
            return FullName;
        }
    }
}
