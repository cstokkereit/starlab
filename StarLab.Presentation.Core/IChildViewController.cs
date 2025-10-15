namespace StarLab.Presentation
{
    /// <summary>
    /// Defines the methods that are common to all child view controllers.
    /// </summary>
    public interface IChildViewController : IController
    {
        /// <summary>
        /// Activates the view.
        /// </summary>
        void Activate();

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
        /// Runs the controller as part of a use case.
        /// </summary>
        /// <param name="context"></param> TODO - This may not be necessary
        void Run(IInteractionContext context);
    }
}
