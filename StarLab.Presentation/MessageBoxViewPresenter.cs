using AutoMapper;
using log4net;
using StarLab.Application;
using Stratosoft.Commands;

using ImageResources = StarLab.Presentation.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Presentation
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IMessageBoxView"/>.
    /// </summary>
    public class MessageBoxViewPresenter : Presenter<IMessageBoxView>, IMessageBoxViewPresenter, IMessageBoxController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MessageBoxViewPresenter)); // The logger that will be used for writing log messages.

        /// <summary>
        /// Initialises a new instance of the <see cref="MessageBoxViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IMessageBoxView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="settings">An <see cref="IApplicationSettings"/> that provides access to the application configuration.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public MessageBoxViewPresenter(IMessageBoxView view, ICommandManager commands, IUseCaseFactory factory, IApplicationSettings settings, IMapper mapper, IEventAggregator events) 
            : base(view, commands, factory, settings, mapper, events)
        { 
            View.Attach(this);

            if (log.IsDebugEnabled) log.Debug(string.Format(StringResources.InstanceCreated, nameof(MessageBoxViewPresenter)));
        }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public override string Name => Controllers.MessageBoxController;

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
                    View.ConfigureDialog(caption, message, ImageResources.Error);
                    break;

                case InteractionType.Info:
                    throw new NotImplementedException();
                    //View.ConfigureDialog(caption, message, ImageResources.Info);
                    break;

                case InteractionType.Question:
                    throw new NotImplementedException();
                    //View.ConfigureDialog(caption, message, ImageResources.Question);
                    break;

                case InteractionType.Warning:
                    throw new NotImplementedException();
                    //View.ConfigureDialog(caption, message, ImageResources.Warning);
                    break;
            }

            ConfigureView(responses);

            return View.ShowModal(owner);
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
                    View.ConfigureButton(2, StringResources.OK, InteractionResult.OK);
                    break;

                case InteractionResponses.OKCancel:
                    View.ConfigureButton(1, StringResources.OK, InteractionResult.OK);
                    View.ConfigureButton(2, StringResources.Cancel, InteractionResult.Cancel);
                    break;

                case InteractionResponses.YesNo:
                    View.ConfigureButton(1, StringResources.Yes, InteractionResult.Yes);
                    View.ConfigureButton(2, StringResources.No, InteractionResult.No);
                    break;

                case InteractionResponses.YesNoCancel:
                    View.ConfigureButton(0, StringResources.Yes, InteractionResult.Yes);
                    View.ConfigureButton(1, StringResources.No, InteractionResult.No);
                    View.ConfigureButton(2, StringResources.Cancel, InteractionResult.Cancel);
                    break;

                default:
                    throw new ArgumentException();
            }
        }
    }
}
