using AutoMapper;
using StarLab.Commands;

using StarLab.Shared.Properties;

namespace StarLab.Application
{
    public abstract class ControlViewPresenter<T> : Presenter where T : IControlView
    {
        private readonly T view;

        private IDialogController? dialogs;

        public ControlViewPresenter(T view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(commands, useCaseFactory, configuration, mapper, events)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public override string Name => throw new NotImplementedException();

        protected T View => view;

        protected void Initialise(IApplicationController controller, IDialogController dialogs)
        {
            base.Initialise(controller);

            this.dialogs = dialogs;
        }

        protected DialogResult ShowMessage(string caption, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            if (dialogs == null) throw new Exception(Resources.ObjectNotInitialised);

            return dialogs.ShowMessage(caption, message, buttons, icon);
        }

        protected void ShowMessage(string caption, string message, MessageBoxIcon icon)
        {
            if (dialogs == null) throw new Exception(Resources.ObjectNotInitialised);

            dialogs.ShowMessage(caption, message, icon);
        }

        protected string ShowOpenFileDialog(string title, string filter)
        {
            if (dialogs == null) throw new Exception(Resources.ObjectNotInitialised);

            return dialogs.ShowOpenFileDialog(title, filter);
        }

        protected string ShowSaveFileDialog(string title, string filter, string extension)
        {
            if (dialogs == null) throw new Exception(Resources.ObjectNotInitialised);

            return dialogs.ShowSaveFileDialog(title, filter, extension);
        }
    }
}
