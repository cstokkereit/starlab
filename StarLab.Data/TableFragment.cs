namespace StarLab.Data
{
    /// <summary>
    /// A table that forms part of a database query.
    /// </summary>
    public class TableFragment : IQueryFragment, ITable
    {
        private readonly Dictionary<string, IField> fields = new Dictionary<string, IField>(); // A dictionary containing the table fields indexed by name.

        /// <summary>
        /// Initialises a new instance of the <see cref="TableFragment"/> class.
        /// </summary>
        /// <param name="name">The name of the table.</param>
        /// <param name="fields">An <see cref="IEnumerable{IField}"/> that contains the table fields.</param>
        public TableFragment(string name, IEnumerable<IField> fields)
        {
            Name = name;

            foreach (var field in fields)
            {
                AddField(field);
            }
        }

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
        /// Gets the name of the table.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A flag that indicates whether all of the fields in the table have been selected.
        /// </summary>
        public bool SelectAll => fields.Count == 0;

        /// <summary>
        /// Adds an <see cref="IField"/> to the table.
        /// </summary>
        /// <param name="field">RThe <see cref="IField"/> being added.</param>
        /// <returns>A reference to this <see cref="ITable"/> object to allow fluent addition of fields.</returns>
        public ITable AddField(IField field)
        {
            if (fields.ContainsKey(field.Name)) throw new ArgumentException(); // TODO

            if (field.Table != Name)
            {
                fields.Add(field.Name, new FieldFragment(field, Name));
            }
            else
            {
                fields.Add(field.Name, field);
            }

            return this;
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
