using System.Windows.Forms;

namespace TimeTracker.Helper
{
    public static class ScreenHandler
    {
        public static Screen GetCurrentScreen(System.Windows.Window window)
        {
            System.Drawing.Rectangle ParentArea = new System.Drawing.Rectangle((int)window.Left, (int)window.Top, (int)window.Width, (int)window.Height);
            return Screen.FromRectangle(ParentArea);
        }

        public static Screen GetScreen(int requestedScreen)
        {
            Screen[] Screens = Screen.AllScreens;
            int MainScreen = 0;
            if (Screens.Length > 1 && MainScreen < Screens.Length)
            {
                return Screens[requestedScreen];
            }
            return Screens[MainScreen];
        }
    }
}
