using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class ContainerChangeDrive : Container
    {
        private int Top = 0;
        private int PositionX;
        private int PositionY;

        private List<Disk> disks = new List<Disk>();

        public ContainerChangeDrive(int x, int y)
        {
            this.PositionX = x;
            this.PositionY = y;

            foreach (DriveInfo drive in DriveStatus.allDrives)
            {
                Disk disk = new Disk(drive);
                this.disks.Add(disk);
            }
        }

        public override void Print(bool active)
        {
            int Pad = 14;
            int repeatCount = this.disks.Count; if (repeatCount > 5) { repeatCount = 0; }
            for (int i = Top; i < repeatCount + Top; i++)
            {             
                Console.CursorVisible = false;
                this.disks[i].PositionX = this.PositionX + Pad;
                this.disks[i].PositionY = this.PositionY;
                this.disks[i].Print(i == this.Selected);
                Pad += 7;
            }                  
            
            Functions.ReadKeyError();
        }

        public override void HandleKey(ConsoleKeyInfo info)
        {
            this.disks[this.Selected].HandleKey(info);

            if (info.Key == ConsoleKey.Tab)
            {
                this.Selected = (this.Selected + 1) % this.disks.Count;
                if (this.Selected > 4) { this.Top++; } else { this.Top = 0; }
            }
        }
    }
}
