using StarLab.Presentation;
using StarLab.Presentation.Options;

namespace StarLab.UI.Options
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
    }
}
