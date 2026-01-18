using MongoDB.Bson;
using StarLab.Data.Import;

namespace StarLab.Data.MongoDB.Import
{
    /// <summary>
    /// A MongoDB specific implementation of the <see cref="IImportProvider"/> interface that provides methods for importing data into a MongoDB database.
    /// </summary>
    public class ImportProvider : IImportProvider
    {
        private const int BATCH_SIZE = 1000; // The number of documents that constitutes a batch.

        private readonly Connection connection; // A wrapped connection to the MongoDB server.

        /// <summary>
        /// Initialises a new instance of the <see cref="ImportProvider"/> class.
        /// </summary>
        /// <param name="connection">A <see cref="Connection"/> that can be used to access the MongoDB server.</param>
        public ImportProvider(Connection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Imports the data contained in an <see cref="IDataset"/> into the specified collection within a MongoDB database.
        /// </summary>
        /// <param name="source">An <see cref="IDataset"/> that contains the source data.</param>
        /// <param name="database">The name of the MongoDB database.</param>
        /// <param name="destination">The name of the destination collection.</param>
        public void Import(IDataset source, string database, string destination)
        {
            var collection = connection.GetDatabase(database).GetCollection<BsonDocument>(destination);

            while (!source.EOF)
            {
                var documents = GetBatch(source);

                if (documents.Count > 0) collection.InsertMany(documents);
            }
        }

        /// <summary>
        /// Populates a <see cref="List{BsonDocument}"/> with the number of documents specified by the batch size unless the end of the file has been reached.
        /// </summary>
        /// <param name="dataset">An <see cref="IDataset"/> that contains the data being imported.</param>
        /// <returns>A <see cref="List{BsonDocument}"/> that contains at most the number of documents specified by the batch size.</returns>
        private List<BsonDocument> GetBatch(IDataset dataset)
        {
            var documents = new List<BsonDocument>();

            var counter = 0;

            while (counter++ < BATCH_SIZE && !dataset.EOF)
            {
                dataset.MoveNext();

                if (!dataset.EOF)
                {
                    // TODO - Need to wrap the field conversion in a try catch and throw a new custom exception with the details of the failure to be trapped here and added to a report

                    documents.Add(CreateDocument(dataset.Fields));
                }
            }

            return documents;
        }

        /// <summary>
        /// Constructs a <see cref="BsonDocument"/> from the values in the <see cref="IEnumerable{IDataField}"/> provided.
        /// </summary>
        /// <param name="fields">An <see cref="IEnumerable{IDataField}"/> containing the fields that comprise the <see cref="BsonDocument"/>.</param>
        /// <returns>A <see cref="BsonDocument"/> constructed from the specified field values.</returns>
        private static BsonDocument CreateDocument(IEnumerable<IDataField> fields)
        {
            var document = new BsonDocument();

            foreach (var field in fields)
            {
                document.Add(field.Name, BsonValue.Create(field.Value));
            }

            return document;
        }
    }
}
