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

        public static void Button(int x, int y, string text, int buttonNumber)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Write(x, y, buttonNumber.ToString());
            Console.BackgroundColor = ProgramSettings.SelectedBackColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Write(x + buttonNumber.ToString().Length, y, text.PadRight(6));
            Console.ResetColor();
        }
    }
}
