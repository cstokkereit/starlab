namespace StarLab.Application.Help
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IAboutView : IChildView
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyName"></param>
        void SetCompanyName(string companyName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="copyright"></param>
        void SetCopyright(string copyright);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        void SetDescription(string description);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        void SetLogo(Image image);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productName"></param>
        void SetProductName(string productName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        void SetVersion(string version);
    }
}
