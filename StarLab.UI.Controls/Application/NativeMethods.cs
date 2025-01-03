﻿using System.Runtime.InteropServices;

namespace StarLab.Application
{
    /// <summary>
    /// TODO
    /// </summary>
    internal static class NativeMethods
    {
        // Extending the LabelEdit functionality of a TreeView to include validation
        // http://cyotek.com/blog/extending-the-labeledit-functionality-of-a-treeview-to-include-validation

        public const int TVM_GETEDITCONTROL = 0x110F;

        public const int WM_SETTEXT = 0xC;

        [DllImport("USER32", EntryPoint = "SendMessage", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern nint SendMessage(nint hWnd, int msg, nint wParam, nint lParam);

        [DllImport("USER32", EntryPoint = "SendMessage", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern nint SendMessage(nint hWnd, int msg, nint wParam, string? lParam); // Made this nullable - in case it causes problems
    }
}
