using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Helper;

namespace TimeTracker
{
    /// <summary>
    /// Keeps track of the apps state variables.
    /// </summary>
    public class AppStateTracker
    {
        /* Delegates */
        public delegate void CustomEventDelegate(object sender, CustomEventArgs args);

        /* Events */
        public event CustomEventDelegate ChangeContextMenu;

        /* Constants */
        public const string Version = "1.0.0.0";

        /* Variables */
        public IDictionary<string, DateTime> WindowsLastSeen { get; set; }
        public Nullable<DateTime> LastConfirmed { get; set; }
        public Window CurrentWindow { get; set; }
        public Activity CurrentActivity { get; set; }
        public DateTime? LastLocked { get; set; }
        public bool Paused { get; set; }
        public bool Disturb { get; set; }

        private Dictionary<string, List<string>> Colors { get; set; }
        public Dictionary<string, string> ColorAssingments { get; set; }

        private StorageHandler StorageHandler;

        /// <summary>
        /// Keeps track of the apps state variables.
        /// </summary>
        /// <param name="storageHandler">
        /// Handles writing to the csv files.
        /// </param>
        public AppStateTracker(StorageHandler storageHandler)
        {
            WindowsLastSeen = new Dictionary<string, DateTime>();
            LastConfirmed = null;
            CurrentWindow = null;
            CurrentActivity = null;
            LastLocked = null;
            Paused = false;
            Disturb = true;
            StorageHandler = storageHandler;

            Colors = new Dictionary<string, List<string>>();

            // Blue
            Colors.Add("#7cd6fd", new List<string>
            {
                "#81d8fd",
                "#4fc8fc",
                "#1db8fc",
                "#039fe2",
            });

            // Lila
            Colors.Add("#5e64ff", new List<string>
            {
                "#8084ff",
                "#4d53ff",
                "#1a22ff",
                "#0009e6"
            });

            // Lila 2
            Colors.Add("#743ee2", new List<string>
            {
                "#af90ee",
                "#8f64e8",
                "#6f37e1",
                "#561ec8"
            });

            // Red
            Colors.Add("#ff5858", new List<string>
            {
                "#ff8080",
                "#ff4d4d",
                "#ff1a1a",
                "#e60000"
            });

            // Orange
            Colors.Add("#ffa00a", new List<string>
            {
                "#ffce80",
                "#ffba4d",
                "#ffa61a",
                "#e68d00"
            });

            // Yellow
            Colors.Add("#feef72", new List<string>
            {
                "#fef180",
                "#feeb4e",
                "#fde51b",
                "#e4cc02"
            });

            // Green
            Colors.Add("#28a745", new List<string>
            {
                "#98e6aa",
                "#6fdd88",
                "#46d366",
                "#2cb94d"
            });

            // Green 2
            Colors.Add("#98d85b", new List<string>
            {
                "#bee798",
                "#a4dd6f",
                "#8bd346",
                "#71b92c"
            });

            // Lila 3
            Colors.Add("#b554ff", new List<string>
            {
                "#c880ff",
                "#b24dff",
                "#9c1aff",
                "#8200e6"
            });

            // Pink
            Colors.Add("#ffa3ef", new List<string>
            {
                "#ff80e9",
                "#ff4de0",
                "#ff1ad7",
                "#e600be"
            });

            // Blue 2
            Colors.Add("#bdd3e6", new List<string>
            {
                "#a3c1dc",
                "#7ea9ce",
                "#5990c0",
                "#3f77a6"
            });

            // Gray
            Colors.Add("#b8c2cc", new List<string>
            {
                "#b5bfca",
                "#97a6b4",
                "#798c9f",
                "#607386"
            });

            AssignColors();
        }

        /// <summary>
        /// Toogles/set the pause status.
        /// If no parameter is provided, the status is toggled.
        /// If a bool-parameter is provided, the status is set to that parameter.
        /// </summary>
        /// <param name="setPause">True, if the tracking should be pause; otherwise False.</param>
        public void Pause(bool? setPause)
        {
            if (setPause != null) // Set pause
            {
                if (setPause == true)
                {
                    SaveCurrentWindow();
                    SaveCurrentActivity();
                    ChangeContextMenu?.Invoke(this, new CustomEventArgs(true));
                    Paused = true;
                }
                else
                {
                    ChangeContextMenu?.Invoke(this, new CustomEventArgs(false));
                    Paused = false;
                }
            }
            else // Toggle pause
            {
                if (Paused)
                {
                    ChangeContextMenu?.Invoke(this, new CustomEventArgs(false));
                    Paused = false;
                }
                else
                {
                    SaveCurrentWindow();
                    SaveCurrentActivity();
                    ChangeContextMenu?.Invoke(this, new CustomEventArgs(true));
                    Paused = true;
                }
            }
        }

