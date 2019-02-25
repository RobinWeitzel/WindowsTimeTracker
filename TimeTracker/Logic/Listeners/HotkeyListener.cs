using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Input;
using TimeTracker.Properties;

namespace TimeTracker
{
    /// <summary>
    /// Listens for keyboard presses and determines if the correct hotkey was pressed.
    /// Sends out an event if the hotkey was pressed.
    /// Whether to listen for an hotkey and for which hotkey can be configured in the settings.
    /// </summary>
    public class HotkeyListener : IDisposable
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 SWP_SHOWWINDOW = 0x0040;

        private LowLevelKeyboardProc keyboardProc;
        private IntPtr hookId = IntPtr.Zero;

        private HashSet<Key> pressedKeys = new HashSet<Key>();

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        public delegate void CustomEventDelegate(object sender, CustomEventArgs args);

        public event CustomEventDelegate KeyCombinationPressed;

        public HotkeyListener()
        {
            keyboardProc = HookCallback;
            hookId = SetHook(keyboardProc);
        }

        public void Dispose()
        {
            UnhookWindowsHookEx(hookId);
        }

        public void OnKeyCombinationPressed(EventArgs e)
        {
            KeyCombinationPressed?.Invoke(this, new CustomEventArgs());
        }

        /// <summary>
        /// Sets up the hook to listen for key presses.
        /// </summary>
        /// <param name="proc"></param>
        /// <returns></returns>
        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                                        GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        /// <summary>
        /// Called every time a key is pressed.
        /// If the correct hotkeys are pressed, an event is sent out.
        /// </summary>
        /// <param name="nCode">The key code</param>
        /// <param name="wParam">The key action type</param>
        /// <param name="lParam"></param>
        /// <returns>Calls the next hook</returns>
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                var keyPressed = KeyInterop.KeyFromVirtualKey(vkCode);
                pressedKeys.Add(keyPressed);
                if (!Settings.Default.HotkeyDisabled && Settings.Default.Hotkeys != null && Settings.Default.Hotkeys.SequenceEqual(pressedKeys))
                {
                    OnKeyCombinationPressed(new EventArgs());
                }
            }
            else if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                var keyReleased = KeyInterop.KeyFromVirtualKey(vkCode);
                pressedKeys.Remove(keyReleased);
            }

            return CallNextHookEx(hookId, nCode, wParam, lParam);
        }
    }
}
