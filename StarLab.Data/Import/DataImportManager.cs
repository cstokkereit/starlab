using StarLab.Application.Data.Import;

namespace StarLab.Data.Import
{
    /// <summary>
    /// TODO
    /// </summary>
    public class DataImportManager
    {
        private readonly IImportProvider provider;

        public DataImportManager(IImportProvider provider)
        {
            this.provider = provider;
        }

        public void Import(string filename, IImportDefinition importDefinition)
        {
            using (var dataset = new FileBackedDataset(filename, importDefinition))
            {
                provider.Import(dataset);
            }
        }
    }
}
