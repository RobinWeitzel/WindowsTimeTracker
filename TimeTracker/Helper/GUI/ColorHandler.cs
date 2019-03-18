using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Helper
{
    class ColorHandler
    {
        private List<Color> PrimaryColors;
        private List<Color> BackupColors;
        private int Counter;
        private Dictionary<string, Color> Mapping;

        public ColorHandler()
        {
            PrimaryColors = new List<Color>();
            BackupColors = new List<Color>();

            // inintiate colors
            PrimaryColors.Add(new Color("Yellow", new string[] { "#FFFBEA", "#FFF3C4", "#FCE588", "#FADB5F", "#F7C948", "#F0B429", "#DE911D", "#CB6E17", "#B44D12", "#8D2B0B" }));
            PrimaryColors.Add(new Color("Lime Green", new string[] { "#F8FFED", "#E6FFBF", "#CAFF84", "#AFF75C", "#8DED2D", "#6CD410", "#5CB70B", "#399709", "#2E7B06", "#1E5303" }));
            PrimaryColors.Add(new Color("Light Blue", new string[] { "#E3F8FF", "#B3ECFF", "#81DEFD", "#5ED0FA", "#40C3F7", "#2BB0ED", "#1992D4", "#127FBF", "#0B69A3", "#035388" }));
            PrimaryColors.Add(new Color("Purple", new string[] { "#F2EBFE", "#DAC4FF", "#B990FF", "#A368FC", "#9446ED", "#8719E0", "#7A0ECC", "#690CB0", "#580A94", "#44056E" }));
            PrimaryColors.Add(new Color("Orange", new string[] { "#FFE8D9", "#FFD0B5", "#FFB088", "#FF9466", "#F9703E", "#F35627", "#DE3A11", "#C52707", "#AD1D07", "#841003" }));
            PrimaryColors.Add(new Color("Teal", new string[] { "#F0FCF9", "#C6F7E9", "#8EEDD1", "#5FE3C0", "#2DCCA7", "#17B897", "#079A82", "#048271", "#016457", "#004440" }));
            PrimaryColors.Add(new Color("Indigo", new string[] { "#D9E8FF", "#B0D0FF", "#88B1FC", "#5E8AEE", "#3A66DB", "#2251CC", "#1D3DBF", "#132DAD", "#0B1D96", "#061178" }));
            PrimaryColors.Add(new Color("Magenta", new string[] { "#FDEBFF", "#F8C4FE", "#F48FFF", "#F368FC", "#ED47ED", "#E019D0", "#CB10B8", "#B30BA3", "#960888", "#6E0560" }));
            PrimaryColors.Add(new Color("Pink", new string[] { "#FFE3EC", "#FFB8D2", "#FF8CBA", "#F364A2", "#E8368F", "#DA127D", "#BC0A6F", "#A30664", "#870557", "#620042" }));

            BackupColors.Add(new Color("Red", new string[] { "#FFE3E3", "#FFBDBD", "#FF9B9B", "#F86A6A", "#EF4E4E", "#E12D39", "#CF1124", "#AB091E", "#8A041A", "#610316" }));
            BackupColors.Add(new Color("Green", new string[] { "#E3F9E5", "#C1F2C7", "#91E697", "#51CA58", "#31B237", "#18981D", "#0F8613", "#0E7817", "#07600E", "#014807" }));
            BackupColors.Add(new Color("Cyan", new string[] { "#E1FCF8", "#C1FEF6", "#92FDF2", "#62F4EB", "#3AE7E1", "#1CD4D4", "#0FB5BA", "#099AA4", "#07818F", "#05606E" }));
            BackupColors.Add(new Color("Blue", new string[] { "#E6F6FF", "#BAE3FF", "#7CC4FA", "#47A3F3", "#2186EB", "#0967D2", "#0552B5", "#03449E", "#01337D", "#002159" }));

            Counter = 0;
            Mapping = new Dictionary<string, Color>();
        }

        /// <summary>
        /// Gets a free color for Activity-Category
        /// </summary>
        /// <param name="name">The name of the category (first part of an activity name)</param>
        /// <returns>The color as a hex-code</returns>
        private string getColor(string name)
        {
            if (!Mapping.ContainsKey(name)) {
                if (Counter >= PrimaryColors.Count() + BackupColors.Count())
                    Counter = 0;

                if (Counter < PrimaryColors.Count())
                    Mapping.Add(name, PrimaryColors[Counter]);
                else
                    Mapping.Add(name, BackupColors[Counter - PrimaryColors.Count()]);

                Counter++;
            }

            return Mapping[name].GetColor();
        }

        /// <summary>
        /// Generates a dictionary mapping a color to each activity name.
        /// Activities belonging to the same category have similar colors
        /// </summary>
        /// <param name="storageHandler">Handles reading and wrting from/to the csv files</param>
        /// <param name="split">Whether the name should be split into category/subactivity</param>
        /// <returns>A dictionary mapping the names of all activities to colors in the hex-format</returns>
        public Dictionary<string, string> getColorDictionary(List<string> names, bool split = true)
        {
            Dictionary<string, string> Result = new Dictionary<string, string>();

            foreach (string Name in names)
            {
                if (split)
                {
                    string Category = Name.Split(new string[] { " - " }, StringSplitOptions.None).First();

                    if (!Result.ContainsKey(Category))
                        Result.Add(Category, getColor(Category));

                    Result.Add(Name, getColor(Category));
                } else
                {
                    Result.Add(Name, getColor(Name));
                }
            }

            return Result;
        }
    }
}
