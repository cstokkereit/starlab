namespace StarLab.Data
{
    /// <summary>
    /// Represents a database. Provides methods for accessing the data that it contains.
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Retrieves the data specified in the query. If a large amount of data could be returned by the query use the skip and rowLimit parameters to limit the amount of data returned.
        /// </summary>
        /// <param name="query">The <see cref="IQuery"/> that determines which values will be returned.</param>
        /// <param name="skip">The number of records to skip before starting to retrieve records.</param>
        /// <param name="rowLimit">The maximum number of records to retrieve.</param>
        /// <returns>An <see cref="IList{IStar}"/> containg the specified values.</returns>
        IList<IStar> GetStars(IQuery query, int skip, int rowLimit);

        /// <summary>
        /// Retrieves the data specified in the query. This is the preferred method for returning large amounts of data.
        /// </summary>
        /// <param name="query">The <see cref="IQuery"/> that determines which values will be returned.</param>
        /// <returns>An <see cref="IForwardOnlyCursor{IStar}"/> containg the specified values.</returns>
        IForwardOnlyCursor<IStar> GetStars(IQuery query);

        /// <summary>
        /// Closes the database that contains the data.
        /// </summary>
        void CloseDatabase();

        /// <summary>
        /// Opens the specified database.
        /// </summary>
        /// <param name="database">The name of the database.</param>
        void OpenDatabase(string database);
    }
}