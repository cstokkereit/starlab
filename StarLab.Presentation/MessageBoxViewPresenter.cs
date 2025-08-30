using AutoMapper;
using StarLab.Application;
using StarLab.Presentation.Configuration;
using Stratosoft.Commands;

using ImageResources = StarLab.Presentation.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Presentation
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IMessageBoxView"/>.
    /// </summary>
    public class MessageBoxViewPresenter : Presenter, IMessageBoxViewPresenter, IMessageBoxController
    {
        private readonly IMessageBoxView view; // The view controlled by the presenter.

        /// <summary>
        /// Initialises a new instance of the <see cref="MessageBoxViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IMessageBoxView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="configuration">The <see cref="Configuration.IApplicationConfiguration"/> that will be used to get configuration information.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public MessageBoxViewPresenter(IMessageBoxView view, ICommandManager commands, IUseCaseFactory factory, IApplicationConfiguration configuration, IMapper mapper, IEventAggregator events) 
            : base(commands, factory, configuration, mapper, events) 
        { 
            this.view = view;
        }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public override string Name => ControllerNames.MessageBoxController;

        /// <summary>
        /// Shows the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        public void Show(IView view)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified owner and options.
        /// </summary>
        /// <param name="owner">The <see cref="IView"/> that will own the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="type">An <see cref="InteractionType"/> that specifies the type of message being displayed.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(IView owner, string caption, string message, InteractionType type, InteractionResponses responses)
        {
            switch (type)
            {
                case InteractionType.Error:
                    view.ConfigureDialog(caption, message, ImageResources.Error);
                    break;

                case InteractionType.Info:
                    throw new NotImplementedException();
                    //view.ConfigureDialog(caption, message, ImageResources.Info);
                    break;

                case InteractionType.Question:
                    throw new NotImplementedException();
                    //view.ConfigureDialog(caption, message, ImageResources.Question);
                    break;

                case InteractionType.Warning:
                    throw new NotImplementedException();
                    //view.ConfigureDialog(caption, message, ImageResources.Warning);
                    break;
            }

            ConfigureView(responses);

            return view.ShowModal(owner);
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified owner and options.
        /// </summary>
        /// <param name="owner">The <see cref="IView"/> that will own the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(IView owner, string caption, string message, InteractionResponses responses)
        {
            return ShowMessage(owner, caption, message, InteractionType.Info, responses);
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified owner and options.
        /// </summary>
        /// <param name="owner">The <see cref="IView"/> that will own the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(IView owner, string caption, string message)
        {
            return ShowMessage(owner, caption, message, InteractionResponses.OK);
        }

        /// <summary>
        /// Configures the buttons that correspond to the available responses.
        /// </summary>
        /// <param name="responses">An <see cref="InteractionResponses"/ that specifies the available responses.></param>
        /// <exception cref="ArgumentException"></exception>
        private void ConfigureView(InteractionResponses responses)
        {
            switch (responses)
            {
                case InteractionResponses.OK:
                    view.ConfigureButton(2, StringResources.OK, InteractionResult.OK);
                    break;

                case InteractionResponses.OKCancel:
                    view.ConfigureButton(1, StringResources.OK, InteractionResult.OK);
                    view.ConfigureButton(2, StringResources.Cancel, InteractionResult.Cancel);
                    break;

                case InteractionResponses.YesNo:
                    view.ConfigureButton(1, StringResources.Yes, InteractionResult.Yes);
                    view.ConfigureButton(2, StringResources.No, InteractionResult.No);
                    break;

                case InteractionResponses.YesNoCancel:
                    view.ConfigureButton(0, StringResources.Yes, InteractionResult.Yes);
                    view.ConfigureButton(1, StringResources.No, InteractionResult.No);
                    view.ConfigureButton(2, StringResources.Cancel, InteractionResult.Cancel);
                    break;

                default:
                    throw new ArgumentException();
            }
        }
    }
}
