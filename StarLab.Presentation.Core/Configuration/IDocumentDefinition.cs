using StarLab.Presentation.Workspace.Documents;

namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IDocumentDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 
        /// </summary>
        string Image { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        string Text { get; }

        /// <summary>
        /// 
        /// </summary>
        DocumentTypes Type { get; }

        /// <summary>
        /// 
        /// </summary>
        string View { get; }
    }
}
