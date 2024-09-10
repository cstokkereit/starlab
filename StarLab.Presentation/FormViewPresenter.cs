using AutoMapper;
using StarLab.Application.UseCases;
using StarLab.Presentation.Events;
using System.ComponentModel;

namespace StarLab.Presentation
{
    public abstract class FormViewPresenter<T> : Presenter, IFormViewPresenter, IViewController where T : IFormView
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

        public override string Name => View.Name + Constants.CONTROLLER;

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
    }
}
