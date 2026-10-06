using StarLab.Application.Data;
using StarLab.Shared;

namespace StarLab.Data
{
    /// <summary>
    /// A table that forms part of a database query.
    /// </summary>
    internal class TableFragment : IQueryFragment, ITable
    {
        private readonly Dictionary<string, IField> fields = new Dictionary<string, IField>(); // A dictionary containing the table fields indexed by name.

        /// <summary>
        /// Initialises a new instance of the <see cref="TableFragment"/> class.
        /// </summary>
        /// <param name="name">The name of the table.</param>
        public TableFragment(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{IField}"/> that contains the table fields.
        /// </summary>
        public IEnumerable<IField> Fields => fields.Values;

        /// <summary>
        /// Determines whether the specified field exists.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>true if the field exists; false otherwise.</returns>
        public bool HasField(string name)
        {
            return fields.ContainsKey(name);
        }

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A flag that indicates whether all of the fields in the table have been selected.
        /// </summary>
        public bool SelectAll => fields.Count == 0;

        /// <summary>
        /// Adds a field with the specified name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>A reference to this <see cref="ITable"/> object to allow fluent modification of the table.</returns>
        public ITable AddField(string name)
        {
            if (fields.ContainsKey(name)) throw new InvalidOperationException(ExceptionMessages.FieldAlreadyAdded(name));

            fields.Add(name, new FieldFragment(Name, name));

            return this;
        }

        /// <summary>
        /// Adds the <see cref="IField"/> field provided.
        /// </summary>
        /// <param name="field">The field being added.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void AddField(IField field)
        {
            if (field.Table != Name) throw new ArgumentException(ExceptionMessages.InvalidTableName, nameof(field));

            if (fields.ContainsKey(field.Name)) throw new InvalidOperationException(ExceptionMessages.FieldAlreadyAdded(field.Name));

            fields.Add(field.Name, field);
        }

        /// <summary>
        /// Converts the value of the current <see cref="TableFragment"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="TableFragment"/> object.</returns>
        public override string ToString()
        {
            return Name; 
        }
    }
}
