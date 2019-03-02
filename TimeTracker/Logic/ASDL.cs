using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Properties;

namespace TimeTracker
{
    /// <summary>
    /// Activity Switch Detection Logic
    /// </summary>
    public class ASDL
    {
        /* Delegates */
        public delegate void CustomEventDelegate(object sender, CustomEventArgs args);

        /* Events */
        public event CustomEventDelegate ShowActivityDialog;
        public event CustomEventDelegate ShowAwayFromPCDialog;

        /* Variables */
        private AppStateTracker AppStateTracker;

        /// <summary>
        /// Decides if the activity/window has truely changed.
        /// If so, triggers the activity dialog.
        /// </summary>
        /// <param name="appStateTracker">The state tracker for this app</param>
        /// <param name="programSwitchListener">A listener to determine if the current window has changed</param>
        /// <param name="machineStateListener">A listener to determine if the machine state has changed</param>
        /// <param name="hotkeyListener">A listener to detemrine if the hotkey has been pressed</param>
        public ASDL(AppStateTracker appStateTracker, ProgramSwitchListener programSwitchListener, MachineStateListener machineStateListener, HotkeyListener hotkeyListener)
        {
            AppStateTracker = appStateTracker;

            programSwitchListener.ProgramChanged += ListenerEvent;
            machineStateListener.StateChanged += ListenerEvent;
            hotkeyListener.KeyCombinationPressed += ListenerEvent;
        }

        /// <summary>
        /// Reattaches the hotkey listener.
        /// When the device is locked, the old listener no longer works.
        /// Therefore, it has to be reattached every time the machine is unlocked.
        /// </summary>
        /// <param name="hotkeyListener"></param>
        public void ReattachHotkeyListener(HotkeyListener hotkeyListener)
        {
            hotkeyListener.KeyCombinationPressed += ListenerEvent;
        }

        /// <summary>
        /// The function called any time an event from one of the listeners is triggered.
        /// </summary>
        /// <param name="sender">The class that sent the event</param>
        /// <param name="e">The event</param>
        private void ListenerEvent(object sender, CustomEventArgs e)
        {
            switch (sender.GetType().Name)
            {
                case "ProgramSwitchListener":
                    if (ActivitySwitched((string)e.Value))
                        ChangeActivity();
                    break;
                case "MachineStateListener":
                    if ((bool)e.Value) // True means the machine has been unlocked/woken up
                    {
                        if (Settings.Default.OfflineTracking && AppStateTracker.LastLocked != null)
                        {
                            ShowAwayFromPCDialog?.Invoke(this, new CustomEventArgs(AppStateTracker.LastLocked));
                            AppStateTracker.LastLocked = null;
                        }
                        AppStateTracker.Pause(false);
                    }
                    else // False means the machine has been locked/put to sleep.
                    {
                        AppStateTracker.Pause(true);
                        AppStateTracker.LastLocked = DateTime.Now;
                    }
                    break;
                case "HotkeyListener":
                    ChangeActivity(true);
                    break;
            }
        }

        /// <summary>
        /// Checks if the activity has (presumably) changed.
        /// Saves the current window if the program has changed.
        /// If the activity has not changed but no activity is running, also creates a new activity (without notifying the user).
        /// </summary>
        /// <param name="windowTitle">The name of the new window/program</param>
        /// <returns>True, if the activity has (presumably) changed; otherwise False</returns>
        private bool ActivitySwitched(string windowTitle)
        {
            if (AppStateTracker.Paused)
                return false;

            string[] SplitWindowTitle = windowTitle.Split(new string[] { "- " }, StringSplitOptions.None);

            if (SplitWindowTitle.Length < 1) // Ignore empty window titles
                return false;

            string ProgramTitle = SplitWindowTitle.Last();
            string WindowDetails = SplitWindowTitle.Length > 1 ? string.Join("- ", SplitWindowTitle.Take(SplitWindowTitle.Length - 1)) : "";

            bool HasBeenSeen = false;
            DateTime LastSeen;

            // Load need variables from settings
            long TimeNotUsed = Settings.Default.TimeSinceAppLastUsed * 60 * 1000;  // Convert to ms 
            long Timeout2 = Settings.Default.Timeout2;

            // Stop if the current activity is blacklisted or a file path
            if (Settings.Default.Blacklist.Contains(ProgramTitle) || ProgramTitle.Contains("\\"))
            {
                if (!windowTitle.Equals("NotificationsWindow - TimeTracker"))
                    AppStateTracker.SaveCurrentWindow();
                return false;
            }

            // Check if the window has not been see for more than whatever is specified in the settings
            if (AppStateTracker.WindowsLastSeen.TryGetValue(ProgramTitle, out LastSeen))
                HasBeenSeen = TimeNotUsed > DateTime.Now.Subtract(LastSeen).TotalMilliseconds;

            // If window has not changed do nothing
            if (AppStateTracker.CurrentWindow != null && AppStateTracker.CurrentWindow.Name.Equals(ProgramTitle))
                return false;

            AppStateTracker.SaveCurrentWindow();
            AppStateTracker.CreateCurrentWindow(ProgramTitle, WindowDetails);

            // Show notification if app has not been seen in last few minutes
            if ((AppStateTracker.LastConfirmed == null || DateTime.Now.Subtract((DateTime)AppStateTracker.LastConfirmed).TotalSeconds > Timeout2) && !HasBeenSeen && AppStateTracker.Disturb)
            {
                return true;
            }
            // Handle case that window has been seen shortly before but still no activity is running. In this case just start up another activity of the same kind
            else if (AppStateTracker.CurrentActivity == null)
            {
                AppStateTracker.CreateCurrentActivity(); // Creates a new activity using the name of the last activity.
            }

            return false;
        }

        /// <summary>
        /// Triggers the activity dialog.
        /// </summary>
        /// <param name="focusToast">True, if the dialog should be focused when it is shown, otherwise False</param>
        public void ChangeActivity(bool focusToast = false)
        {
            AppStateTracker.Pause(false);
            ShowActivityDialog.Invoke(this, new CustomEventArgs(focusToast));
        }
    }
}
