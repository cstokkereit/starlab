using AutoMapper;
using StarLab.Commands;
using System.ComponentModel;

namespace StarLab.Application
{
    public class DialogViewPresenter : Presenter, IDialogViewPresenter, IDialogController
    {
        private readonly IDialogView view;

        public DialogViewPresenter(IDialogView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(commands, useCaseFactory, configuration, mapper, events)
        {
            this.view = view;
        }

        public override string Name => view.Name + Constants.CONTROLLER;

        public void Close()
        {
            view.Close();
        }

        public override void Initialise(IApplicationController controller)
        {
            base.Initialise(controller);

            view.HideOnClose = true;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ViewClosing(CancelEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
