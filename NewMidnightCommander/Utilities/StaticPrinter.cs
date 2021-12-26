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
            Console.ForegroundColor = ProgramSettings.ForeColor;
            Console.BackgroundColor = ProgramSettings.BackColor;
          
            for (int y = 1; y < ProgramSettings.PanelHeight - 1; y++)
            {
                Functions.Write(0, y, "│");
                Functions.Write(ProgramSettings.PanelWidth - 1, y, "│");
                Functions.Write(ProgramSettings.PanelWidth / 2, y, "│");
            }

            for (int x = 1; x < ProgramSettings.PanelWidth - 1; x++)
            {
                Functions.Write(x, ProgramSettings.PanelHeight - 2, "─");
            }
            
            for (int x = 1; x < ProgramSettings.PanelWidth / 2; x++)
            {
                Functions.Write(x, ProgramSettings.PanelHeight - 4, "─");
                Functions.Write(x + ProgramSettings.PanelWidth / 2, ProgramSettings.PanelHeight - 4, "─");
            }

            for (int i = 1; i < 3; i++)
            {
                Functions.Write(39, i , "│");
                Functions.Write(39 + ProgramSettings.PanelWidth / 2, i , "│");
                Functions.Write(46, i , "│");
                Functions.Write(46 + ProgramSettings.PanelWidth / 2, i , "│");
            }            

            Functions.Write(ProgramSettings.PanelWidth / 2, 0, "┬"); 
            Functions.Write(ProgramSettings.PanelWidth / 2, ProgramSettings.PanelHeight - 4, "┼"); 
            Functions.Write(ProgramSettings.PanelWidth / 2, ProgramSettings.PanelHeight - 2, "┴"); 

            Functions.Write(0, 0, "┌"); 
            Functions.Write(0, ProgramSettings.PanelHeight - 2, "└"); 
            Functions.Write(ProgramSettings.PanelWidth - 1, 0, "┐"); 
            Functions.Write(ProgramSettings.PanelWidth - 1, ProgramSettings.PanelHeight - 2, "┘");

            Console.ForegroundColor = ProgramSettings.TitleColor;
            for (int i = 0; i < ProgramSettings.PanelWidth / 2 + 1; i += ProgramSettings.PanelWidth / 2)
            {
                Functions.Write(1 + i, 2, String.Empty.PadRight(38));
                Functions.Write(40 + i, 2, String.Empty.PadRight(6));
                Functions.Write(47 + i, 2, String.Empty.PadRight(12));

                Functions.Write(1 + i, 1, "Name".PadLeft(20).PadRight(38));
                Functions.Write(40 + i, 1, "Size".PadLeft(5).PadRight(6));
                Functions.Write(47 + i, 1, "MT".PadLeft(7).PadRight(12));
            }
        }

        public static void PrintButtons()
        {
            Functions.Button(0, ProgramSettings.PanelHeight - 1, "Help", 1);
            Functions.Button(12, ProgramSettings.PanelHeight - 1, "Menu", 2);
            Functions.Button(24, ProgramSettings.PanelHeight - 1, "View", 3);
            Functions.Button(36, ProgramSettings.PanelHeight - 1, "Edit", 4);
            Functions.Button(48, ProgramSettings.PanelHeight - 1, "Copy", 5);
            Functions.Button(60, ProgramSettings.PanelHeight - 1, "RenMov", 6);
            Functions.Button(72, ProgramSettings.PanelHeight - 1, "MkDir", 7);
            Functions.Button(84, ProgramSettings.PanelHeight - 1, "Delete", 8);
            Functions.Button(96, ProgramSettings.PanelHeight - 1, "SDrive", 9);    
            Functions.Button(108, ProgramSettings.PanelHeight - 1, "Quit", 10);    
        }

        public static void PrintSelectedItem(string item, int padRightTable)
        {
            if(item.Length > ProgramSettings.PanelWidth / 2 - 1) { item = item.Substring( 0 , ProgramSettings.PanelWidth / 2 - 1); }
            Functions.Write(1 + padRightTable, ProgramSettings.PanelHeight - 3, item.PadRight(58));
            Console.SetCursorPosition(1, 1);        
        }

        public static void PrintPath(string path, int padRightTable)
        {
            if(path.Length - 1 > ProgramSettings.PanelWidth / 2 - 4) { path = path.Substring(path.Length - 56, 56); }

            if(padRightTable > 0 && ProgramSettings.LeftPanelActive != true) { Console.BackgroundColor = ProgramSettings.SelectedBackColor; }
            if(padRightTable == 0 && ProgramSettings.LeftPanelActive) { Console.BackgroundColor = ProgramSettings.SelectedBackColor; }
            Functions.Write(2 + padRightTable, 0, path);

            Console.ForegroundColor = ProgramSettings.ForeColor;
            Console.BackgroundColor = ProgramSettings.BackColor;

            Functions.Write(1 + padRightTable, 0, "─");
            Functions.Write(2 + path.Length + padRightTable, 0, "─".PadRight(57 - path.Length, '─'));

            Console.SetCursorPosition(1, 1);
        }
    }
}
