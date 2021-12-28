using System;
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

        private int Top;
        private int Selected;
        private int PadRightPanel;

        private bool LeftPanel;
        private bool IsFindItemOn;
        private bool IsMarkItemsOn;
        private bool IsMarkItemsAfterF1On;

        private List<string[]> Files;

        private FindItem findItem;
        private MarkFiles markFiles;

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

            this.Top = 0;
            this.Selected = 0;
            this.PadRightPanel = 0;
        }

        public void Print(bool active)
        {
            this.CheckPath();
            this.UpdateList();
            this.SelectedCondition();

            Console.ForegroundColor = ProgramSettings.PanelForeColor;
            Console.BackgroundColor = ProgramSettings.PanelBackColor;

            for (int i = Top; i < ProgramSettings.PanelDataHeight + Top; i++)
            {
                if (i < this.Files.Count)
                {
                    if (((this.IsMarkItemsOn == false) || (this.IsMarkItemsOn == true) && (this.IsMarkItemsAfterF1On == true)) && i == this.Selected && ((ProgramSettings.LeftPanelActive && this.LeftPanel) || (ProgramSettings.LeftPanelActive == false && this.LeftPanel == false)))
                    {                    
                        Console.ForegroundColor = ProgramSettings.PanelSelectedForeColor;
                    }

                    if (this.IsMarkItemsOn && IfNameIsEqual(this.Files[i][0], markFiles.MarkedFileNames) && ((ProgramSettings.LeftPanelActive && this.LeftPanel) || (ProgramSettings.LeftPanelActive == false && this.LeftPanel == false)))
                    {
                        Console.ForegroundColor = ProgramSettings.PanelMarkForeSelected;
                    }

                    if (i == this.Selected && ((ProgramSettings.LeftPanelActive && this.LeftPanel) || (ProgramSettings.LeftPanelActive == false && this.LeftPanel == false))) { Console.BackgroundColor = ProgramSettings.PanelSelectedBackColor; }

                    string subItem = this.Files[i][0];
                    if (this.Files[i][0].Length > 35) { subItem = this.Files[i][0].Substring(0, 35); }

                    Functions.Write(1 + this.PadRightPanel, i + 4 - this.Top, subItem.PadRight(38));          
                    
                    if (i == this.Selected) { Console.ForegroundColor = ProgramSettings.PanelSelectedForeColor; } 
                    else { Console.ForegroundColor = ProgramSettings.PanelForeColor; }

                    Functions.Write(41 + this.PadRightPanel, i + 4 - this.Top, this.Files[i][1].PadRight(5));
                    Functions.Write(48 + this.PadRightPanel, i + 4 - this.Top, this.Files[i][2].PadRight(11));
                    Functions.Write(39 + this.PadRightPanel, i + 4 - this.Top, "│".PadRight(2));
                    Functions.Write(46 + this.PadRightPanel, i + 4 - this.Top, "│".PadRight(2));

                    Console.ForegroundColor = ProgramSettings.PanelForeColor;
                    Console.BackgroundColor = ProgramSettings.PanelBackColor;
                }
                else
                {
                    Functions.Write(1 + this.PadRightPanel, i + 4 - this.Top, string.Empty.PadRight(38));
                    Functions.Write(41 + this.PadRightPanel, i + 4 - this.Top, string.Empty.PadRight(5));
                    Functions.Write(48 + this.PadRightPanel, i + 4 - this.Top, string.Empty.PadRight(11));
                    Functions.Write(39 + this.PadRightPanel, i + 4 - this.Top, "│".PadRight(2));
                    Functions.Write(46 + this.PadRightPanel, i + 4 - this.Top, "│".PadRight(2));
                }
                Console.SetCursorPosition(1, 2);
            }            

            if (this.IsFindItemOn) 
            {
                StaticPrinter.PrintSelectedItem('\\' + findItem.sb.ToString(), PadRightPanel);
            } 
            else
            {
                StaticPrinter.PrintSelectedItem(this.Files[this.Selected][0], PadRightPanel);
            }

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
            else if (info.Key == ConsoleKey.Home) { Home(); }           
            else if (info.Key == ConsoleKey.F1) { SetMarkFilesBools(); }
            else if (info.Key == ConsoleKey.F2) { MkFile(); }
            else if (info.Key == ConsoleKey.F4) { Edit(); }
            else if (info.Key == ConsoleKey.F7) { MkDir(); }
            else if (info.Key == ConsoleKey.F9) { ChangeDrive(); }
            else if (info.Key == ConsoleKey.F5 && IsSelectedItem()) { Copy(); }
            else if (info.Key == ConsoleKey.F6 && IsSelectedItem()) { RenMov(); }
            else if (info.Key == ConsoleKey.F8 && IsSelectedItem()) { Delete(); }
            else if (info.Key == ConsoleKey.F && IsFindItemOn == false) { this.findItem = new FindItem(this.Files); IsFindItemOn = true; }
          
            if (Reset(info, 0)) { ResetMark(); }
            if (Reset(info, 1)) { this.IsFindItemOn = false; }

            if (this.IsFindItemOn) { FindItem(info); }
            if (this.IsMarkItemsOn && this.IsMarkItemsAfterF1On == false) { MarkFiles(); }
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
            if (this.Selected == this.Top + ProgramSettings.PanelDataHeight) { this.Top++; }
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

        private void Home() 
        {
            if(ProgramSettings.LeftPanelActive) 
            {
                ProgramSettings.LeftPanelPath = this.InitialPath;
            }
            else
            {
                ProgramSettings.RightPanelPath = this.InitialPath;
            }
            this.Path = this.InitialPath;  
        }

        // File Actions

        private void Edit()
        {
            if (!this.Files[this.Selected][0].EndsWith(".txt") || this.IsMarkItemsOn) 
            {
                return;
            }
            Application.SaveLastWindow();
            Application.window = new TextFileEditorWindow(this.Path + '\\' + this.Files[this.Selected][0].Remove(0, 1));
        }

        private void Copy()
        {
            string destinationPath;
            if (ProgramSettings.LeftPanelActive) 
            {
                destinationPath = ProgramSettings.RightPanelPath; 
            }
            else 
            { 
                destinationPath = ProgramSettings.LeftPanelPath; 
            }

            Application.SaveLastWindow();
            if (this.IsMarkItemsOn)
            {
                Application.window = new CopyFiles(this.Path, this.markFiles.MarkedFileNames, destinationPath.ToString());
            }
            else
            {
                Application.window = new Copy(this.Path + this.Files[this.Selected][0].Remove(0, 1).ToString(), destinationPath + this.Files[this.Selected][0].Remove(0, 1).ToString());
            }            
        }

        private void Delete()
        {
            Application.SaveLastWindow();

            if (this.IsMarkItemsOn)
            {
                Application.window = new DeleteFiles(this.Path, this.markFiles.MarkedFileNames);
            }
            else
            {
                Application.window = new Delete(this.Path + this.Files[this.Selected][0].Remove(0, 1).ToString());
            }
        }

        private void RenMov()
        {
            if (this.IsMarkItemsOn == false)
            {
                Application.SaveLastWindow();
                Application.window = new RenMov(this.Path + this.Files[Selected][0].Remove(0, 1).ToString());
            }          
        }

        private void MkDir()
        {
            if (this.IsMarkItemsOn == false)
            {
                Application.SaveLastWindow();
                Application.window = new MkDir(this.Path);
            }            
        }

        private void MkFile()
        {
            if (this.IsMarkItemsOn == false)
            {
                Application.SaveLastWindow();
                Application.window = new MkFile(this.Path);
            }          
        }

        private void ChangeDrive()
        {
            this.PageUp();
            Application.SaveLastWindow();
            Application.window = new ChangeDrive();
        }

        // Other Actions

        private void SetMarkFilesBools()
        {
            if( this.IsMarkItemsOn == false && this.IsMarkItemsAfterF1On == false && this.IsSelectedItem()) 
            {
                this.IsMarkItemsOn = true; this.IsMarkItemsAfterF1On = false;
                this.markFiles = new MarkFiles(this.Files);
                this.markFiles.startFileSelected = this.Selected;
            }
            else if( this.IsMarkItemsOn == true && this.IsMarkItemsAfterF1On == false) { this.IsMarkItemsOn = true; this.IsMarkItemsAfterF1On = true; }
            else if( this.IsMarkItemsOn == true && this.IsMarkItemsAfterF1On == true) { this.IsMarkItemsOn = false; this.IsMarkItemsAfterF1On = false; }
        }

        private void MarkFiles()
        {
            if (IsSelectedItem())
            {
                this.markFiles.lastFileSelected = this.Selected;
            }        
            this.markFiles.AddFiles();
        }

        private void FindItem(ConsoleKeyInfo info)
        {
            this.findItem.Find(info);
            this.Selected = this.findItem.selectedForFileManager(this.Selected);

            if(this.Selected > this.Top + 22 || this.Selected < this.Top)
            {
                this.Top = this.Selected - 22;
            }
            
            if (this.Top < 0) { this.Top = 0; }
        }

        // Mark utilites

        private void ResetMark()
        {
            this.IsMarkItemsOn = false;
            this.IsMarkItemsAfterF1On = false;
        }

        private bool IfNameIsEqual(string fileName, List<string> markedFiles)
        {
            for (int i = 0; i < markedFiles.Count; i++)
            {
                if (fileName.Trim(' ').Trim('\\') == markedFiles[i].ToString()) { return true; }
            }
            return false;
        }

        // Others

        private bool Reset(ConsoleKeyInfo info, int type)
        {
            // Type 1 is for FindFiles, any else number is for MarkFiles.

            if (type == 1)
            {
                if (info.Key == ConsoleKey.UpArrow) { return true; }
                else if (info.Key == ConsoleKey.DownArrow) { return true; }
                else if (info.Key == ConsoleKey.PageUp) { return true; }
                else if (info.Key == ConsoleKey.PageDown) { return true; }
            }

            else if (info.Key == ConsoleKey.Enter) { return true; }
            else if (info.Key == ConsoleKey.Tab) { return true; }
            else if (info.Key == ConsoleKey.F3) { return true; }
            else if (info.Key == ConsoleKey.F4) { return true; }
            else if (info.Key == ConsoleKey.F5) { return true; }
            else if (info.Key == ConsoleKey.F6) { return true; }
            else if (info.Key == ConsoleKey.F7) { return true; }
            else if (info.Key == ConsoleKey.F8) { return true; }
            else if (info.Key == ConsoleKey.F9) { return true; }

            return false;
        }

        private bool IsSelectedItem()
        {
            if (this.Selected != 0 || this.Selected == 0 && this.Path == this.InitialPath) { return true; }
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
            if (this.LeftPanel) { ProgramSettings.LeftPanelPath = this.Path; }
            else { ProgramSettings.RightPanelPath = this.Path; }
        }

        private void CheckPath()
        {
            if (this.LeftPanel)
            {
                if (this.Path != ProgramSettings.LeftPanelPath)
                {
                    this.Path = ProgramSettings.LeftPanelPath;
                    this.InitialPath = ProgramSettings.LeftPanelPath;
                }
            }
            else
            {
                if (this.Path != ProgramSettings.RightPanelPath)
                {
                    this.Path = ProgramSettings.RightPanelPath;
                    this.InitialPath = ProgramSettings.RightPanelPath;
                }
            }
        }

        private void SelectedCondition()
        {
            if (this.Selected > this.Files.Count - 1) { this.PageDown(); }
        }
    }
}
