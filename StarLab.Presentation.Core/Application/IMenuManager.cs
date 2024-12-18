using StarLab.Commands;

namespace StarLab.Application
{
    /// <summary>
    /// Defines the methods that can be used to create a menu.
    /// </summary>
    public interface IMenuManager
    {
        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        void AddMenuItem(string name, string text);

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        void AddMenuItem(string parent, string name, string text);

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="image">The menu item <see cref="Image"/>.</param>
        void AddMenuItem(string name, string text, Image image);

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="command">The <see cref="ICommand"/> that will be invoked when the menu item is clicked.</param>
        void AddMenuItem(string name, string text, ICommand command);

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="image">The menu item <see cref="Image"/>.</param>
        void AddMenuItem(string parent, string name, string text, Image image);

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="command">The <see cref="ICommand"/> that will be invoked when the menu item is clicked.</param>
        void AddMenuItem(string parent, string name, string text, ICommand command);

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="image">The menu item <see cref="Image"/>.</param>
        /// <param name="command">The <see cref="ICommand"/> that will be invoked when the menu item is clicked.</param>
        void AddMenuItem(string name, string text, Image image, ICommand command);

        /// <summary>
        /// Adds a menu item to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The menu item text.</param>
        /// <param name="image">The menu item <see cref="Image"/>.</param>
        /// <param name="command">The <see cref="ICommand"/> that will be invoked when the menu item is clicked.</param>
        void AddMenuItem(string parent, string name, string text, Image image, ICommand command);

        /// <summary>
        /// Adds a separator to the menu.
        /// </summary>
        /// <param name="parent">The name of the parent menu item.</param>
        void AddMenuSeparator(string parent);

        /// <summary>
        /// Adds a separator to the menu.
        /// </summary>
        void AddMenuSeparator();
    }
}
