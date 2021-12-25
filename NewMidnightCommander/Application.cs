using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public static class Application
    {
        public static Window window;

        public static void Print()
        {
            Application.window.Print();
        }

        public static void HandleKey(ConsoleKeyInfo info)
        {
            Application.window.HandleKey(info);
        }
    }
}
