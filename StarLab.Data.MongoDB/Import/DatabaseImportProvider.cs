using log4net;
using MongoDB.Bson;
using StarLab.Application.Data;
using StarLab.Application.Data.Import;
using StarLab.Shared;

namespace StarLab.Data.MongoDB.Import
{
    /// <summary>
    /// An implementation of the <see cref="IDatabaseImportProvider"/> interface that can be used to import data into a MongoDB database.
    /// </summary>
    public class DatabaseImportProvider : IDatabaseImportProvider
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DatabaseImportProvider)); // The logger that will be used for writing log messages.

        private const int BATCH_SIZE = 1000; // The number of documents that constitutes a batch.

        private readonly IDatabaseManager databases; // Provides access to the MongoDB server.

        /// <summary>
        /// Initialises a new instance of the <see cref="DatabaseImportProvider"/> class.
        /// </summary>
        /// <param name="databases">An <see cref="IDatabaseManager"/> that can be used to access the MongoDB server.</param>
        public DatabaseImportProvider(IDatabaseManager databases)
        {
            this.databases = databases;
        }

        /// <summary>
        /// Imports the data contained in an <see cref="IDataset"/> into the specified collection within a MongoDB database.
        /// </summary>
        /// <param name="source">An <see cref="IForwardOnlyDataset"/> that contains the source data.</param>
        /// <param name="database">The name of the MongoDB database.</param>
        /// <param name="destination">The name of the destination collection.</param>
        public void Import(IForwardOnlyDataset source, string database, string destination)
        {
            if (databases.GetDatabase(database) is Database db)
            {
                var collection = db.GetCollection(destination);

                while (!source.EOF)
                {
                    var documents = GetBatch(source);

                    if (documents.Count > 0) collection.InsertMany(documents);
                }
            }
        }

        /// <summary>
        /// Populates a <see cref="List{BsonDocument}"/> with the number of documents specified by the batch size unless the end of the file has been reached.
        /// </summary>
        /// <param name="dataset">An <see cref="IForwardOnlyDataset"/> that contains the data being imported.</param>
        /// <returns>A <see cref="List{BsonDocument}"/> that contains at most the number of documents specified by the batch size.</returns>
        private static List<BsonDocument> GetBatch(IForwardOnlyDataset source)
        {
            var documents = new List<BsonDocument>();

            var counter = 0;

            while (counter++ < BATCH_SIZE && !source.EOF)
            {
                source.MoveNext();

                if (!source.EOF)
                {
                    documents.Add(CreateDocument(source));
                }
            }

            return documents;
        }

        /// <summary>
        /// Constructs a <see cref="BsonDocument"/> from the values in the <see cref="IEnumerable{IDataField}"/> provided.
        /// </summary>
        /// <param name="fields">An <see cref="IEnumerable{IDataField}"/> containing the fields that comprise the <see cref="BsonDocument"/>.</param>
        /// <returns>A <see cref="BsonDocument"/> constructed from the specified field values.</returns>
        private static BsonDocument CreateDocument(IForwardOnlyDataset source)
        {
            var document = new BsonDocument();

            foreach (var field in source.Fields)
            {
                try
                {
                    document.Add(field.Name, CreateValue(source, field));
                }
                catch (FormatException f)
                {
                    // TODO : Count the number of conversion failures and output the count to the log.
                }
                catch (Exception e)
                {
                    // TODO : Log the errors
                }
            }

            return document;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        private static BsonValue CreateValue(IForwardOnlyDataset source, IDataField field)
        {
            switch (field.DataType)
            {
                case DataTypes.Decimal:
                    return BsonValue.Create(source.GetValue<double>(field));

                case DataTypes.Integer:
                    return BsonValue.Create(source.GetValue<int>(field));

                case DataTypes.Text:
                    return BsonValue.Create(source.GetValue<string>(field));

                default:

                    return BsonValue.Create(string.Empty);
            }

            throw new Exception(ExceptionMessages.UnknownType(field.DataType.ToString()));
        }
    }
}
