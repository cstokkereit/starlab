namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// This is the base class for all settings group managers.
    /// </summary>
    /// <typeparam name="TView">The <see cref="IChildView"/> used to display the settings sections applicable to this settings group.</typeparam>
    internal abstract class SettingsGroupManager<TView>
        where TView : IChildView
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="SettingsGroupManager{TView}"/> class.
        /// </summary>
        /// <param name="view">The <see cref="IChildView"/> that this class uses to display the settings sections applicable to this settings group.</param>
        /// <param name="group">The name of the settings group managed by this settings manager.</param>
        public SettingsGroupManager(TView view, string group)
        {
            Group = group;
            View = view;
        }

        /// <summary>
        /// Gets the name of the settings group.
        /// </summary>
        public string Group { get; }

        /// <summary>
        /// Gets the <see cref="IChildView"/> that this class uses to display the settings sections applicable to this settings group.
        /// </summary>
        protected TView View { get; }

        /// <summary>
        /// Shows the settings sections required by this group in the target view.
        /// </summary>
        /// <param name="settings">An <see cref="IChartSettings"/> that represents the current state of the chart.</param>
        public abstract void ShowSettings(IChartSettings settings);
    }
}
