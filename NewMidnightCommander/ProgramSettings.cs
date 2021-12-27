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
      
        public static ConsoleColor ButtonForeColor = ConsoleColor.Black;
        public static ConsoleColor ButtonBackColor = ConsoleColor.DarkCyan;

        public static ConsoleColor AlertTextColor = ConsoleColor.Red;

        public static ConsoleColor MenuForeColor = ButtonForeColor;
        public static ConsoleColor MenuBackColor = ButtonBackColor;

        public static ConsoleColor PanelForeColor = ConsoleColor.White;
        public static ConsoleColor PanelBackColor = ConsoleColor.DarkBlue;        
        public static ConsoleColor PanelSelectedForeColor = ConsoleColor.White;
        public static ConsoleColor PanelSelectedBackColor = ConsoleColor.Blue;
        public static ConsoleColor PanelTitleBackColor = ConsoleColor.DarkYellow;

        public static ConsoleColor BoxForeColor = ConsoleColor.Black;
        public static ConsoleColor BoxBackColor = ConsoleColor.Gray; 
        public static ConsoleColor BoxSelectedColor = ConsoleColor.DarkRed;
        public static ConsoleColor BoxTitleColor = ConsoleColor.Blue;

        public static string LeftPanelPath = DriveStatus.InitialDrives()[0];
        public static string RightPanelPath = DriveStatus.InitialDrives()[1];
        public static bool LeftPanelActive = true;
    }
}
