using AutoMapper;
using StarLab.Application.Configuration;
using StarLab.Commands;
using System.Diagnostics;

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

        protected DialogResult ShowMessage(string caption, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            Debug.Assert(parentController != null);

            return parentController.ShowMessage(caption, message, buttons, icon);
        }

        protected void ShowMessage(string caption, string message, MessageBoxIcon icon)
        {
            Debug.Assert(parentController != null);

            parentController.ShowMessage(caption, message, icon);
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
