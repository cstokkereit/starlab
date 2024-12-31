namespace StarLab.Application.Help
{
    /// <summary>
    /// Defines the properties and methods used by the <see cref="IAboutViewPresenter"/> to control the behaviour of the About dialog.
    /// </summary>
    public interface IAboutView : IChildView
    {
        /// <summary>
        /// Sets the name of the company to be displayed in the About dialog.
        /// </summary>
        /// <param name="name">The name of the company.</param>
        void SetCompanyName(string name);

        /// <summary>
        /// Sets the copyright information to be displayed in the About dialog.
        /// </summary>
        /// <param name="copyright">The copyright information.</param>
        void SetCopyright(string copyright);

        /// <summary>
        /// Sets the description of the application to be displayed in the About dialog.
        /// </summary>
        /// <param name="description">The description of the application.</param>
        void SetDescription(string description);

        /// <summary>
        /// Sets the company logo to be displayed in the About dialog.
        /// </summary>
        /// <param name="image">An <see cref="Image"/> of the company logo.</param>
        void SetLogo(Image image);

        /// <summary>
        /// Sets the name of the product to be displayed in the About dialog.
        /// </summary>
        /// <param name="name">The name of the product.</param>
        void SetProductName(string name);

        /// <summary>
        /// Sets the product version number to be displayed in the About dialog.
        /// </summary>
        /// <param name="version">The product version number.</param>
        void SetVersion(string version);
    }
}
