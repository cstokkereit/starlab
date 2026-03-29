using StarLab.Presentation.Workspace.Documents;

namespace StarLab.Presentation
{
    /// <summary>
    /// Provides names for the various controllers.
    /// </summary>
    public static class Controllers
    {
        public const string ApplicationController = "ApplicationController";
        public const string ApplicationViewController = "ApplicationViewController";

        /// <summary>
        /// Generates an ID for a view controller.
        /// </summary>
        /// <param name="view">The <see cref="IView"/>.</param>
        /// <returns>The controller ID.</returns>
        public static string GetControllerID(IView view)
        {
            string id = string.Empty;

            if (view is IChildView)
            {
                id = $"ContentController({view.ID})";
            }
            else if (view is IDocumentView)
            {
                id = $"DocumentController({view.ID})";
            }
            else
            {
                id = $"{view.ID}Controller";
            }

            return id;
        }
    }
}
