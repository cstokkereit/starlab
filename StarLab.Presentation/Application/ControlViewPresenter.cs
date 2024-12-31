using AutoMapper;
using StarLab.Application.Configuration;
using StarLab.Commands;
using System.Diagnostics;
using System.Drawing.Printing;

namespace StarLab.Application
{
    public abstract class ControlViewPresenter<TView, TParent> : Presenter
        where TParent : IViewController
        where TView : IChildView
    {
        private readonly TView view;

        private TParent? parentController;

        public ControlViewPresenter(TView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfigurationService configuration, IMapper mapper, IEventAggregator events)
            : base(commands, useCaseFactory, configuration, mapper, events)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public override string Name => View.Name + Constants.CONTROLLER;

        protected TParent Parent
        {
            get
            {
                Debug.Assert(parentController != null);
                return parentController;
            }
        }

        public virtual void Initialise(IApplicationController controller, TParent parentController)
        {
            base.Initialise(controller);

            this.parentController = parentController;
        }

        protected TView View => view;

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
            Debug.Assert(parentController != null);

            return parentController.ShowMessage(caption, message, type, responses);
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
            Debug.Assert(parentController != null);

            return parentController.ShowMessage(caption, message, responses);
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified options.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        protected InteractionResult ShowMessage(string caption, string message)
        {
            Debug.Assert(parentController != null);

            return parentController.ShowMessage(caption, message);
        }

        protected string ShowOpenFileDialog(string title, string filter)
        {
            Debug.Assert(parentController != null);

            return parentController.ShowOpenFileDialog(title, filter);
        }

        protected string ShowSaveFileDialog(string title, string filter, string extension)
        {
            Debug.Assert(parentController != null);

            return parentController.ShowSaveFileDialog(title, filter, extension);
        }
    }
}
