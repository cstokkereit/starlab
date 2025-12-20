namespace StarLab.Application
{
    /// <summary>
    /// A static class containing constant values.
    /// </summary>
    public static class Constants
    {
        public static readonly char[] IllegalCharacters = { '\\', '/', ':', '*', '?', '\'', '\"', '<', '>', '|' };

        public const string DefaultBackColour = "White";
        public const string DefaultFontFamily = "Segoe UI";
        public const int DefaultFontSize = 10;
        public const string DefaultForeColour = "Black";
        public const int DefaultMajorTickMarkLength = 4;
        public const int DefaultMinorTickMarkLength = 2;

        public const string InvalidOperationMessage = "The item with key '{0}' is not valid for the current operation.";
        public const string InvalidPathMessage = "The path cannot be an empty string.";

        public const string NameExistsMessage = "A {0} with the name '{1}' already exists.";

        public const string Workspace = "Workspace";
        public const string WorkspaceExtension = ".slw";
    }
}
