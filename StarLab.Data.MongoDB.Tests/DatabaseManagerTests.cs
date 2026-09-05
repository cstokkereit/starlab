using StarLab.Application.Data;
using StarLab.Application.Data.Import;
using StarLab.Data.Import;
using StarLab.Data.MongoDB.Import;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="DatabaseManager"/> class.
    /// </summary>
    public class DatabaseManagerTests
    {
        private readonly IImportDefinition importDefinition;

        /// <summary>
        /// Initialises a new instance of the <see cref="DatabaseManagerTests"/> class.
        /// </summary>
        public DatabaseManagerTests()
        {
            importDefinition = ImportDefinitionBuilder.GetInstance("|")
                .AddCompoundField("ID", "{0}-{1}", [0, 1])
                .Build();
        }

        /// <summary>
        /// Test that the <see cref="DatabaseManager()"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var manager = new DatabaseManager();

            Assert.That(manager, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="DatabaseManager.DropDatabase(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestDropDatabase()
        {
            var manager = new DatabaseManager();

            manager.OpenConnection("localhost", 27017);
            
            ImportData(manager, "test", "stars");

            manager.GetDatabase("test");

            var names0 = manager.GetDatabaseNames();

            manager.DropDatabase("test");
            
            var names1 = manager.GetDatabaseNames();

            Assert.That(names0.Count, Is.EqualTo(4));
            Assert.That(names0, Does.Contain("admin"));
            Assert.That(names0, Does.Contain("config"));
            Assert.That(names0, Does.Contain("local"));
            Assert.That(names0, Does.Contain("test"));

            Assert.That(names1.Count, Is.EqualTo(3));
            Assert.That(names1, Does.Contain("admin"));
            Assert.That(names1, Does.Contain("config"));
            Assert.That(names1, Does.Contain("local"));
            Assert.That(names1, Does.Not.Contain("test"));
        }

        /// <summary>
        /// Test that the <see cref="DatabaseManager.GetDatabase(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestGetDatabase()
        {
            var manager = new DatabaseManager();

            manager.OpenConnection("localhost", 27017);

            var database = manager.GetDatabase("local");

            Assert.That (database, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="DatabaseManager.GetDatabaseNames()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestGetDatabaseNames()
        {
            var manager = new DatabaseManager();

            manager.OpenConnection("localhost", 27017);

            var names = manager.GetDatabaseNames();

            Assert.That(names, Is.Not.Null);
            Assert.That(names.Count, Is.EqualTo(3));
            Assert.That(names, Does.Contain("admin"));
            Assert.That(names, Does.Contain("config"));
            Assert.That(names, Does.Contain("local"));
        }

        /// <summary>
        /// Test that the <see cref="DatabaseManager.OpenConnection(string, int)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestOpenConnection()
        {
            var manager = new DatabaseManager();

            manager.OpenConnection("localhost", 27017);

            Assert.Pass();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="database"></param>
        /// <param name="collection"></param>
        private void ImportData(IDatabaseManager manager, string database, string collection)
        {
            var provider = new ImportManager(manager);

            using (var dataset = new FileBackedDataset(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Stars.dat"), importDefinition))
            {
                provider.Import(dataset, database, collection);
            }
        }
    }
}
