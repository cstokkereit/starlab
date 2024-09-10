namespace StarLab.Presentation.Help
{
    public interface IAboutView : IFormView
    {
        void SetCompanyName(string companyName);

        void SetCopyright(string copyright);

        void SetDescription(string description);

        void SetLogo(Image image);

        void SetProductName(string productName);

        void SetVersion(string version);
    }
}
