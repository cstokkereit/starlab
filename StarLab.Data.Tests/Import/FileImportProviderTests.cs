using StarLab.Application.Data.Import;

namespace StarLab.Data.Import
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="FileImportProvider"/> class.
    /// </summary>
    internal class FileImportProviderTests
    {
        private readonly string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "DataWithoutHeaders.csv");

        /// <summary>
        /// Test that the <see cref="FileImportProvider()"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var provider = new FileImportProvider();

            Assert.That(provider, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="FileImportProvider.CreateImportDefinitionBuilder(string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreateImportDefinitionBuilderWithDelimiterAndTextDelimiter()
        {
            var provider = new FileImportProvider();

            var builder = provider.CreateImportDefinitionBuilder(",", "\"");

            Assert.That(builder, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="FileImportProvider.CreateImportDefinitionBuilder(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreateImportDefinitionBuilderWithDelimiter()
        {
            var provider = new FileImportProvider();

            var builder = provider.CreateImportDefinitionBuilder(",");

            Assert.That(builder, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="FileImportProvider.CreateImportDefinitionBuilder()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreateImportDefinitionBuilder()
        {
            var provider = new FileImportProvider();

            var builder = provider.CreateImportDefinitionBuilder();

            Assert.That(builder, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="FileImportProvider.ImportData(string, IImportDefinition)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestImportData()
        {
            var provider = new FileImportProvider();

            var builder = provider.CreateImportDefinitionBuilder(",");

            var importDefinition = builder.AddField(0, "Name", DataTypes.Text)
                                          .AddField(1, "Spectral Class", DataTypes.Text)
                                          .AddField(2, "Mv", DataTypes.Decimal)
                                          .Build();

            using (var dataset = provider.ImportData(filename, importDefinition))
            {
                Assert.That(dataset, Is.Not.Null);

                dataset.MoveNext();

                Assert.That(dataset.GetValue<string>("Name"), Is.EqualTo("Achernar"));
                Assert.That(dataset.GetValue<string>("Spectral Class"), Is.EqualTo("B3V"));
                Assert.That(dataset.GetValue<double>("Mv"), Is.EqualTo(-2.3));
            }
        }


        /// <summary>
        /// Test that the <see cref="FileImportProvider.ImportData(string, IImportDefinition)"/> method works correctly.
        /// </summary>
        [Test]
        public async Task TestImportDataAsync()
        {
            var provider = new FileImportProvider();

            var builder = provider.CreateImportDefinitionBuilder(",");

            var importDefinition = builder.AddField(0, "Name", DataTypes.Text)
                                          .AddField(1, "Spectral Class", DataTypes.Text)
                                          .AddField(2, "Mv", DataTypes.Decimal)
                                          .Build();

            var dataset = await provider.ImportDataAsync(filename, importDefinition);

            Assert.That(dataset, Is.Not.Null);

            dataset.MoveNext();

            Assert.That(dataset.GetValue<string>("Name"), Is.EqualTo("Achernar"));
            Assert.That(dataset.GetValue<string>("Spectral Class"), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>("Mv"), Is.EqualTo(-2.3)); 
        }
    }
}
