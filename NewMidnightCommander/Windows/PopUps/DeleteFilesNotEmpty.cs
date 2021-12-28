using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class DeleteFilesNotEmpty : PopUpWindow
    {
        private string SourcePath;

        private List<string> FileNames;

        public DeleteFilesNotEmpty(string sourcePath, List<string> fileNames)
        {
            this.Height = 5;
            this.Width = 50;
            this.Title = "Delete Alert";
            this.AdditionalText = "Not all of selected directories are empty!".PadLeft(this.Width/2 + 20);
            this.SecondAdditionalText = "Are you sure you want to delete it?".PadLeft(this.Width - 8);
            this.ForeColor = ConsoleColor.Black;
            this.BackColor = ConsoleColor.DarkRed;
            this.TitleColor = ConsoleColor.Black;
            this.ItemBackColor = ConsoleColor.DarkRed;
            this.FileNames = fileNames;
            this.SourcePath = sourcePath;
            this.ShowAdditional = true;

            Container container = new Container();

            Button OkButton = new Button((ProgramSettings.PanelWidth / 2) - (this.Width / 2) + 7, (ProgramSettings.PanelHeight / 2) + (this.Height / 2) - 1, "OK") 
            {
                BackColor = ConsoleColor.DarkRed,
                SelectedColor = ConsoleColor.Gray
            };

            Button CancelButton = new Button((ProgramSettings.PanelWidth / 2) - (this.Width / 2) + 30, (ProgramSettings.PanelHeight / 2) + (this.Height / 2) - 1, "Cancel")
            { 
                BackColor = ConsoleColor.DarkRed, 
                SelectedColor = ConsoleColor.Gray
            };

            OkButton.EnterClick += OkPressed;
            CancelButton.EnterClick += CancelPressed;
       
            container.components.Add(OkButton);
            container.components.Add(CancelButton);

            this.component = container;
            this.PrintBox();           
        }

        // Button methods

        private void OkPressed()
        {
            int directoryCount = 0;
            int fileCount = 0;

            string dir = "directory";
            string file = "file";

            foreach (string fileName in this.FileNames)
            {
                if (Directory.Exists(this.SourcePath + '\\' + fileName))
                {
                    try
                    {
                        Directory.Delete(this.SourcePath + '\\' + fileName, true);
                    }
                    catch
                    {
                        directoryCount++;
                    }
                }
                else if (File.Exists(this.SourcePath + '\\' + fileName))
                {
                    try
                    {
                        File.Delete(this.SourcePath + '\\' + fileName);
                    }
                    catch
                    {
                        fileCount++;
                    }
                }               
            }

            if(directoryCount > 1) { dir = "directories"; }
            if(fileCount > 1) { file = "files"; }

            if(directoryCount > 0 && fileCount > 0) { Functions.TextAlert($"{directoryCount} {dir} and {fileCount} {file} cannot be deleted!"); }
            else if(directoryCount > 0) { Functions.TextAlert($"{directoryCount} {dir} cannot be deleted!"); }
            else if(fileCount > 0) { Functions.TextAlert($"{fileCount} {file} cannot be deleted!"); }

            StaticPrinter.PrintTable();
            Application.RenewWindow(1);           
        }
        
        private void CancelPressed()
        {
            StaticPrinter.PrintTable();
            Application.RenewWindow(1);           
        }
    }
}
