﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class Panel : IComponent
    {
        public string Path;
        private string InitialPath;

        private int Top = 0;
        private int Selected  = 0;
        private int PadRightPanel = 0;

        private bool LeftPanel;

        private List<string[]> Files;

        public Panel(bool leftPanel)
        {
            if (leftPanel)
            {
                this.Path = ProgramSettings.LeftPanelPath;
                this.InitialPath = this.Path;
                this.LeftPanel = true;
            }
            else
            {
                this.Path = ProgramSettings.RightPanelPath;
                this.InitialPath = this.Path;
                this.PadRightPanel = ProgramSettings.PanelWidth / 2;
                this.LeftPanel = false;
            }
            this.Files = DataGetter.Files(this.Path);
        }

        public void Print(bool active)
        {
            this.CheckPath();
            this.UpdateList();
            this.SelectedCondition();

            Console.ForegroundColor = ProgramSettings.ForeColor;
            Console.BackgroundColor = ProgramSettings.BackColor;

            for (int i = Top; i < ProgramSettings.PanelDataHeight + Top - 2; i++)
            {
                if (i < this.Files.Count)
                {
                    if (i == this.Selected && ((ProgramSettings.LeftPanelActive && this.LeftPanel) || (ProgramSettings.LeftPanelActive == false && this.LeftPanel == false)))
                    {
                        Console.ForegroundColor = ProgramSettings.SelectedForeColor;
                        Console.BackgroundColor = ProgramSettings.SelectedBackColor;
                    }

                    string subItem = this.Files[i][0];
                    if (this.Files[i][0].Length > 35) { subItem = this.Files[i][0].Substring(0, 35); }

                    Functions.Write(1 + this.PadRightPanel, i + 3 - Top, subItem.PadRight(38));
                    Functions.Write(41 + this.PadRightPanel, i + 3 - Top, this.Files[i][1].PadRight(5));
                    Functions.Write(48 + this.PadRightPanel, i + 3 - Top, this.Files[i][2].PadRight(11));
                    Functions.Write(39 + this.PadRightPanel, i + 3 - Top, "│".PadRight(2));
                    Functions.Write(46 + this.PadRightPanel, i + 3 - Top, "│".PadRight(2));


                    if (i == this.Selected)
                    {
                        Console.ForegroundColor = ProgramSettings.ForeColor;
                        Console.BackgroundColor = ProgramSettings.BackColor;
                    }
                }
                else
                {
                    Functions.Write(1 + this.PadRightPanel, i + 3 - Top, string.Empty.PadRight(38));
                    Functions.Write(41 + this.PadRightPanel, i + 3 - Top, string.Empty.PadRight(5));
                    Functions.Write(48 + this.PadRightPanel, i + 3 - Top, string.Empty.PadRight(11));
                    Functions.Write(39 + this.PadRightPanel, i + 3 - Top, "│".PadRight(2));
                    Functions.Write(46 + this.PadRightPanel, i + 3 - Top, "│".PadRight(2));
                }
                Console.SetCursorPosition(1, 1);
            }
            StaticPrinter.PrintSelectedItem(this.Files[this.Selected][0], PadRightPanel);
            StaticPrinter.PrintPath(this.Path, this.PadRightPanel);
            Functions.ReadKeyError();
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.UpArrow) { SelectUp(); }
            else if (info.Key == ConsoleKey.DownArrow) { SelectDown(); }
            else if (info.Key == ConsoleKey.Enter) { Enter(); }
            else if (info.Key == ConsoleKey.PageUp) { PageUp(); }
            else if (info.Key == ConsoleKey.PageDown) { PageDown(); }
            else if (info.Key == ConsoleKey.Tab) { SwitchPanel(); }
            else if (info.Key == ConsoleKey.F7) { MkDir(); }
            else if (info.Key == ConsoleKey.F9) { ChangeDrive(); }
            else if (info.Key == ConsoleKey.F5 && IsSelectedItem()) { Copy(); }
            else if (info.Key == ConsoleKey.F6 && IsSelectedItem()) { RenMov(); }
            else if (info.Key == ConsoleKey.F8 && IsSelectedItem()) { Delete(); }
        }

        // Move Actions

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
            if ((this.Selected != 0 || this.Path == this.InitialPath) && this.Files[this.Selected][0][0] != ' ')
            {
                try { DirectoryInfo directoryInfo = new(this.Path + this.Files[this.Selected][0].Remove(0, 1)); directoryInfo.GetDirectories(); }
                catch { Functions.TextAlert("Directory cannot be opened!"); return; }

                this.Path = this.Path + this.Files[this.Selected][0].Remove(0, 1) + '\\';
                this.Selected = 0;
                this.Top = 0;
            }
            else if (this.Selected == 0 && this.Path != this.InitialPath)
            {
                var subPaths = this.Path.Split('\\');
                string newpath = string.Empty;

                for (int i = 0; i < subPaths.Length - 2; i++)
                {
                    newpath += subPaths[i] + '\\';
                }

                this.Path = newpath;
                this.Selected = 0;
                this.Top = 0;
            }
         
            this.SetOtherPanelPath();
        }

        private void PageUp()
        {
            this.Top = 0;
            this.Selected = 0;
        }

        private void PageDown()
        {
            this.Selected = this.Files.Count - 1;
            this.Top = this.Files.Count - ProgramSettings.PanelHeight + 7;

            if(this.Top < 0) { this.Top = 0; }
        }

        private void SwitchPanel()
        {
            ProgramSettings.LeftPanelActive = !ProgramSettings.LeftPanelActive;
        }

        // File Actions

        private void Copy()
        {
            string destinationPath;
            if (LeftPanel) 
            {
                destinationPath = ProgramSettings.RightPanelPath; 
            }
            else 
            { 
                destinationPath = ProgramSettings.LeftPanelPath; 
            }

            Application.SaveLastWindow();
            Application.window = new Copy(this.Path + this.Files[Selected][0].Remove(0,1).ToString(), destinationPath + this.Files[Selected][0].Remove(0, 1).ToString());
        }

        private void Delete()
        {
            Application.SaveLastWindow();
            Application.window = new Delete(this.Path + this.Files[Selected][0].Remove(0, 1).ToString());
        }

        private void RenMov()
        {
            Application.SaveLastWindow();
            Application.window = new RenMov(this.Path + this.Files[Selected][0].Remove(0, 1).ToString());
        }

        private void MkDir()
        {
            Application.SaveLastWindow();
            Application.window = new MkDir(Path);
        }

        private void ChangeDrive()
        {
            Application.SaveLastWindow();
            Application.window = new ChangeDrive();
        }

        // Others

        private bool IsSelectedItem()
        {
            if(Selected != 0 || Selected == 0 && Path == InitialPath) { return true; }
            return false;
        }

        private void UpdateList()
        {
            this.Files = DataGetter.Files(this.Path);

            if (this.Path != this.InitialPath)
            {
                string[] goBackRow = { @"\..", string.Empty, string.Empty };
                this.Files.Insert(0, goBackRow);
            }
        }

        private void SetOtherPanelPath()
        {
            if (LeftPanel) { ProgramSettings.LeftPanelPath = Path; }
            else { ProgramSettings.RightPanelPath = Path; }
        }

        private void CheckPath()
        {
            if (LeftPanel) 
            { 
                if (Path != ProgramSettings.LeftPanelPath) 
                { 
                    Path = ProgramSettings.LeftPanelPath; 
                    InitialPath = ProgramSettings.LeftPanelPath; 
                } 
            }
            else 
            { 
                if (Path != ProgramSettings.RightPanelPath) 
                { 
                    Path = ProgramSettings.RightPanelPath;
                    InitialPath = ProgramSettings.RightPanelPath; 
                }
            }
        }

        private void SelectedCondition()
        {
            if(Selected > this.Files.Count - 1) { Selected = this.Files.Count - 1; }
        }
    }
}
