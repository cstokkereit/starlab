﻿using StarLab.Application;

namespace StarLab.Presentation
{
    /// <summary>
    /// Base interface implemented by all presenters.
    /// </summary>
    public interface IPresenter : IController
    {
        /// <summary>
        /// Initialises the presenter.
        /// </summary>
        /// <param name="controller">The application controller.</param>
        void Initialise(IApplicationController controller);
    }
}
