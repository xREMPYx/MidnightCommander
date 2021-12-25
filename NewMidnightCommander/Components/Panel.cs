using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class Panel : IComponent
    {
        private string Path { get; set; }
        private string InitialPath { get; set; }
        private int Selected { get; set; } = 0;
        private int Top { get; set; } = 0;
        private int PadRightTable { get; set; } = 0;
        private bool LeftTable { get; set; }
        List<string[]> Files { get; set; }
        public Panel(bool leftTable)
        {
            if (leftTable) 
            {
                this.Path = DriveStatus.InitialDrives()[0];
                this.InitialPath = Path;
                this.LeftTable = true;
            }
            else 
            {
                this.Path = DriveStatus.InitialDrives()[1];
                this.InitialPath = Path;
                this.PadRightTable = ProgramSettings.TableWidth / 2;
                this.LeftTable = false;
            }
            this.Files = DataGetter.Files(Path);
        }

        public void Print()
        {
            Console.ForegroundColor = ProgramSettings.ForeColor;
            Console.BackgroundColor = ProgramSettings.BackColor;

            for (int i = Top; i < ProgramSettings.PanelDataHeight + Top - 2; i++)
            {
                if(i < this.Files.Count)
                {
                    if(i == this.Selected && ((Container.IsLeftSelected && this.LeftTable) || (Container.IsLeftSelected == false && this.LeftTable == false)))
                    {
                        Console.ForegroundColor = ProgramSettings.SelectedForeColor;
                        Console.BackgroundColor = ProgramSettings.SelectedBackColor;
                    }

                    Functions.Write(1 + this.PadRightTable, i + 3 - Top, this.Files[i][0].PadRight(ProgramSettings.TableWidth / 2 - 1));

                    if (i == this.Selected)
                    {
                        Console.ForegroundColor = ProgramSettings.ForeColor;
                        Console.BackgroundColor = ProgramSettings.BackColor;
                    }
                }
                else
                {
                    Functions.Write(1 + this.PadRightTable, i + 3 - Top, String.Empty.PadRight(ProgramSettings.TableWidth / 2 - 1));
                }
                Console.SetCursorPosition(1, 1);    
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
            if (this.Selected > 0) { this.Selected--; }
            if (this.Selected == Top - 1) { this.Top--; }
        }

        private void SelectDown()
        {
            if (this.Selected < this.Files.Count - 1) { this.Selected++; }
            if (this.Selected == this.Top + ProgramSettings.PanelDataHeight - 2) { this.Top++; }
        }

        private void Enter()
        {
            if((this.Selected != 0 || this.Path == this.InitialPath) && this.Files[this.Selected][0][0] != ' ')
            {
                this.Path = this.Path + this.Files[this.Selected][0].Trim('\\').Trim(' ') + '\\';
            }
            else if(this.Selected == 0 && this.Path != this.InitialPath)
            {
                var subPaths = this.Path.Split('\\');
                string newpath = string.Empty;

                for (int i = 0; i < subPaths.Length - 2; i++)
                {
                    newpath += subPaths[i] + '\\';
                }

                this.Path = newpath;
            }

            this.Files = DataGetter.Files(this.Path);

            if(this.Path != this.InitialPath)
            {
                string[] goBackRow = { @"\..", string.Empty, string.Empty };
                this.Files.Insert(0, goBackRow);
            }

            this.Selected = 0;
            this.Top = 0;
        }
    }
}
