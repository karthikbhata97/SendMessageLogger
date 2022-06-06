using System;
using System.Runtime.InteropServices;

namespace SendMessageLogger
{
    public class Utils
    {
        public const uint WM_SETTEXT = 0x000C;
        public const uint EM_SETSEL = 0x00b1;
        public const uint EM_REPLACESEL = 0x00c2;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parent, IntPtr zeroOnly1, string classname, IntPtr zeroOnly2);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(IntPtr zeroOnly, string title);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint msg, Int32 wparam, Int32 lparam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint msg, IntPtr wparam, [MarshalAs(UnmanagedType.LPWStr)] string lparam);

        public static IntPtr FindWindow(string title)
        {
            return FindWindow(IntPtr.Zero, title);
        }

        public static IntPtr FindChildWithClassname(IntPtr parent, string classname)
        {
            return FindWindowEx(parent, IntPtr.Zero, classname, IntPtr.Zero);
        }

    }
    public class SMLogger
    {
        private class Window
        {
            private readonly IntPtr _handle;
            private void SelectEnd()
            {
                Utils.SendMessage(_handle, Utils.EM_SETSEL, 0, -1);
                Utils.SendMessage(_handle, Utils.EM_SETSEL, -1, -1);
            }
            public Window(string title)
            {
                var window = Utils.FindWindow(title);
                _handle = Utils.FindChildWithClassname(window, "RichEditD2DPT");
                if (_handle == IntPtr.Zero)
                {
                    throw new Exception("Failed to get handle");
                }
            }

            public void SetText(string message)
            {
                Utils.SendMessage(_handle, Utils.WM_SETTEXT, IntPtr.Zero, message);
            }

            public void AppendText(string message)
            {
                SelectEnd();
                Utils.SendMessage(_handle, Utils.EM_REPLACESEL, IntPtr.Zero, message);
            }
        }

        private readonly Window _window;
        public SMLogger(string windowName)
        {
            _window = new Window(windowName);
        }

        public void Log(string message)
        {
            _window.AppendText(message + "\n");
        }
    }
}
