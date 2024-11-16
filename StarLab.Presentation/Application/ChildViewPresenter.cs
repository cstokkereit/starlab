using AutoMapper;
using StarLab.Commands;
using System.Diagnostics;

namespace StarLab.Application
{
    public abstract class ChildViewPresenter<TView, TParent> : Presenter, IChildViewPresenter 
        where TParent : IViewController
        where TView : IChildView
    {
        private readonly TView view;

        public ChildViewPresenter(TView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(commands, useCaseFactory, configuration, mapper, events)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public override string Name => View.Name + Constants.CONTROLLER;

        public virtual void Attach(IViewController parentController)
        {
            ParentController = (TParent)parentController;
        }

        protected TParent? ParentController { get; private set; }

        protected TView View => view;

        protected DialogResult ShowMessage(string caption, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            Debug.Assert(ParentController != null);

            return ParentController.ShowMessage(caption, message, buttons, icon);
        }

        protected void ShowMessage(string caption, string message, MessageBoxIcon icon)
        {
            Debug.Assert(ParentController != null);

            ParentController.ShowMessage(caption, message, icon);
        }

        protected string ShowOpenFileDialog(string title, string filter)
        {
            Debug.Assert(ParentController != null);

            return ParentController.ShowOpenFileDialog(title, filter);
        }

        protected string ShowSaveFileDialog(string title, string filter, string extension)
        {
            Debug.Assert(ParentController != null);

            return ParentController.ShowSaveFileDialog(title, filter, extension);
        }
    }
}
