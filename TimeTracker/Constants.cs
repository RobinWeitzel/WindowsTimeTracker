using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TimeTracker
{
    static class Constants
    {
        public static long defaultTimeout = 10000; // 10 seconds
        public static long defaultTimeNotUsed = 1000 * 60 * 5; // 5 minutes
        public static long defaultTimeout2 = 30; // 30 Seconds
        public static bool fuzzyMatching = false;
        public static bool useHashColors = false;
        public static bool lastActivities = true;
        public static bool useNativeToast = false; 
        public static bool makeSound = false;

        public static Nullable<DateTime> lastConfirmed = null;
    }
}
