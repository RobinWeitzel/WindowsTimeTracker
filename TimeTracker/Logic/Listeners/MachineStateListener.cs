using Microsoft.Win32;

namespace TimeTracker
{
    /// <summary>
    /// Tracks the logon/logoff state of the computer.
    /// Sends out an event any time the state changes.
    /// </summary>
    public class MachineStateListener
    {
        public delegate void CustomEventDelegate(object sender, CustomEventArgs args);
        public event CustomEventDelegate StateChanged;

        /// <summary>
        /// Tracks the logon/logoff state of the computer.
        /// Sends out an event any time the state changes.
        /// </summary>
        public MachineStateListener()
        {
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(OnSessionSwitch);
        }

        /// <summary>
        /// This function is called any time the state changes.
        /// It determines whether the computer was locked or unlocked.
        /// It triggers an event passing this informatin along (Eventargs.Value = true if computer was unlocked, otherwise false)
        /// </summary>
        /// <param name="s">The sender</param>
        /// <param name="e">The event arguments</param>
        private void OnSessionSwitch(object s, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    StateChanged?.Invoke(this, new CustomEventArgs(false));
                    break;
                case SessionSwitchReason.SessionLogoff:
                    StateChanged?.Invoke(this, new CustomEventArgs(false));
                    break;
                case SessionSwitchReason.SessionLogon:
                    StateChanged?.Invoke(this, new CustomEventArgs(true));
                    break;
                case SessionSwitchReason.SessionUnlock:
                    StateChanged?.Invoke(this, new CustomEventArgs(true));
                    break;
            }
        }

    }
}
