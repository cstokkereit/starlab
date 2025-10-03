using StarLab.Domain;

namespace StarLab.Data.Import
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IImportProvider
    {
        void Import(IDataset dataset);
    }
}
