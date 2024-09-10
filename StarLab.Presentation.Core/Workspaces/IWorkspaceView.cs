using StarLab.Commands;
using StarLab.Presentation.Docking;

namespace StarLab.Presentation.Workspaces
{
    /// <summary>
    /// The interface for the Main view.
    /// </summary>
    public interface IWorkspaceView : IFormView, IMenuManager
    {
        /// <summary>
        /// Adds a button to the tool bar.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        /// <param name="tooltip">The tooltip text.</param>
        /// <param name="image">The image to use for the button.</param>
        /// <param name="command">The command to invoke when the button is clicked.</param>
        void AddToolbarButton(string name, string tooltip, Image image, ICommand command);

        /// <summary>
        /// 
        /// </summary>
        void CloseAll();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetLayout();

        void Initialise(IApplicationController controller, IDockableViewFactory factory);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layout"></param>
        void SetLayout(string layout);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        void Show(IDockableView view);
    }
}
