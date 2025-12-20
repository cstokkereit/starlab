namespace Stratosoft.Nomenclature.Tests
{
    /// <summary>
    /// TODO
    /// </summary>
    public class NomenclatureManagerTests
    {
        //private static string resourcesPath = string.Empty;
        //private static string testPath = string.Empty;

        [SetUp]
        public static void Initialise()
        {
            //resourcesPath = Path.GetFullPath(Path.Combine(context.DeploymentDirectory, @"..\..\..\Resources"));
            //testPath = context.TestRunDirectory;
        }


        [Test]
        public void TestExportDictionary()
        {
            //var manager = new NomenclatureManager();
        }



        [Test]
        public void TestImportNomenclature()
        {
            //var manager = new NomenclatureManager();

            //manager.ImportDictionary(Path.Combine(resourcesPath, "Nomenclature-1.xml"));

            //Assert.That(1, manager.Nomenclatures.Count);

            //var nomenclature = manager.Nomenclatures["Test Nomenclature"];

            //Assert.IsNotNull(nomenclature);
            //Assert.That(new Guid("4B9A3242-B224-4172-B76A-C2A9A48F09DD"), nomenclature.ID);
            //Assert.That("Test Nomenclature", nomenclature.Name);
            //Assert.That("This is a test nomenclature.", nomenclature.Description);
        }

        [Test]
        public void TestImportNomenclaturesWithSameNameAndId()
        {
            //var manager = new NomenclatureManager();

            //manager.ImportDictionary(Path.Combine(resourcesPath, "Nomenclature-2.xml"));
            //manager.ImportDictionary(Path.Combine(resourcesPath, "Nomenclature-3.xml"));
            
            //Assert.That(1, manager.Nomenclatures.Count);
            //Assert.IsNotNull(manager.Nomenclatures["Test Nomenclature"]);
        }

        [Test]
        public void TestImportNomenclaturesWithSameNameButDifferentIds()
        {
            //var manager = new NomenclatureManager();

            //manager.ImportDictionary(Path.Combine(resourcesPath, "Nomenclature-1.xml"));
            //manager.ImportDictionary(Path.Combine(resourcesPath, "Nomenclature-2.xml"));

            //Assert.That(2, manager.Nomenclatures.Count);
            //Assert.IsNotNull(manager.Nomenclatures["Test Nomenclature"]);
            //Assert.IsNotNull(manager.Nomenclatures["Test Nomenclature(2)"]);
        }
    }
}
