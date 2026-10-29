using StarLab.Shared;

namespace StarLab.UI
{
    /// <summary>
    /// The base class for settings proxies that handle 
    /// </summary>
    /// <typeparam name="SettingsType">The type of settings that this instance acts as a proxy for.</typeparam>
    public abstract class SettingsBase<SettingsType>
    {
        private SettingsType? settings; // The settings that this proxy wraps.

        /// <summary>
        /// Gets the wrapped settings.
        /// </summary>
        protected SettingsType Settings
        {
            get
            {
                return settings ?? throw new InvalidOperationException(ExceptionMessages.ObjectNotInitialised(GetType()));
            }
        }

        /// <summary>
        /// Updates the wrapped settings.
        /// </summary>
        /// <param name="settings">The new settings.</param>
        public void Update(SettingsType settings)
        {
            this.settings = settings;
        }
    }
}
