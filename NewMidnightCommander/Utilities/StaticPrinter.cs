using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    internal class StaticPrinter
    {
        public static void PrintTable(bool leftTable)
        {
            for (int y = 0; y < ProgramSettings.TableHeight; y++)
            {
                for (int x = 0; x < ProgramSettings.TableWidth; x++)
                {
                    if (leftTable) { Functions.Write(x, y, ""); }
                    else { Functions.Write(x + ProgramSettings.TableWidth, y, ""); }                   
                }
            }
        }
    }
}
