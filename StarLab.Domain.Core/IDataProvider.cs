namespace StarLab.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Connects to the specified database.
        /// </summary>
        /// <param name="host">The host name.</param>
        /// <param name="database">The database name.</param>
        void Connect(string host, string database);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<IStar> GetStars();
    }
}
