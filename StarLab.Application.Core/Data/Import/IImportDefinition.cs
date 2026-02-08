namespace StarLab.Application.Data.Import
{
    /// <summary>
    /// Represents the format 
    /// </summary>
    public interface IImportDefinition
    {
        IReadOnlyList<ICompoundFieldDefinition> CompoundFields { get; }

        IReadOnlyList<IFieldDefinition> Fields { get; }

        string Delimiter { get; }

        FileTypes FileType {  get; }

        string Name { get; }

        string TextDelimiter { get; }
    }
}
