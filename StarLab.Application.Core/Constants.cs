namespace StarLab.Application
{
    /// <summary>
    /// A static class containing constant values.
    /// </summary>
    public static class Constants
    {
        public static readonly char[] IllegalCharacters = { '\\', '/', ':', '*', '?', '\'', '\"', '<', '>', '|' };

        public const string Chart = "Chart";

        public const string DefaultBackColour = "White";
        public const string DefaultFontFamily = "Segoe UI";
        public const int DefaultFontSize = 10;
        public const string DefaultForeColour = "Black";
        public const int DefaultMajorTickMarkLength = 4;
        public const int DefaultMinorTickMarkLength = 2;

        public const string Table = "Table";

        public const string Workspace = "Workspace";
        public const string WorkspaceExtension = ".slw";
    }
}
