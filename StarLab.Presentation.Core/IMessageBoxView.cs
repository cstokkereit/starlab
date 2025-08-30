using StarLab.Application;

namespace StarLab.Presentation
{
    /// <summary>
    /// Defines the properties and methods used by the <see cref="IMessageBoxViewPresenter"/> to control the behaviour of the message box view.
    /// </summary>
    public interface IMessageBoxView : IView
    {
        /// <summary>
        /// Configures one of the message box response buttons.
        /// </summary>
        /// <param name="index">The index of the button.</param>
        /// <param name="caption">The button caption.</param>
        /// <param name="result">The <see cref="InteractionResult"/> that will be returned by the <see cref="ShowModal"/> method when the button is clicked.</param>
        void ConfigureButton(int index, string caption, InteractionResult result);

        /// <summary>
        /// Configures the the message box.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message to display.</param>
        /// <param name="image">An <see cref="Image"/> that identifies the type of message being displayed.</param>
        void ConfigureDialog(string caption, string message, Image image);

        /// <summary>
        /// Shows the message box as a modal dialog with the specified owner.
        /// </summary>
        /// <param name="owner">The <see cref="IView"/> that owns the message box.</param>
        /// <returns>The <see cref="InteractionResult"/> corresponding to the button that was clicked.</returns>
        InteractionResult ShowModal(IView owner);
    }
}
