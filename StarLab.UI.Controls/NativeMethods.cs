using System.Runtime.InteropServices;

namespace StarLab.UI.Controls
{
    internal static class NativeMethods
    {
        // Extending the LabelEdit functionality of a TreeView to include validation
        // http://cyotek.com/blog/extending-the-labeledit-functionality-of-a-treeview-to-include-validation

        #region Constants

        public const int TVM_GETEDITCONTROL = 0x110F;

        public const int WM_SETTEXT = 0xC;

        #endregion

        #region Class Members

        [DllImport("USER32", EntryPoint = "SendMessage", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("USER32", EntryPoint = "SendMessage", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string? lParam); // Made this nullable - in case it causes problems

        #endregion
    }
}
