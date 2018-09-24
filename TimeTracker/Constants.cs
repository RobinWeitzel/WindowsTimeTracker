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
        public static long defaultTimeRecordsKept = 30; // A month
        public static bool fuzzyMatching = false;
        public static bool useHashColors = false;

        /*
           <Color A="255" R="33" G="149" B="242" />
        <Color A="255" R="243" G="67" B="54" />
        <Color A="255" R="254" G="192" B="7" />
        <Color A="255" R="96" G="125" B="138" />
        <Color A="255" R="232" G="30" B="99" />
        <Color A="255" R="76" G="174" B="80" />
        <Color A="255" R="63" G="81" B="180" />
        <Color A="255" R="204" G="219" B="57" />
    */

        public static List<Color> colors = new List<Color>
        {
            Color.FromRgb(33, 149, 242),
            Color.FromRgb(243, 67, 54),
            Color.FromRgb(254, 192, 7),
            Color.FromRgb(96, 125, 138),
            Color.FromRgb(232, 30, 99),
            Color.FromRgb(76, 174, 80),
            Color.FromRgb(63, 81, 180),
            Color.FromRgb(204, 219, 57),
        };
    }
}
