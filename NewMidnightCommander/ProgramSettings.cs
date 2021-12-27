using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public static class ProgramSettings
    {
        public static int PanelDataHeight = 23;
        public static int PanelHeight = 30;
        public static int PanelWidth = 119;

        public static ConsoleColor SelectedForeColor = ConsoleColor.White;
        public static ConsoleColor SelectedBackColor = ConsoleColor.Blue;

        public static ConsoleColor AlertTextColor = ConsoleColor.Red;

        public static ConsoleColor ForeMenuColor = ConsoleColor.Black;

        public static ConsoleColor ForeColor = ConsoleColor.White;
        public static ConsoleColor BackColor = ConsoleColor.DarkBlue;
        public static ConsoleColor TitleColor = ConsoleColor.DarkYellow;

        public static ConsoleColor TitleBoxColor = ConsoleColor.Blue;
        public static ConsoleColor BackBoxColor = ConsoleColor.Gray;
        public static ConsoleColor ForeBoxColor = ConsoleColor.Black;
        public static ConsoleColor SelectedBoxColor = ConsoleColor.DarkRed;

        public static string LeftPanelPath = DriveStatus.InitialDrives()[0];
        public static string RightPanelPath = DriveStatus.InitialDrives()[1];
        public static bool LeftPanelActive = true;
    }
}
