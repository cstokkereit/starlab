using System.Runtime.InteropServices;

namespace StarLab.Application
{
    /// <summary>
    /// Provides access to functions defined in the Windows API libraries.
    /// </summary>
    internal static class NativeMethods
    {
        public const int TVM_GETEDITCONTROL = 0x110F;

        public const int WM_SETTEXT = 0xC;

        [DllImport("USER32", EntryPoint = "SendMessage", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern nint SendMessage(nint hWnd, int msg, nint wParam, nint lParam);

        [DllImport("USER32", EntryPoint = "SendMessage", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern nint SendMessage(nint hWnd, int msg, nint wParam, string? lParam);
    }
}
