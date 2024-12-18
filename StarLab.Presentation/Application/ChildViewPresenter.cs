using AutoMapper;
using StarLab.Application.Configuration;
using StarLab.Commands;
using System.Diagnostics;

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

        protected DialogResult ShowMessage(string caption, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return ParentController.ShowMessage(caption, message, buttons, icon);
        }

        protected void ShowMessage(string caption, string message, MessageBoxIcon icon)
        {
            ParentController.ShowMessage(caption, message, icon);
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
