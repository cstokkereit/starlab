using StarLab.Application;
using StarLab.Shared.Properties;
using Stratosoft.Commands;

namespace StarLab.Presentation
{
    /// <summary>
    /// The base class for all <see cref="UserControl"> based view presenters. The view controlled by this presenter is a child of a <see cref="Form"/> based parent view.
    /// </summary>
    /// <typeparam name="TView">The <see cref="IChildView"/> controlled by the presenter.</typeparam>
    /// <typeparam name="TParent">The <see cref="IViewController"/> that controls the parent view.</typeparam>
    public abstract class ChildViewPresenter<TView, TParent> : Presenter<TView>, IChildViewPresenter
        where TParent : IViewController
        where TView : IChildView
    {
        private TParent? parentController; // The parent view controller.

        /// <summary>
        /// Initialises a new instance of the <see cref="ChildViewPresenter{TView, TParent}"/> class.
        /// </summary>
        /// <param name="view">The <see cref="TView"/> controlled by the presenter.</param>
        /// <param name="commands">An instance of <see cref="ICommandManager"/> that is required for the creation of commands.</param>
        /// <param name="settings">An <see cref="IApplicationSettings"/> that provides access to the application configuration.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public ChildViewPresenter(TView view, ICommandManager commands, IApplicationSettings settings, IEventAggregator events)
            : base(view, commands, settings, events) 
        {
            ID = Controllers.GetControllerID(view);
        }

        /// <summary>
        /// Gets the controller ID.
        /// </summary>
        public override string ID { get; }

        /// <summary>
        /// Registers the parent <see cref="IViewController"/> with the <see cref="ChildViewPresenter{TView, TParent}"/>.
        /// </summary>
        /// <param name="parentController">An <see cref="IViewController"/> that can be used to control the behaviour of the parent view.</param>
        public virtual void RegisterController(IViewController parentController)
        {
            this.parentController = (TParent)parentController;
        }

        /// <summary>
        /// Gets or sets the <see cref="TParent"/> controller.
        /// </summary>
        protected TParent ParentController
        {
            get
            {
                if (parentController == null) throw new InvalidOperationException(Resources.NotInitialised);

                return parentController;
            }

            private set { parentController = value; }
        }

        /// <summary>
        /// Displays a message box with the specified caption, message, message type and available responses.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="type">An <see cref="InteractionType"/> that specifies the type of message being displayed.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message, InteractionType type, InteractionResponses responses)
        {
            return AppController.ShowMessage(caption, message, type, responses);
        }

        /// <summary>
        /// Displays a message box with the specified caption, message and available responses.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message, InteractionResponses responses)
        {
            return AppController.ShowMessage(caption, message, responses);
        }

        /// <summary>
        /// Displays a message box with the specified caption and message.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        public void ShowMessage(string caption, string message)
        {
            AppController.ShowMessage(caption, message);
        }
    }
}
