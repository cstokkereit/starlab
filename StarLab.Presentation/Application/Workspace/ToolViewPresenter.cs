﻿using AutoMapper;
using StarLab.Application.Configuration;
using StarLab.Commands;

namespace StarLab.Application.Workspace
{
    public class ToolViewPresenter : Presenter, IDockableViewPresenter, IViewController
    {
        private readonly IDockableView view;

        public ToolViewPresenter(IDockableView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfigurationService configuration, IMapper mapper, IEventAggregator events)
            : base(commands, useCaseFactory, configuration, mapper, events)
        {
            this.view = view;

            Location = Constants.DOCK_RIGHT; // TODO
        }

        public string Location { get; set; }

        public override string Name => view.ID + Constants.CONTROLLER;

        public override void Initialise(IApplicationController controller)
        {
            if (!Initialised)
            {
                base.Initialise(controller);

                view.Initialise(controller);
            }
        }

        public void Show(IView view)
        {
            this.view.Show(view);
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
    }
}
