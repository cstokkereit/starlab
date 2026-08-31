using System.Runtime.InteropServices;

namespace StarLab.UI.Controls
{
    /// <summary>
    /// A static class that contains imported Win32 API functions.
    /// </summary>
    internal static class Win32Api
    {
        public const string ExplorerTheme = "explorer"; // The theme used by the Windows Explorer.

        /// <summary>
        /// Causes a window to use a different set of visual style information than its class normally uses.
        /// </summary>
        /// <param name="hwnd">Handle to the window whose visual style information is to be changed.</param>
        /// <param name="pszSubAppName">Pointer to a string that contains the application name to use in place of the calling application's name. If this parameter is NULL, the calling application's name is used.</param>
        /// <param name="pszSubIdList">Pointer to a string that contains a semicolon-separated list of CLSID names to use in place of the actual list passed by the window's class. If this parameter is NULL, the ID list from the calling class is used.</param>
        /// <returns>Returns zero on success or a non-zero error code indicating the cause and severity of the failure.</returns>
        [DllImport("UxTheme.dll", CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hwnd, string? pszSubAppName, string? pszSubIdList);
    }
}
