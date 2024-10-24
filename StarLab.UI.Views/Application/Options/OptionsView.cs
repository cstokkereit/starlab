namespace StarLab.Application.Options
{
    public partial class OptionsView : UserControl, IOptionsView
    {
        private readonly IOptionsViewPresenter presenter;

        public OptionsView(IPresenterFactory factory)
        { 
            InitializeComponent();

            Name = Views.OPTIONS;

            presenter = (IOptionsViewPresenter)factory.CreatePresenter(this);
        }

        public void Initialise(IApplicationController controller, IFormController parentController)
        {
            presenter.Initialise(controller);
        }
    }
}
