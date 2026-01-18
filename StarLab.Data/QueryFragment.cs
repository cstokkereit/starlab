namespace StarLab.Data
{
    /// <summary>
    /// A base implementation for classes that represent part of a query.
    /// </summary>
    public class QueryFragment : IQueryFragment
    {
        protected const string SEPARATOR = ", "; // A separator that can be used when generating a string representation of derived classes.
    }
}
