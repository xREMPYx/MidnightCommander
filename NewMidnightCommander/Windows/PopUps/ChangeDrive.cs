using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class ChangeDrive : PopUpWindow
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public ChangeDrive()
        {
            this.Height = 8;
            this.Width = 50;
            this.Title = "ChangeDrive";
            this.PositionX = (ProgramSettings.PanelWidth / 2) - (this.Width / 2) + 1;
            this.PositionY = (ProgramSettings.PanelHeight / 2) - (this.Height / 2) + 2;
      
            this.ForeColor = ConsoleColor.Black;
            this.BackColor = ConsoleColor.Gray;

            this.ShowAdditional = false;

            ContainerChangeDrive containerChangeDrive = new ContainerChangeDrive(PositionX,PositionY);     

            this.component = containerChangeDrive;

            this.PrintBox();
        }

        public override void PrintBox()
        {
            base.PrintBox(); ;
            Console.ForegroundColor = this.ForeColor;
            Functions.Write(PositionX, PositionY, "Name:");
            Functions.Write(PositionX, PositionY + 2, "Total size:");
            Functions.Write(PositionX, PositionY + 4, "Free Space:");
        }
    }
}
