﻿using StarLab.Commands;
using StarLab.Presentation;

namespace StarLab.UI.Commands
{
    /// <summary>
    /// A command that shows the specified view.
    /// </summary>
    internal class ShowViewCommand : ComponentCommand<IViewController>
    {
        private readonly IView view;

        public ShowViewCommand(ICommandManager commands, IViewController controller, IView view)
            : base(commands, controller)
        {
            this.view = view;
        }

        public override void Execute()
        {
            receiver.Show(view);
        }
    }
}