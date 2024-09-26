using AutoMapper;
using StarLab.Application.Events;
using StarLab.Presentation;
using StarLab.Presentation.Model;

namespace StarLab.Application.Workspace.Documents
{
    public class DocumentViewPresenter : Presenter, IDockableViewPresenter
    {
        private readonly IDockableView view;

        private readonly IDocument document;

        public DocumentViewPresenter(IDockableView view, IDocument document, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(useCaseFactory, configuration, mapper, events)
        {
            this.document = document;
            this.view = view;
        }

        #region IDockableViewPresenter Members

        public string Location { get; set; }

        public override void Initialise(IApplicationController controller)
        {
            base.Initialise(controller);

            Location = Constants.DOCUMENT;
            
            view.Name = document.FullName;
            view.Text = document.Name;

            AttachEventHandlers();
        }

        public void Show(IView view)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected override string GetName()
        {
            return document.Name + Constants.CONTROLLER;
        }

        private void AttachEventHandlers()
        {
            document.NameChanged += OnNameChanged;
        }

        private void OnNameChanged(object? sender, string name)
        {
            view.Name = name;
            view.Text = name;
        }
    }
}
