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
        public const string Version = "0.9.7.0";

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
            Colors.Add("#0063B1", new List<string>
            {
                "#b3ddff",
                "#80c7ff",
                "#4db0ff",
                "#1a9aff",
                "#0080e6",
                "#0064b3"
            });

            // Green
            Colors.Add("#aaf38c", new List<string>
            {
                "#ccf8ba",
                "#aaf38c",
                "#88ee5d",
                "#65e92f",
                "#4cd016",
                "#3ba211"
            });

            // Red
            Colors.Add("#E81123", new List<string>
            {
                "#fab8bd",
                "#f68891",
                "#f35966",
                "#ef293a",
                "#d61020",
                "#a60c19"
            });

            // Torquis
            Colors.Add("#0becc8", new List<string>
            {
                "#b6fcf0",
                "#85f9e7",
                "#54f7dd",
                "#24f5d3",
                "#0adbba",
                "#08ab91"
            });

            // Lila
            Colors.Add("#b710a2", new List<string>
            {
                "#f9b9f1",
                "#f58ae7",
                "#f15bde",
                "#ed2cd4",
                "#d312bb",
                "#a40e91"
            });

            // Orange
            Colors.Add("#FF8C00", new List<string>
            {
                "#ffddb3",
                "#ffc680",
                "#ffaf4d",
                "#ff981a",
                "#e67e00",
                "#b36200"
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
            List<string> ColorsList = new List<string>
            {
                "#0063B1",
                "#aaf38c",
                "#E81123",
                "#0becc8",
                "#b710a2",
                "#FF8C00"
            };
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
                        Result.Add(name, Colors[Color][Index % 6]); // 6 is the length of the list inside the dictionary
                        Helper[ActivityCategory] = Index + 1;
                    }
                });

            ColorAssingments = Result;
        }
    }
}
