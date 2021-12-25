using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public static class Functions
    {
        public static void Write(int x, int y, string text)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }
    }
}
