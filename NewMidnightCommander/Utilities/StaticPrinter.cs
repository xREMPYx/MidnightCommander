using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public static class StaticPrinter
    {
        // │ ┐ ┌ └ ┘ ├ ┤ ┬ ┴ ┼ ─
        public static void PrintTable()
        {          
            Console.ForegroundColor = ProgramSettings.MenuForeColor;
            Console.BackgroundColor = ProgramSettings.MenuBackColor;

            Functions.Write(0, 0, PrintUpperMenu().PadRight(ProgramSettings.PanelWidth));

            Console.ForegroundColor = ProgramSettings.PanelForeColor;
            Console.BackgroundColor = ProgramSettings.PanelBackColor;
          
            for (int y = 2; y < ProgramSettings.PanelHeight; y++)
            {
                Functions.Write(0, y, "│");
                Functions.Write(ProgramSettings.PanelWidth - 1, y, "│");
                Functions.Write(ProgramSettings.PanelWidth / 2, y, "│");
            }

            for (int x = 1; x < ProgramSettings.PanelWidth - 1; x++)
            {
                Functions.Write(x, ProgramSettings.PanelHeight - 3, "─");
            }
            
            for (int x = 1; x < ProgramSettings.PanelWidth / 2; x++)
            {
                Functions.Write(x, ProgramSettings.PanelHeight - 1, "─");
                Functions.Write(x + ProgramSettings.PanelWidth / 2, ProgramSettings.PanelHeight - 1, "─");
            }

            for (int y = 2; y < 4; y++)
            {
                Functions.Write(39, y , "│");
                Functions.Write(39 + ProgramSettings.PanelWidth / 2, y , "│");
                Functions.Write(46, y , "│");
                Functions.Write(46 + ProgramSettings.PanelWidth / 2, y , "│");
            }            

            Functions.Write(ProgramSettings.PanelWidth / 2, 1, "┬"); 
            Functions.Write(ProgramSettings.PanelWidth / 2, ProgramSettings.PanelHeight - 3, "┼"); 
            Functions.Write(ProgramSettings.PanelWidth / 2, ProgramSettings.PanelHeight - 1, "┴"); 

            Functions.Write(0, 1, "┌"); 
            Functions.Write(0, ProgramSettings.PanelHeight - 1, "└"); 
            Functions.Write(ProgramSettings.PanelWidth - 1, 1, "┐"); 
            Functions.Write(ProgramSettings.PanelWidth - 1, ProgramSettings.PanelHeight - 1, "┘");

            Console.ForegroundColor = ProgramSettings.PanelTitleBackColor;
            for (int i = 0; i < ProgramSettings.PanelWidth / 2 + 1; i += ProgramSettings.PanelWidth / 2)
            {
                Functions.Write(1 + i, 3, String.Empty.PadRight(38));
                Functions.Write(40 + i, 3, String.Empty.PadRight(6));
                Functions.Write(47 + i, 3, String.Empty.PadRight(12));

                Functions.Write(1 + i, 2, "Name".PadLeft(20).PadRight(38));
                Functions.Write(40 + i, 2, "Size".PadLeft(5).PadRight(6));
                Functions.Write(47 + i, 2, "MT".PadLeft(7).PadRight(12));
            }
            PrintButtons();
        }

        private static void PrintButtons()
        {
            Functions.Button(0, ProgramSettings.PanelHeight, "Help", 1);
            Functions.Button(12, ProgramSettings.PanelHeight, "Menu", 2);
            Functions.Button(24, ProgramSettings.PanelHeight, "View", 3);
            Functions.Button(36, ProgramSettings.PanelHeight, "Edit", 4);
            Functions.Button(48, ProgramSettings.PanelHeight, "Copy", 5);
            Functions.Button(60, ProgramSettings.PanelHeight, "RenMov", 6);
            Functions.Button(72, ProgramSettings.PanelHeight, "MkDir", 7);
            Functions.Button(84, ProgramSettings.PanelHeight, "Delete", 8);
            Functions.Button(96, ProgramSettings.PanelHeight, "SDrive", 9);    
            Functions.Button(108, ProgramSettings.PanelHeight, "Quit", 10);    
        }

        private static string PrintUpperMenu()
        {
            string space = string.Empty.PadRight(8);
            string menu = string.Empty.PadRight(5);
            menu += "Left" + space;
            menu += "File" + space;
            menu += "Command" + space;
            menu += "Options" + space;
            menu += "Right" + space;
            return menu;
        }

        public static void PrintSelectedItem(string item, int padRightTable)
        {
            if(item.Length > ProgramSettings.PanelWidth / 2 - 1) { item = item.Substring( 0 , ProgramSettings.PanelWidth / 2 - 1); }
            Functions.Write(1 + padRightTable, ProgramSettings.PanelHeight - 2, item.PadRight(58));
            Console.SetCursorPosition(1, 2);        
        }

        public static void PrintPath(string path, int padRightTable)
        {
            if(path.Length - 1 > ProgramSettings.PanelWidth / 2 - 4)
            { 
                path = path.Substring(path.Length - 56, 56);
            }

            if(padRightTable > 0 && ProgramSettings.LeftPanelActive != true) 
            {                
                Console.ForegroundColor = ProgramSettings.PanelSelectedForeColor;
                Console.BackgroundColor = ProgramSettings.PanelSelectedBackColor;
            }

            if(padRightTable == 0 && ProgramSettings.LeftPanelActive)
            {             
                Console.ForegroundColor = ProgramSettings.PanelSelectedForeColor;
                Console.BackgroundColor = ProgramSettings.PanelSelectedBackColor;
            }

            Functions.Write(2 + padRightTable, 1, path);

            Console.ForegroundColor = ProgramSettings.PanelForeColor;
            Console.BackgroundColor = ProgramSettings.PanelBackColor;

            Functions.Write(1 + padRightTable, 1, "─");
            Functions.Write(2 + path.Length + padRightTable, 1, "─".PadRight(57 - path.Length, '─'));

            Console.SetCursorPosition(1, 1);
        }
    }
}
