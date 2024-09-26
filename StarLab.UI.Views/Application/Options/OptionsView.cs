using StarLab.Presentation;

namespace StarLab.Application.Options
{
    public partial class OptionsView : View, IOptionsView
    {
        private readonly IOptionsViewPresenter presenter;

        public OptionsView(IPresenterFactory presenterFactory)
        {
            InitializeComponent();

            presenter = (IOptionsViewPresenter)presenterFactory.CreatePresenter(this);
        }

        #region IOptionsView Members

        public override void Initialise(IApplicationController controller)
        {
            base.Initialise(controller);

            presenter.Initialise(controller);
        }

        #endregion

        protected override IViewController GetController()
        {
            return presenter;
        }
    }
}
