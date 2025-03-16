namespace StarLab.Application
{
    /// <summary>
    /// Generates names for controllers.
    /// </summary>
    public static class ControllerNames
    {
        public const string APPLICATION_CONTROLLER = "ApplicationController";
        public const string APPLICATION_VIEW_CONTROLLER = "ApplicationViewController";

        /// <summary>
        /// Generates a name for a content controller.
        /// </summary>
        /// <param name="content">The content name.</param>
        /// <returns>The controller name.</returns>
        public static string GetContentControllerName(string content)
        {
            return $"ContentController({content})";
        }

        /// <summary>
        /// Generates a name for a document controller.
        /// </summary>
        /// <param name="id">The document ID.</param>
        /// <returns>The controller name.</returns>
        public static string GetDocumentControllerName(string id)
        {
            return $"DocumentController({id})";
        }

        /// <summary>
        /// Generates a name for a view controller.
        /// </summary>
        /// <param name="view">The view name.</param>
        /// <returns>The controller name.</returns>
        public static string GetViewControllerName(string view)
        {
            return $"ViewController({view})";
        }
    }
}
