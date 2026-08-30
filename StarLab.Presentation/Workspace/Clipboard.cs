namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// TODO
    /// </summary>
    internal class Clipboard
    {
        private ClipboardState state; //

        /// <summary>
        /// 
        /// </summary>
        public bool IsEmpty => state.Operation == ClipboardOperation.None;

        /// <summary>
        /// 
        /// </summary>
        public ClipboardOperation Operation => state.Operation;

        /// <summary>
        /// 
        /// </summary>
        public string Source => state.Source;

        /// <summary>
        /// Clears the clipboard.
        /// </summary>
        public void Clear()
        {
            state = new ClipboardState();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public void Copy(string source)
        {
            ArgumentException.ThrowIfNullOrEmpty(source, nameof(source));

            state = new ClipboardState(ClipboardOperation.Copy, source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public void Cut(string source)
        {
            ArgumentException.ThrowIfNullOrEmpty(source, nameof(source));

            state = new ClipboardState(ClipboardOperation.Cut, source);
        }

        /// <summary>
        /// TODO
        /// </summary>
        private struct ClipboardState
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="operation"></param>
            /// <param name="source"></param>
            public ClipboardState(ClipboardOperation operation, string source)
            {
                Operation = operation;
                Source = source;
            }

            /// <summary>
            /// 
            /// </summary>
            public ClipboardState()
            {
                Operation = ClipboardOperation.None;
                Source = string.Empty;
            }

            /// <summary>
            /// 
            /// </summary>
            public ClipboardOperation Operation { get; }

            /// <summary>
            /// 
            /// </summary>
            public string Source { get; }
        }
    }
}
