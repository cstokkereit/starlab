namespace StarLab.Application
{
    /// <summary>
    /// Defines the properties and methods used by an <see cref="IChildViewPresenter"/> to control the behaviour of a child view. The child view is responsible for the behaviour that is specific to a 
    /// particular dialog, document or tool window while the parent view is responsible for the behaviour that is shared by all dialogs, document or tool windows.
    /// </summary>
    public interface IChildView
    {
        /// <summary>
        /// Gets the <see cref="IChildViewController"/> that controls the view.
        /// </summary>
        IChildViewController Controller { get; }

        /// <summary>
        /// Gets or sets the minimum size of the view.
        /// </summary>
        Size MinimumSize { get; set; }

        /// <summary>
        /// Gets the name of the view.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets the preferred panel, if any, in which to display the view.
        /// </summary>
        SplitViewPanels Panel { get; }

        /// <summary>
        /// Gets the current size of the view.
        /// </summary>
        Size Size { get; }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        void Initialise(IApplicationController controller);
    }
}
