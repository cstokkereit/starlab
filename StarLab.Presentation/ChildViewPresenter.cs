using AutoMapper;
using StarLab.Application;
using StarLab.Presentation.Configuration;
using StarLab.Shared.Properties;
using Stratosoft.Commands;

namespace StarLab.Presentation
{
    /// <summary>
    /// The base class for all <see cref="UserControl"> based view presenters. The view controlled by this presenter is a child of a <see cref="Form"/> based parent view.
    /// </summary>
    /// <typeparam name="TView">The <see cref="IChildView"/> controlled by the presenter.</typeparam>
    /// <typeparam name="TParent">The <see cref="IViewController"/> that controls the parent view.</typeparam>
    public abstract class ChildViewPresenter<TView, TParent> : Presenter, IChildViewPresenter
        where TParent : IViewController
        where TView : IChildView
    {
        private TParent? parentController; // The parent view controller.

        /// <summary>
        /// Initialises a new instance of the <see cref="ChildViewPresenter{TView, TParent}"/> class.
        /// </summary>
        /// <param name="view">The <see cref="TView"/> controlled by the presenter.</param>
        /// <param name="commands">An instance of <see cref="ICommandManager"/> that is required for the creation of commands.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="configuration">The <see cref="IApplicationConfiguration"/> that will be used to get configuration information.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ChildViewPresenter(TView view, ICommandManager commands, IUseCaseFactory factory, IApplicationConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(commands, factory, configuration, mapper, events)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
        }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public override string Name => ControllerNames.GetContentControllerName(View.Name);

        /// <summary>
        /// Registers the parent <see cref="IViewController"/> with the <see cref="ChildViewPresenter{TView, TParent}"/>.
        /// </summary>
        /// <param name="parentController">An <see cref="IViewController"/> that can be used to control the behaviour of the parent view.</param>
        public virtual void RegisterController(IViewController parentController)
        {
            this.parentController = (TParent)parentController;
        }

        /// <summary>
        /// Runs the presenter with the <see cref="IInteractionContext"/> provided.
        /// </summary>
        /// <param name="context">An <see cref="IInteractionContext"/> that provides context for the use case.</param>
        public virtual void Run(IInteractionContext context)
        {
            InteractionContext = context; // May not be necessary
        }

        /// <summary>
        /// Gets or sets the <see cref="IInteractionContext"/>.
        /// </summary>
        protected IInteractionContext? InteractionContext { get; private set; }

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
        /// Gets the <see cref="TView"/> that is controlled by the presenter.
        /// </summary>
        protected TView View { get; }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified options.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="type">An <see cref="InteractionType"/> that specifies the type of message being displayed.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message, InteractionType type, InteractionResponses responses)
        {
            return ParentController.ShowMessage(caption, message, type, responses);
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
            return ParentController.ShowMessage(caption, message, responses);
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified options.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message)
        {
            return ParentController.ShowMessage(caption, message);
        }
    }
}
