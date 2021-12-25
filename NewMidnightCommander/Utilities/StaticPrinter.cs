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

            Functions.Write(ProgramSettings.TableWidth / 2, 0, "┬"); 
            Functions.Write(ProgramSettings.TableWidth / 2, ProgramSettings.TableHeight - 4, "┼"); 
            Functions.Write(ProgramSettings.TableWidth / 2, ProgramSettings.TableHeight - 2, "┴"); 

            Functions.Write(0, 0, "┌"); 
            Functions.Write(0, ProgramSettings.TableHeight - 2, "└"); 
            Functions.Write(ProgramSettings.TableWidth - 1, 0, "┐"); 
            Functions.Write(ProgramSettings.TableWidth - 1, ProgramSettings.TableHeight - 2, "┘"); 
        }
    }
}
