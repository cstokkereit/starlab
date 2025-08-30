namespace Stratosoft.Nomenclature.Tests
{
    [TestClass]
    public class NomenclatureManagerTests
    {
        private static string resourcesPath = string.Empty;
        private static string testPath = string.Empty;

        [ClassInitialize]
        public static void Initialise(TestContext context)
        {
            resourcesPath = Path.GetFullPath(Path.Combine(context.DeploymentDirectory, @"..\..\..\Resources"));
            testPath = context.TestRunDirectory;
        }


        [TestMethod]
        public void TestExportDictionary()
        {
            
            


            var manager = new NomenclatureManager();



        }



        [TestMethod]
        public void TestImportNomenclature()
        {
            var manager = new NomenclatureManager();

            manager.ImportDictionary(Path.Combine(resourcesPath, "Nomenclature-1.xml"));

            Assert.AreEqual(1, manager.Nomenclatures.Count);

            var nomenclature = manager.Nomenclatures["Test Nomenclature"];

            Assert.IsNotNull(nomenclature);
            Assert.AreEqual(new Guid("4B9A3242-B224-4172-B76A-C2A9A48F09DD"), nomenclature.ID);
            Assert.AreEqual("Test Nomenclature", nomenclature.Name);
            Assert.AreEqual("This is a test nomenclature.", nomenclature.Description);
        }

        [TestMethod]
        public void TestImportNomenclaturesWithSameNameAndId()
        {
            var manager = new NomenclatureManager();

            manager.ImportDictionary(Path.Combine(resourcesPath, "Nomenclature-2.xml"));
            manager.ImportDictionary(Path.Combine(resourcesPath, "Nomenclature-3.xml"));
            
            Assert.AreEqual(1, manager.Nomenclatures.Count);
            Assert.IsNotNull(manager.Nomenclatures["Test Nomenclature"]);
        }

        [TestMethod]
        public void TestImportNomenclaturesWithSameNameButDifferentIds()
        {
            var manager = new NomenclatureManager();

            manager.ImportDictionary(Path.Combine(resourcesPath, "Nomenclature-1.xml"));
            manager.ImportDictionary(Path.Combine(resourcesPath, "Nomenclature-2.xml"));

            Assert.AreEqual(2, manager.Nomenclatures.Count);
            Assert.IsNotNull(manager.Nomenclatures["Test Nomenclature"]);
            Assert.IsNotNull(manager.Nomenclatures["Test Nomenclature(2)"]);
        }
    }
}
