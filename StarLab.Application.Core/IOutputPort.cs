namespace StarLab.Application
{
    /// <summary>
    /// Used by a <see cref="UseCaseInteractor{TOutputPort}"/> to provide feedback or request user input.
    /// </summary>
    public interface IOutputPort
    {
        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified caption, message, message type and available responses.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="type">An <see cref="InteractionType"/> that specifies the type of message being displayed.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        InteractionResult ShowMessage(string caption, string message, InteractionType type, InteractionResponses responses);

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified caption, message and available responses.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        InteractionResult ShowMessage(string caption, string message, InteractionResponses responses);

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified caption and message.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        InteractionResult ShowMessage(string caption, string message);
    }
}
