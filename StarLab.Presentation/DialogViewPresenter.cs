using AutoMapper;
using StarLab.Application;
using Stratosoft.Commands;
using System.ComponentModel;

namespace StarLab.Presentation
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IDialogView"/>.
    /// </summary>
    public class DialogViewPresenter : Presenter, IDialogViewPresenter, IDialogController
    {
        private readonly IDialogView view; // The view controlled by the presenter.

        /// <summary>
        /// Initialises a new instance of the <see cref="DialogViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IDialogView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="configuration">The <see cref="Configuration.IApplicationConfiguration"/> that will be used to get configuration information.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public DialogViewPresenter(IDialogView view, ICommandManager commands, IUseCaseFactory factory, Configuration.IApplicationConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(commands, factory, configuration, mapper, events)
        {
            this.view = view;
        }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public override string Name => ControllerNames.GetViewControllerName(view.Name);

        /// <summary>
        /// Closes the dialog box.
        /// </summary>
        public void Close()
        {
            view.Close();
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

                view.Initialise(controller);

                view.HideOnClose = true;
            }
        }

        /// <summary>
        /// Shows the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        public void Show(IView view)
        {
            this.view.Show(view);
        }

        /// <summary>
        /// Shows the dialog box.
        /// </summary>
        public void Show()
        {
            AppController.Show(view);
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
