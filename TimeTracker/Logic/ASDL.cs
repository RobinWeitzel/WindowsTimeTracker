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
        public delegate void CustomEventDelegate(object sender, CustomEventArgs args);
        public event CustomEventDelegate ShowActivityDialog;
        public event CustomEventDelegate ShowAwayFromPCDialog;

        private AppStateTracker AppStateTracker;
        public ASDL(AppStateTracker appStateTracker, ProgramSwitchListener programSwitchListener, MachineStateListener machineStateListener, HotkeyListener hotkeyListener)
        {
            AppStateTracker = appStateTracker;

            programSwitchListener.ProgramChanged += ListenerEvent;
            machineStateListener.StateChanged += ListenerEvent;
            hotkeyListener.KeyCombinationPressed += ListenerEvent;
        }

        public void ReattachHotkeyListener(HotkeyListener hotkeyListener)
        {
            hotkeyListener.KeyCombinationPressed += ListenerEvent;
        }

        private void ListenerEvent(object sender, CustomEventArgs e)
        {
            switch (sender.GetType().Name)
            {
                case "ProgramSwitchListener":
                    if (ActivitySwitched((string)e.Value))
                        ChangeActivity();
                    break;
                case "MachineStateListener":
                    if ((bool)e.Value)
                    {
                        if (Settings.Default.OfflineTracking && AppStateTracker.LastLocked != null)
                        {
                            ShowAwayFromPCDialog?.Invoke(this, new CustomEventArgs(AppStateTracker.LastLocked));
                            AppStateTracker.LastLocked = null;
                        }
                        AppStateTracker.Pause(false);
                    }
                    else
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

        public void ChangeActivity(bool focusToast = false)
        {
            AppStateTracker.Pause(false);
            ShowActivityDialog.Invoke(this, new CustomEventArgs(focusToast));
        }
    }
}
