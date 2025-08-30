using StarLab.Application;

namespace StarLab.Presentation
{
    public interface IMessageBoxController : IViewController
    {
        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified owner and options.
        /// </summary>
        /// <param name="owner">The <see cref="IView"/> that will own the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="type">An <see cref="InteractionType"/> that specifies the type of message being displayed.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        InteractionResult ShowMessage(IView owner, string caption, string message, InteractionType type, InteractionResponses responses);

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified owner and options.
        /// </summary>
        /// <param name="owner">The <see cref="IView"/> that will own the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        InteractionResult ShowMessage(IView owner, string caption, string message, InteractionResponses responses);

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified owner and options.
        /// </summary>
        /// <param name="owner">The <see cref="IView"/> that will own the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        InteractionResult ShowMessage(IView owner, string caption, string message);
    }
}
