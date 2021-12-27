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
                disks.Add(disk);
            }
        }

        public override void Print(bool active)
        {
            int Pad = 14;
            for (int i = Top; i < 5 + Top; i++)
            {             
                Console.CursorVisible = false;
                disks[i].PositionX = PositionX + Pad;
                disks[i].PositionY = PositionY;
                disks[i].Print(i == this.Selected);
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
                if (Selected > 4) { Top++; } else { Top = 0; }
            }
        }
    }
}
