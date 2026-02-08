namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// A class for constructing a MongoDB specific implementation of <see cref="IQuery"/> that specifies which records will be returned from a MongoDB database.
    /// </summary>
    public class QueryBuilder : QueryBuilderBase
    {
        /// <summary>
        /// Creates an empty instance of the <see cref="IAndPredicate"/> interface.
        /// </summary>
        /// <returns>An instance of the <see cref="IAndPredicate"/> interface containing no child predicates.</returns>
        public IAndPredicate CreateAndPredicate()
        {
            return new AndFilter();
        }

        /// <summary>
        /// Creates an instance of the <see cref="IAndPredicate"/> interface and initialises it with the predicates contained in the <see cref="IEnumerable{IPredicate}"/> provided.
        /// </summary>
        /// <param name="predicates">An <see cref="IEnumerable{IPredicate}"/> containing the predicates that will be combined using the AND operator.</param>
        /// <returns>An instance of the <see cref="IAndPredicate"/> interface containing the child predicates provided.</returns>
        public IAndPredicate CreateAndPredicate(IEnumerable<IPredicate> predicates)
        {
            return new AndFilter(predicates);
        }

        /// <summary>
        /// Creates an empty instance of the <see cref="IOrPredicate"/> interface.
        /// </summary>
        /// <returns>An instance of the <see cref="IOrPredicate"/> interface containing no child predicates.</returns>
        public IOrPredicate CreateOrPredicate()
        {
            return new OrFilter();
        }

        /// <summary>
        /// Creates an instance of the <see cref="IOrPredicate"/> interface and initialises it with the predicates contained in the <see cref="IEnumerable{IPredicate}"/> provided.
        /// </summary>
        /// <param name="predicates">An <see cref="IEnumerable{IPredicate}"/> containing the predicates that will be combined using the OR operator.</param>
        /// <returns>An instance of the <see cref="IOrPredicate"/> interface containing the child predicates provided.</returns>
        public IOrPredicate CreateOrPredicate(IEnumerable<IPredicate> predicates)
        {
            return new OrFilter(predicates);
        }

        /// <summary>
        /// Creates an instance of <see cref="IField"/> with the specified parent table and name.
        /// </summary>
        /// <param name="table">The name of the table that contains the field.</param>
        /// <param name="name">The name of the field.</param>
        /// <returns>An instance of the <see cref="IOrPredicate"/> interface.</returns>
        public IField CreateField(string table, string name)
        {
            return new FieldFragment(table, name);
        }

        /// <summary>
        /// Creates an instance of <see cref="IField"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>An instance of the <see cref="IOrPredicate"/> interface.</returns>
        public IField CreateField(string name)
        {
            return new FieldFragment(name);
        }

        /// <summary>
        /// Creates an instance of <see cref="IPredicate"/> with the specified properties.
        /// </summary>
        /// <typeparam name="T">The type of the comparison value.</typeparam>
        /// <param name="field">The <see cref="IField"/> containg the values being compared.</param>
        /// <param name="value">The comparison value.</param>
        /// <param name="type">A <see cref="ComparisonOperators"/> that specifies how the value of the field is to be compared to the comparison value.</param>
        /// <returns>An instance of the <see cref="IPredicate"/> interface.</returns>
        /// <exception cref="ArgumentException"></exception>
        public override IPredicate CreatePredicate<T>(IField field, T value, ComparisonOperators type)
        {
            switch (type)
            {
                case ComparisonOperators.Equals:
                    return new EqualsFilter<T>(field, value);

                case ComparisonOperators.GreaterThan:
                    return new GreaterThanFilter<T>(field, value);

                case ComparisonOperators.GreaterThanOrEquals:
                    return new GreaterThanOrEqualsFilter<T>(field, value);

                case ComparisonOperators.LessThan:
                    return new LessThanFilter<T>(field, value);

                case ComparisonOperators.LessThanOrEquals:
                    return new LessThanOrEqualsFilter<T>(field, value);

                case ComparisonOperators.NotEquals:
                    return new NotEqualsFilter<T>(field, value);

                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }

        /// <summary>
        /// Creates an instance of <see cref="ITable"/> with the specified name and fields.
        /// </summary>
        /// <param name="name">The name of the table.</param>
        /// <param name="fields">An <see cref="IEnumerable{IField}"/> containing the table fields.</param>
        /// <returns>An instance of the <see cref="ITable"/> interface containing the fields provided.</returns>
        public ITable CreateTable(string name, IEnumerable<IField> fields)
        {
            return new TableFragment(name, fields);
        }

        /// <summary>
        /// Creates an instance of <see cref="ITable"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the table.</param>
        /// <returns>An instance of the <see cref="ITable"/> interface.</returns>
        public ITable CreateTable(string name)
        {
            return new TableFragment(name);
        }

        /// <summary>
        /// Creates an instance of <see cref="IQuery"/>.
        /// </summary>
        /// <returns>An instance of the <see cref="IQuery"/> interface.</returns>
        protected override IQuery CreateQuery()
        {
            return new Query();
        }
    }
}
