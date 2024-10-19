using AutoMapper;
using StarLab.Application.Events;
using StarLab.Commands;
using StarLab.Presentation;

namespace StarLab.Application.Workspace
{
    public class ToolViewPresenter : Presenter, IDockableViewPresenter, IFormController
    {
        private readonly IDockableView view;

        private readonly string name;

        private readonly string id;

        public ToolViewPresenter(IDockableView view, string id, string name, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(commands, useCaseFactory, configuration, mapper, events)
        {
            this.name = name;
            this.view = view;
            this.id = id;

            Location = Constants.DOCK_RIGHT; // TODO
        }

        public string Location { get; set; }

        public override string Name => id + Constants.CONTROLLER;

        public override void Initialise(IApplicationController controller)
        {
            base.Initialise(controller);

            view.Text = name;
            view.Name = id;
        }

        public void Show(IView view)
        {
            this.view.Show(view);
        }

        public DialogResult ShowMessage(string caption, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return view.ShowMessage(caption, message, buttons, icon);
        }

        public void ShowMessage(string caption, string message, MessageBoxIcon icon)
        {
            view.ShowMessage(caption, message, icon);
        }

        public string ShowOpenFileDialog(string title, string filter)
        {
            return view.ShowOpenFileDialog(title, filter);
        }

        public string ShowSaveFileDialog(string title, string filter, string extension)
        {
            return view.ShowSaveFileDialog(title, filter, extension);
        }
    }
}
