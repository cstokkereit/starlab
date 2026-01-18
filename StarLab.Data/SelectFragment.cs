using System.Text;

namespace StarLab.Data
{
    /// <summary>
    /// A base implementation of the <see cref="ISelect"/> interface.
    /// </summary>
    public abstract class SelectFragment : QueryFragment, ISelect
    {
        protected readonly Dictionary<string, ITable> tables = new Dictionary<string, ITable>(); // A list containing the tables that contain the selected fields.

        public event EventHandler<string>? TableAdded; // An event that notifies subscribers whenever a table is added.

        /// <summary>
        /// Initialises a new instance of the <see cref="SelectFragment"/> class.
        /// </summary>
        /// <param name="table">The name of the table containing the selected fields.</param>
        public SelectFragment(bool distinct)
        {
            Distinct = distinct;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="SelectFragment"/> class.
        /// </summary>
        public SelectFragment()
            : this(false) { }

        /// <summary>
        /// A flag indicating that only unique values or value combinations should be returned.
        /// </summary>
        public bool Distinct { get; }

        /// <summary>
        /// Gets a list containing the tables from which fields have been selected.
        /// </summary>
        public IReadOnlyList<ITable> Tables => new List<ITable>(tables.Values);

        /// <summary>
        /// Adds an <see cref="IField"/> from the specified table to the query.
        /// </summary>
        /// <param name="table">The name of the table containing the field to be added to the query.</param>
        /// <param name="field">The <see cref="IField"/> being added to the query.</param>
        public virtual void AddField(string table, IField field)
        {
            if (!tables.ContainsKey(table))
            {
                tables.Add(table, new TableFragment(table));

                TableAdded?.Invoke(this, table);
            }

            tables[table].AddField(field);
        }

        /// <summary>
        /// Adds all of the fields from the <see cref="ITable"/> provided to the query.
        /// </summary>
        /// <param name="table">The <see cref="ITable"/> containing the fields being added to the query.</param>
        public virtual void AddTable(ITable table)
        {
            if (tables.ContainsKey(table.Name)) throw new ArgumentException(); // TODO

            tables.Add(table.Name, table);

            TableAdded?.Invoke(this, table.Name);
        }

        /// <summary>
        /// Converts the value of the current <see cref="SelectFragment"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="SelectFragment"/> object.</returns>
        public override string ToString()
        {
            return $"SELECT {GetFields()}";
        }

        /// <summary>
        /// Generates a comma separated list containing the names of the selected fields.
        /// </summary>
        /// <returns>A string value that holds a comma separated list containing the names of the selected fields.</returns>
        private string GetFields()
        {
            var builder = new StringBuilder();

            var useFullNames = tables.Count > 1;

            var first = true;

            foreach (var table in tables.Values)
            {
                if (table.SelectAll)
                {
                    if (!first) builder.Append(SEPARATOR);

                    builder.Append(useFullNames ? $"{table.Name}.*" : "*");

                    first = false;
                }
                else
                {
                    foreach (var field in table.Fields)
                    {
                        if (!first) builder.Append(SEPARATOR);

                        builder.Append(useFullNames ? field.FullName : field.Name);

                        first = false;
                    }
                } 
            }

            return builder.ToString();
        }
    }
}
