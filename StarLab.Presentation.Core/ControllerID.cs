using StarLab.Application;
using StarLab.Shared.Properties;
using StarLab.Presentation.Workspace;
using StarLab.Presentation.Workspace.Documents;

namespace StarLab.Presentation
{
    /// <summary>
    /// A strongly typed controller ID.
    /// </summary>
    public class ControllerID : ID<IController>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ControllerID"/> class.
        /// </summary>
        /// <param name="id">The ID of the document represented by the view that the controller manages.</param>
        public ControllerID(DocumentID id)
            : base(id.ToString()) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="ControllerID"/> class.
        /// </summary>
        /// <param name="document">The document represented by the view that the controller manages.</param>
        public ControllerID(IDocument document)
            : this(document.ID) { }


        /// <summary>
        /// Initialises a new instance of the <see cref="ControllerID"/> class.
        /// </summary>
        /// <param name="id">The ID of the view that the controller manages.</param>
        public ControllerID(ViewID id)
            : base(id.ToString()) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="ControllerID"/> class.
        /// </summary>
        /// <param name="view">The view that the controller manages.</param>
        public ControllerID(IView view)
            : base(GetID(view)) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="ControllerID"/> class.
        /// </summary>
        /// <param name="value">The ID of the document represented by the view that the controller manages.</param>
        public ControllerID(string value)
            : base(value) { }

        /// <summary>
        /// Generates an ID for a view controller.
        /// </summary>
        /// <param name="view">The <see cref="IView"/>.</param>
        /// <returns>The controller ID.</returns>
        private static string GetID(IView view)
        {
            switch (view)
            {
                case IApplicationView:
                    return $"{view.ID}{Constants.Window}";

                case IDocumentView documentView:
                    return documentView.DocumentID.ToString();

                case IDockableView:
                    return $"{view.ID}{Constants.Tool}";

                case IDialogView:
                    return $"{view.ID}{Constants.Dialog}";

                case IChildView:
                    return view.ID.ToString();

                default:
                    throw new ArgumentException(Resources.UnexpectedViewType, nameof(view));
            }
        }
    }
}
