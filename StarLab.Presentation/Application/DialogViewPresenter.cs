using AutoMapper;
using StarLab.Application.Configuration;
using StarLab.Commands;
using System.ComponentModel;

namespace StarLab.Application
{
    public class DialogViewPresenter : Presenter, IDialogViewPresenter, IDialogController
    {
        private readonly IDialogView view;

        public DialogViewPresenter(IDialogView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfigurationService configuration, IMapper mapper, IEventAggregator events)
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
            if (!Initialised)
            {
                base.Initialise(controller);

                view.Initialise(controller);

                view.HideOnClose = true;
            }
        }

        public void Show(IView view)
        {
            this.view.Show(view);
        }

        public void Show()
        {
            AppController.Show(view);
        }

        public InteractionResult ShowMessage(string caption, string message, InteractionType type, InteractionResponses responses)
        {
            return view.ShowMessage(caption, message, type, responses);
        }

        public InteractionResult ShowMessage(string caption, string message, InteractionResponses responses)
        {
            return view.ShowMessage(caption, message, responses);
        }

        public InteractionResult ShowMessage(string caption, string message)
        {
            return view.ShowMessage(caption, message);
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
