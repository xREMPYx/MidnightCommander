using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class Application
    {
        public static Window window;

        public static List<Window> windows = new();

        public static void Print()
        {
            Application.window.Print();            
        }

        public static void HandleKey(ConsoleKeyInfo info)
        {
            Application.window.HandleKey(info);
        }

        public static void SaveLastWindow()
        {
            Application.windows.Add(Application.window);
        }

        public static void RenewWindow(int LastOrPreLast)
        {
            Application.window = Application.windows[Application.windows.Count - LastOrPreLast];
        }
    }
}
