using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TimeTracker.Helper;

namespace TimeTracker
{
    static class Variables
    {
        public static string version = "0.9.3.0";
        public static string activityPath = "";
        public static string windowPath = "";
        public static IDictionary<string, DateTime> windowsLastSeen = new Dictionary<string, DateTime>();
        public static Nullable<DateTime> lastConfirmed = null;
        public static Window currentWindow = null;
        public static Activity currentActivity = null;
    }
}
