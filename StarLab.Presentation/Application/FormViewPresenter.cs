using AutoMapper;
using StarLab.Application.Events;
using StarLab.Presentation;
using System.ComponentModel;

namespace StarLab.Application
{
    public abstract class FormViewPresenter<T> : Presenter, IFormViewPresenter where T : IFormView
    {
        private readonly T view;

        #region Constructors

        public FormViewPresenter(T view, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(useCaseFactory, configuration, mapper, events)
        {
            this.view = view;
        }

        #endregion

        #region IFormViewPresenter Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ViewClosing(CancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IViewController Members

        public abstract void Show(IView view);

        #endregion

        protected T View { get => view; }

        protected override string GetName()
        {
            return View.Name + Constants.CONTROLLER;
        }
    }
}
