namespace StarLab.Presentation
{
    /// <summary>
    /// Defines the methods that are common to all ChildViewControllers.
    /// </summary>
    public interface IChildViewController : IController
    {
        /// <summary>
        /// Initialises the controller.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        void Initialise(IApplicationController controller);

        /// <summary>
        /// Registers the <see cref="IViewController"/> that controls the parent view with this controller so that it can call methods on the parent controller.
        /// </summary>
        /// <param name="controller">The <see cref="IViewController"/> that controls the parent view.</param>
        void RegisterController(IViewController controller);

        /// <summary>
        /// Runs the child view.
        /// </summary>
        /// <param name="args">An <see cref="INamedArguments"/> that contains information required to run the view.</param>
        void Run(INamedArguments args);

        /// <summary>
        /// Runs the child view.
        /// </summary>
        void Run();
    }
}
