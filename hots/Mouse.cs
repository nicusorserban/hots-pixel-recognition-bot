using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace hots
{
    public static class Mouse
    {
        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lparam);

        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_MOUSEACTIVATE = 0x0021;
        public const int WM_ACTIVATE = 0x0006;
        public const int MK_LBUTTON = 0x0001;

        public static void Click(IntPtr mainWindowHandle, Point location)
        {
            Win32Api.PostMessage(mainWindowHandle, WM_MOUSEACTIVATE, mainWindowHandle,(IntPtr) (1 | (Mouse.WM_LBUTTONDOWN << 16)));
            //mouse activate ?
            Thread.Sleep(50);
            Win32Api.PostMessage(mainWindowHandle, WM_ACTIVATE, (IntPtr)2, (IntPtr)(0 | (0 << 0)));
            Thread.Sleep(50);
            Win32Api.SendMessage(mainWindowHandle, WM_LBUTTONDOWN, MK_LBUTTON, (105 | (568 << 16)));
            Thread.Sleep(50);
            Win32Api.SendMessage(mainWindowHandle, WM_LBUTTONUP, 0, (105 | (568 << 16)));

            Win32Api.PostMessage(mainWindowHandle, WM_ACTIVATE, (IntPtr)0, (IntPtr)(0 | (0 << 0)));

            //PostMessage(mainWindowHandle, Mouse.WM_MOUSEACTIVATE, mainWindowHandle, (IntPtr)(1 | (Mouse.WM_LBUTTONDOWN << 16)));
            //PostMessage(mainWindowHandle, Mouse.WM_LBUTTONDOWN, IntPtr.Zero,(IntPtr)(72 | (568 << 16))); // 72 568 x | (y << 16)
            //Thread.Sleep(50);
            //PostMessage(mainWindowHandle, Mouse.WM_LBUTTONUP, (IntPtr)72,(IntPtr)(72 | (568 << 16)));
        }
    }


}