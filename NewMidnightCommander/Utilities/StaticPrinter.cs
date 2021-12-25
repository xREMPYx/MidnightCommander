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

            for (int y = 0; y < ProgramSettings.TableHeight - 1; y++)
            {
                Functions.Write(0,y,String.Empty.PadRight(ProgramSettings.TableWidth - 1));
            }

            for (int y = 1; y < ProgramSettings.TableHeight - 1; y++)
            {
                Functions.Write(0, y, "│");
                Functions.Write(ProgramSettings.TableWidth - 1, y, "│");
                Functions.Write(ProgramSettings.TableWidth / 2, y, "│");
            }

            for (int x = 1; x < ProgramSettings.TableWidth - 1; x++)
            {
                Functions.Write(x, 0, "─");
                Functions.Write(x, ProgramSettings.TableHeight - 2, "─");
            }
            
            for (int x = 1; x < ProgramSettings.TableWidth / 2; x++)
            {
                Functions.Write(x, ProgramSettings.TableHeight - 4, "─");
                Functions.Write(x + ProgramSettings.TableWidth / 2, ProgramSettings.TableHeight - 4, "─");
            }

            for (int i = 1; i < 3; i++)
            {
                Functions.Write(36, i , "│");
                Functions.Write(36 + ProgramSettings.TableWidth / 2, i , "│");
                Functions.Write(46, i , "│");
                Functions.Write(46 + ProgramSettings.TableWidth / 2, i , "│");
            }            

            Functions.Write(ProgramSettings.TableWidth / 2, 0, "┬"); 
            Functions.Write(ProgramSettings.TableWidth / 2, ProgramSettings.TableHeight - 4, "┼"); 
            Functions.Write(ProgramSettings.TableWidth / 2, ProgramSettings.TableHeight - 2, "┴"); 

            Functions.Write(0, 0, "┌"); 
            Functions.Write(0, ProgramSettings.TableHeight - 2, "└"); 
            Functions.Write(ProgramSettings.TableWidth - 1, 0, "┐"); 
            Functions.Write(ProgramSettings.TableWidth - 1, ProgramSettings.TableHeight - 2, "┘");

            Console.ForegroundColor = ProgramSettings.HedlineText;
            Functions.Write(17, 1, "Name");
            Functions.Write(38, 1, "DirSize");
            Functions.Write(52, 1, "MT");

            Functions.Write(17 + ProgramSettings.TableWidth / 2, 1, "Name");
            Functions.Write(38 + ProgramSettings.TableWidth / 2, 1, "DirSize");
            Functions.Write(52 + ProgramSettings.TableWidth / 2, 1, "MT");
        }

        public static void PrintButtons()
        {
            Functions.Button(0, ProgramSettings.TableHeight - 1, "Help", 1);
            Functions.Button(13, ProgramSettings.TableHeight - 1, "Menu", 2);
            Functions.Button(26, ProgramSettings.TableHeight - 1, "View", 3);
            Functions.Button(39, ProgramSettings.TableHeight - 1, "Edit", 4);
            Functions.Button(52, ProgramSettings.TableHeight - 1, "Copy", 5);
            Functions.Button(65, ProgramSettings.TableHeight - 1, "RenMov", 6);
            Functions.Button(78, ProgramSettings.TableHeight - 1, "MkDir", 7);
            Functions.Button(91, ProgramSettings.TableHeight - 1, "Delete", 8);
            Functions.Button(104, ProgramSettings.TableHeight - 1, "ChDrive", 9);    
        }

        public static void PrintSelectedItem(string item, int padRightTable)
        {
            if(item.Length > ProgramSettings.TableWidth / 2 - 1) { item = item.Substring( 0 , ProgramSettings.TableWidth / 2 - 1); }
            Functions.Write(1 + padRightTable, ProgramSettings.TableHeight - 3, item.PadRight(58));
            Console.SetCursorPosition(1, 1);
        }
    }
}
