namespace StarLab.Application
{
    /// <summary>
    /// A static class containing constant values.
    /// </summary>
    public static class Constants
    {
        public static char[] IllegalCharacters = { '\\', '/', ':', '*', '?', '\'', '\"', '<', '>', '|' };

        public const string Chart = "Chart";

        public const string DefaultBackColour = "White"; // TODO : These should all be replaced by user settings
        public const string DefaultForeColour = "Black";
        
        public const string Table = "Table";

        public const string Workspace = "Workspace";
        public const string WorkspaceExtension = ".slw";
    }
}
