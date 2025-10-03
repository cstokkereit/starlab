namespace StarLab.Presentation
{
    /// <summary>
    /// Provides names for the various controllers.
    /// </summary>
    public static class Controllers
    {
        public const string ApplicationController = "ApplicationController";
        public const string ApplicationViewController = "ApplicationViewController";

        public const string ChartController = "ChartController";
        public const string ChartSettingsController = "ChartSettingsController";

        public const string FilterController = "FilterController";

        public const string MessageBoxController = "MessageBoxController";

        public const string TableController = "TableController";
        public const string TableSettingsController = "TableSettingsController";

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
