using AutoMapper;
using log4net;
using StarLab.Application;
using StarLab.Presentation.Workspace.Documents;
using StarLab.Shared.Properties;
using Stratosoft.Commands;
using System.ComponentModel;

namespace StarLab.Presentation
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IDialogView"/>.
    /// </summary>
    public class DialogViewPresenter : Presenter<IDialogView>, IDialogViewPresenter, IDialogController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DialogViewPresenter)); // The logger that will be used for writing log messages.

        /// <summary>
        /// Initialises a new instance of the <see cref="DialogViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IDialogView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="settings">An <see cref="IApplicationSettings"/> that provides access to the application configuration.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public DialogViewPresenter(IDialogView view, ICommandManager commands, IUseCaseFactory factory, IApplicationSettings settings, IMapper mapper, IEventAggregator events)
            : base(view, commands, factory, settings, mapper, events)
        {
            View.Attach(this);

            if (log.IsDebugEnabled) log.Debug(string.Format(Resources.InstanceCreated, nameof(DialogViewPresenter)));
        }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public override string Name => Controllers.GetViewControllerName(View.Name);

        /// <summary>
        /// Closes the dialog box.
        /// </summary>
        public void Close()
        {
            View.Close();
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public override void Initialise(IApplicationController controller)
        {
            if (!Initialised)
            {
                base.Initialise(controller);

                View.Initialise(controller);

                View.HideOnClose = true;
            }
        }

        /// <summary>
        /// Shows the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        public void Show(IView view)
        {
            View.Show(view);
        }

        /// <summary>
        /// Shows the dialog box.
        /// </summary>
        public void Show()
        {
            AppController.Show(View);
        }

        /// <summary>
        /// Notifies the presenter that the view is being closed.
        /// </summary>
        /// <param name="e">The <see cref="CancelEventArgs"/> that can be used to determine the reasons that the view is closing and, if necessary, cancel it.</param>
        public void ViewClosing(CancelEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
