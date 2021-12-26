using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public static class ProgramSettings
    {
        public static int PanelDataHeight = 25;
        public static int PanelHeight = 30;
        public static int PanelWidth = 119;
        public static ConsoleColor SelectedForeColor = ConsoleColor.White;
        public static ConsoleColor SelectedBackColor = ConsoleColor.Blue;

        public static ConsoleColor ForeColor = ConsoleColor.White;
        public static ConsoleColor BackColor = ConsoleColor.DarkBlue;
        public static ConsoleColor TitleColor = ConsoleColor.DarkYellow;

        public static ConsoleColor TitleBoxColor = ConsoleColor.Blue;
        public static ConsoleColor BackBoxColor = ConsoleColor.Gray;
        public static ConsoleColor ForeBoxColor = ConsoleColor.Black;
        public static ConsoleColor SelectedBoxColor = ConsoleColor.Red;
        
        public static string LeftPanelPath;
        public static string RightPanelPath;
        public static bool LeftPanelActive = true;
    }
}
