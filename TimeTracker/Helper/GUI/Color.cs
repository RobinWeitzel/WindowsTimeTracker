using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Helper
{
    class Color
    {
        public string Name { get; set; }

        private List<string> PrimaryColors { get; set; }

        private List<string> BackupColors { get; set; }

        private int Counter { get; set; }

        public Color(string name, string[] colors)
        {
            Name = name;

            PrimaryColors = new List<string>();
            BackupColors = new List<string>();

            for (int i = 0; i < colors.Length; i++)
            {
                // Load only the nice colors as primary, everything else as backup
                if (i % 2 == 1 || i == 0 || i == colors.Length - 2)
                    BackupColors.Add(colors[i]);
                else
                    PrimaryColors.Add(colors[i]);
            }

            Counter = 0;
        }

        /// <summary>
        /// Gets a color value that is still available, first using the primary colors, then the backup colors.
        /// </summary>
        /// <returns>A color as a hex-code string</returns>
        public string GetColor()
        {
            string Result;

            if (Counter >= PrimaryColors.Count() + BackupColors.Count()) // All colors have been used, restart from the beginning
                Counter = 0;

            if (Counter < PrimaryColors.Count()) // if primary colors are still available
                Result = PrimaryColors[Counter];
            else
                Result = BackupColors[Counter - PrimaryColors.Count()];

            Counter++;

            return Result;
        }
    }
}
