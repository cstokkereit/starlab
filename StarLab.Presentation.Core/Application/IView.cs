namespace StarLab.Application
{
    /// <summary>
    /// This is the base interface for all views.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// 
        /// </summary>
        IViewController Controller { get; }

        string ID { get; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the view text.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        void Initialise(IApplicationController controller);

        /// <summary>
        /// Shows the specified view.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        void Show(IView view);
    }
}