        /// <summary>
        /// Creates a new window, fills the "from" time and returns it.
        /// Stores this window as the current window.
        /// </summary>
        /// <param name="name">The name of the program the window belongs to.</param>
        /// <param name="details">The rest of the window title.</param>
        /// <returns>The newly created window</returns>
        public Window CreateCurrentWindow(string name, string details)
        {
            Window Window = new Window();
            Window.Name = name;
            Window.Details = details;
            Window.From = DateTime.Now;

            CurrentWindow = Window;

            return Window;
        }

        /// <summary>
        /// Creates a new activity using either the provided name or the name if the last activity-
        /// Stores this activity as the current activity.
        /// </summary>
        /// <param name="name">The name that should be used for the activity.
        /// Can be left blank in which case the name of the most recent activity is used.</param>
        /// <returns>The newly created activity.</returns>
        public Activity CreateCurrentActivity(string name = null, DateTime? from = null)
        {
            Activity Activity = new Activity();
            Activity.From = from ?? DateTime.Now;

            if (name == null) // Nme should be looked up based on the last activity in the csv-file
                Activity.Name = StorageHandler.GetLastActivity()?.Name ?? "";
            else
                Activity.Name = name;

            CurrentActivity = Activity;

            return Activity;
        }

        /// <summary>
        /// Saves the current window to the csv file and nulls the variable afterwards
        /// Does nothing if the window is already null.
        /// </summary>
        /// <param name="to">Optional to-date. If null, the current time will be used.</param>
        public void SaveCurrentWindow(DateTime? to = null)
        {
            if (CurrentWindow == null)
                return;

            CurrentWindow.To = to ?? DateTime.Now;
            WindowsLastSeen[CurrentWindow.Name] = (DateTime)CurrentWindow.To;
            if (CurrentWindow.From <= CurrentWindow.To)
                StorageHandler.WriteWindow(CurrentWindow);

            CurrentWindow = null;
        }

        /// <summary>
        /// Saves the current activity to the csv file and nulls the variable afterwards.
        /// Does nothing if the activity is already null.
        /// </summary>
        /// <param name="to"></param>
        public void SaveCurrentActivity(DateTime? to = null)
        {
            if (CurrentActivity == null)
                return;

            CurrentActivity.To = to ?? DateTime.Now;

            if (CurrentActivity.From <= CurrentActivity.To && ((DateTime)CurrentActivity.To).Subtract(CurrentActivity.From).TotalSeconds >= 30)
                StorageHandler.WriteActivity(CurrentActivity);

            CurrentActivity = null;

            AssignColors();
        }

        /// <summary>
        /// Assigns colors to each activity name (and also activity categories if available).
        /// </summary>
        public void AssignColors()
        {
            List<string> ColorsList = Colors.Keys.ToList();

            int Counter = 0;
            Dictionary<string, int> Helper = new Dictionary<string, int>();
            Dictionary<string, string> Result = new Dictionary<string, string>();

            StorageHandler.GetActivitiesByLambda(a => true)
                .GroupBy(a => a.Name)
                .Select(g => g.Key)
                .ToList()
                .ForEach(name =>
                {
                    string ActivityCategory = name.Split(new string[] { " - " }, StringSplitOptions.None).First();

                    string Color;
                    // Get main color for category if not yet set
                    if (!Result.ContainsKey(ActivityCategory))
                    {
                        Color = ColorsList[Counter % ColorsList.Count];
                        Counter = Counter + 1;
                        Helper.Add(ActivityCategory, 0);
                        Result.Add(ActivityCategory, Color);
                    }
                    else
                    {
                        Color = Result[ActivityCategory];
                    }

                    if (!Result.ContainsKey(name))
                    {
                        int Index = Helper[ActivityCategory];
                        Result.Add(name, Colors[Color][Index % Colors.Min(c => c.Value.Count)]);
                        Helper[ActivityCategory] = Index + 1;
                    }
                });

            ColorAssingments = Result;
        }
    }
}
