using AutoMapper;
using StarLab.Application.Configuration;
using StarLab.Commands;
using System.Diagnostics;
using System.Drawing;

namespace StarLab.Application
{
    public abstract class ChildViewPresenter<TView, TParent> : Presenter, IChildViewPresenter
        where TParent : IViewController
        where TView : IChildView
    {
        private TParent? parentController;

        public ChildViewPresenter(TView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfigurationService configuration, IMapper mapper, IEventAggregator events)
            : base(commands, useCaseFactory, configuration, mapper, events)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
        }

        public override string Name => View.Name + Constants.CONTROLLER;

        public virtual void RegisterController(IViewController parentController)
        {
            this.parentController = (TParent)parentController;
        }

        public virtual void Run(IInteractionContext context)
        {
            InteractionContext = context; // May not be necessary
        }

        protected IInteractionContext? InteractionContext { get; private set; }

        protected TParent ParentController 
        {
            get
            {
                if (parentController == null) throw new InvalidOperationException(); // TODO - not initialised

                return parentController;
            }
            
            private set { parentController = value; }
        }

        protected TView View { get; }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified options.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="type">An <see cref="InteractionType"/> that specifies the type of message being displayed.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the button that was clicked.</returns>
        protected InteractionResult ShowMessage(string caption, string message, InteractionType type, InteractionResponses responses)
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
        protected InteractionResult ShowMessage(string caption, string message, InteractionResponses responses)
        {
            return ParentController.ShowMessage(caption, message, responses);
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified options.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        protected InteractionResult ShowMessage(string caption, string message)
        {
            return ParentController.ShowMessage(caption, message);
        }

        protected string ShowOpenFileDialog(string title, string filter)
        {
            return ParentController.ShowOpenFileDialog(title, filter);
        }

        protected string ShowSaveFileDialog(string title, string filter, string extension)
        {
            return ParentController.ShowSaveFileDialog(title, filter, extension);
        }
    }
}
