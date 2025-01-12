using AutoMapper;
using StarLab.Application.Configuration;
using StarLab.Commands;
using System.ComponentModel;

namespace StarLab.Application
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
        /// <param name="configuration">The <see cref="IConfigurationService"/> that will be used to get configuration information.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public DialogViewPresenter(IDialogView view, ICommandManager commands, IUseCaseFactory factory, IConfigurationService configuration, IMapper mapper, IEventAggregator events)
            : base(commands, factory, configuration, mapper, events)
        {
            this.view = view;
        }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public override string Name => $"{view.Name}{Constants.CONTROLLER}";

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
        /// Displays a <see cref="MessageBox"/> with the specified options.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="type">An <see cref="InteractionType"/> that specifies the type of message being displayed.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the button that was clicked.</returns>
        public InteractionResult ShowMessage(string caption, string message, InteractionType type, InteractionResponses responses)
        {
            return view.ShowMessage(caption, message, type, responses);
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified options.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message, InteractionResponses responses)
        {
            return view.ShowMessage(caption, message, responses);
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified options.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message)
        {
            return view.ShowMessage(caption, message);
        }

        /// <summary>
        /// Displays an <see cref="OpenFileDialog"/> with the specified options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <returns>The filename selected in the dialog.</returns>
        public string ShowOpenFileDialog(string title, string filter)
        {
            return view.ShowOpenFileDialog(title, filter);
        }

        /// <summary>
        /// Displays a <see cref="SaveFileDialog"/> with the specified options. TODO - May want to split Save and Open file into their own FileManager? also PrintManager? interface maybe Show(IView) as well as only certain views will do this but messages are ubiquitous
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <param name="extension">The default file extension.</param>
        /// <returns>The filename selected in the dialog.</returns>
        public string ShowSaveFileDialog(string title, string filter, string extension)
        {
            return view.ShowSaveFileDialog(title, filter, extension);
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
