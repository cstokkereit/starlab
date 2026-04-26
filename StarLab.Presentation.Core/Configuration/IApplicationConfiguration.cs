namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApplicationConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<IDocumentDefinition> DocumentDefinitions { get; }
    }
}
