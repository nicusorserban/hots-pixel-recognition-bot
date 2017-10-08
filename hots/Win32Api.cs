using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace hots
{
    public static class Win32Api
    {
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);

        /// <summary>
        /// self-defined struct
        /// </summary>
        public struct Lparam
        {
            public Lparam(int _i, string _s)
            {
                i = _i;
                s = _s;
            }
            public int i;
            public string s;
        }
        /// <summary>
        /// use COPYDATASTRUCT to send a string
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }
        //message delivery API
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
            IntPtr hWnd,        // the target windows's handle
            int Msg,            // message ID
            int wParam,         // parameter 1
            int lParam          //parameter 2
        );


        //message delivery API
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
            IntPtr hWnd,        // the target windows's handle
            int Msg,            // message ID
            int wParam,         // parameter 1
            ref Lparam lParam //parameter 2
        );

        //message delivery API
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
            IntPtr hWnd,        // the target windows's handle
            int Msg,            // message ID
            int wParam,         // parameter 1
            ref COPYDATASTRUCT lParam  //parameter 2
        );

        //message delivery API
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(
            IntPtr hWnd,        // the target windows's handle
            int Msg,            // message ID
            IntPtr wParam,         // parameter 1
            IntPtr lParam            // parameter 2
        );



        //message delivery API
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(
            IntPtr hWnd,        // the target windows's handle
            int Msg,            // message ID
            IntPtr wParam,         // parameter 1
            Lparam lParam // parameter 2
        );

        //message delivery API
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(
            IntPtr hWnd,        // the target windows's handle
            int Msg,            // message ID
            int wParam,         // parameter 1
            ref COPYDATASTRUCT lParam  // parameter 2
        );

    }
}