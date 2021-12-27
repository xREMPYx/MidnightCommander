using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class Disk : IComponent
    {
        public event Action EnterClick;

        private string Name;
        private string TotalSize;
        private string FreeSpace;
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        private DriveInfo drive;
        public Disk(DriveInfo drive)
        {
            this.drive = drive;
            this.Name = drive.Name;
            try { this.TotalSize = DataGetter.ItemSize(drive.TotalSize); } catch { this.TotalSize = "---"; }
            try { this.FreeSpace = DataGetter.ItemSize(drive.TotalFreeSpace); } catch { this.FreeSpace = "---"; }
            this.EnterClick = EnterPressed;
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            switch (info.Key)
            {
                case ConsoleKey.Enter: EnterClick(); break;
            }
        }

        public void Print(bool active)
        {
            if (active) { Console.ForegroundColor = ProgramSettings.SelectedBoxColor; }
            else { Console.ForegroundColor = ProgramSettings.ForeBoxColor; }

            Console.BackgroundColor = ProgramSettings.BackBoxColor;

            Functions.Write(this.PositionX, this.PositionY, this.Name.PadRight(5));
            Functions.Write(this.PositionX, this.PositionY + 2, this.TotalSize.PadRight(5));
            Functions.Write(this.PositionX, this.PositionY + 4, this.FreeSpace.PadRight(5));
        }

        private void EnterPressed()
        {
            if (drive.IsReady)
            {
                if (ProgramSettings.LeftPanelActive)
                {
                    ProgramSettings.LeftPanelPath = this.Name;
                }
                else
                {
                    ProgramSettings.RightPanelPath = this.Name;
                }
                StaticPrinter.PrintTable();
                Application.RenewWindow(1);
            }
            else
            {
                Functions.TextAlert("Selected drive is not ready!");
            }      
        }
    }
}
