using StarLab.Application.Data.Import;

namespace StarLab.Data.Import
{
    /// <summary>
    /// TODO
    /// </summary>
    public class DataImportManagerTests
    {
        private readonly IImportDefinition importDefinition;

        public DataImportManagerTests()
        {
            importDefinition = ImportDefinitionBuilder.GetInstance("|")
                .AddField(5, "Apparent Magnitude", DataTypes.Decimal)
                .AddField(8, "RightAscension", DataTypes.Decimal)
                .AddField(9, "Declination", DataTypes.Decimal)
                .AddField(11, "Parallax", DataTypes.Decimal)
                .AddField(37, "B-V", DataTypes.Decimal)
                .AddField(40, "V-I", DataTypes.Decimal)
                .AddField(76, "Spectral Type", DataTypes.Text)
                .AddCompoundField("ID", "{0}-{1}", [0, 1])
                .Build();
        }

        [Test]
        public void TestConstructor()
        {
            var importer = new DataImportManager(new ImportProvider());

            Assert.That(importer, Is.Not.Null);
        }

        //[Ignore("Needs a test database or mock implementation of one")]
        [Test]
        public void TestImport()
        {
            var importer = new DataImportManager(new ImportProvider());

            importer.Import("D:\\Documents\\Science\\Astronomy\\Catalogs\\Hipparcos\\hip_main.dat", importDefinition);
        }
    }
}
