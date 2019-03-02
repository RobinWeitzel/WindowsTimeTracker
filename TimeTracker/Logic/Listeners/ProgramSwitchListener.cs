using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TimeTracker
{
    /// <summary>
    /// Tracks the active window to determine when the user changes to a different window.
    /// Sents out an event if he does.
    /// </summary>
    public class ProgramSwitchListener
    {
        /* Delegates */
        private delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
        public delegate void CustomEventDelegate(object sender, CustomEventArgs args);

        /* DLL imports */
        [DllImport("user32.dll")]
        private static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern long GetWindowText(IntPtr hwnd, StringBuilder lpString, long cch);

        [DllImport("User32.dll")]
        private static extern IntPtr GetParent(IntPtr hwnd);

        /* Events */
        public event CustomEventDelegate ProgramChanged;

        /* Constants */
        private const uint WINEVENT_OUTOFCONTEXT = 0;
        private const uint EVENT_SYSTEM_FOREGROUND = 3;

        /* Variables */
        private WinEventDelegate WindowChanged;
        

        /// <summary>
        /// Tracks the active window to determine when the user changes to a different window.
        /// Sents out an event if he does.
        /// </summary>
        public ProgramSwitchListener()
        {
            // Set up callback if active window changes
            WindowChanged = new WinEventDelegate(WinEventProc);
            IntPtr m_hhook = SetWinEventHook(
                EVENT_SYSTEM_FOREGROUND,
                EVENT_SYSTEM_FOREGROUND,
                IntPtr.Zero,
                WindowChanged,
                0,
                0,
                WINEVENT_OUTOFCONTEXT);
        }

        /// <summary>
        /// Determines the window title from a window id
        /// </summary>
        /// <param name="handle">The window id</param>
        /// <returns>The name of the window or null, if it has no name</returns>
        private string GetActiveWindowTitle(IntPtr handle)
        {
            const int NChars = 256;
            StringBuilder Buff = new StringBuilder(NChars);
            if (handle.ToInt64() == 0)
                handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, NChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        /// <summary>
        /// Called any time the active window changes.
        /// Checks if the program belonging to the new window can be determined.
        /// If it can be determined an event is sent out with the name of the window attached to it.
        /// </summary>
        /// <param name="hWinEventHook"></param>
        /// <param name="eventType"></param>
        /// <param name="hwnd"></param>
        /// <param name="idObject"></param>
        /// <param name="idChild"></param>
        /// <param name="dwEventThread"></param>
        /// <param name="dwmsEventTime"></param>
        public void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            IntPtr Handle;
            long HandleLong;

            // Check if parent exists, if it does use it (meaning the active window is only a sub window)
            IntPtr NewParent = GetParent(hwnd);
            IntPtr OldParent = hwnd;
            while (NewParent.ToInt64() > 0)
            {
                OldParent = NewParent;
                NewParent = GetParent(OldParent);
            }

            Handle = OldParent;
            HandleLong = hwnd.ToInt64();

            if (HandleLong <= 0)
                return;

            string windowTitle = GetActiveWindowTitle(Handle);
            if (windowTitle == null)
                return;

            ProgramChanged?.Invoke(this, new CustomEventArgs(windowTitle));
        }
    }
}
