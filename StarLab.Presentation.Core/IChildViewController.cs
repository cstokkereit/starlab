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
        /// Initiates the workflow managed by the controller.
        /// </summary>
        /// <param name="context">An <see cref="IWorkflowContext"/> that contains the information required to execute the workflow.</param>
        void Run(IWorkflowContext context);
    }
}
