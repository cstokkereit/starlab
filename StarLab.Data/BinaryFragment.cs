namespace StarLab.Data
{
    /// <summary>
    /// A base implementation of the <see cref="IQueryFragment"/> interface that represents part of a query made up of two parts.
    /// </summary>
    public abstract class BinaryFragment : IQueryFragment
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="BinaryFragment"/> class.
        /// </summary>
        /// <param name="lhs">The left hand side of the query fragment.</param>
        /// <param name="rhs">The right hand side of the query fragment.</param>
        public BinaryFragment(IQueryFragment lhs, IQueryFragment rhs)
        {
            RHS = rhs;
            LHS = lhs;
        }

        /// <summary>
        /// Gets the right hand side of the query fragment.
        /// </summary>
        protected IQueryFragment RHS { get; }

        /// <summary>
        /// Gets the left hand side of the query fragment.
        /// </summary>
        protected IQueryFragment LHS { get; }
    }
}
