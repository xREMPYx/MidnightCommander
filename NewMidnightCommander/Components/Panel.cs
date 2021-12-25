using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    internal class Panel : IComponent
    {
        private string Path { get; set; }
        private int Selected { get; set; } = 0;
        private int Top { get; set; } = 0;
        public Panel(bool leftTable)
        {
            if (leftTable) { Path = DriveStatus.InitialDrives()[0]; }
            else { Path = DriveStatus.InitialDrives()[1]; }
        }

        public void Print()
        {
            for (int i = Top; i < ProgramSettings.PanelHeight + Top; i++)
            {
                
            }
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            switch (info.Key)
            {
                case ConsoleKey.UpArrow: SelectUp(); break;
                case ConsoleKey.DownArrow: SelectDown(); break;
                case ConsoleKey.Enter: Enter(); break;
            }
        }

        // HandleKey Actions

        private void SelectUp()
        {
            if (Selected > 0) { Selected--; }
        }

        private void SelectDown()
        {
            if (Selected < DataGetter.Files(Path).Count) { Selected++; }
        }

        private void Enter()
        {

        }
    }
}
