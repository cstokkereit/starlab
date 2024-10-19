namespace StarLab.Application
{
    public interface IControlView
    {
        /// <summary>
        /// 
        /// </summary>
        Size MinimumSize { get; set; }

        /// <summary>
        /// Gets or sets the view text.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Size Size { get; }
    }
}
