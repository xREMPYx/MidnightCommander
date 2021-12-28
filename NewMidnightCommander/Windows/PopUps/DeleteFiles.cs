using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class DeleteFiles : PopUpWindow
    {
        private string SourcePath;

        private List<string> FileNames;

        public DeleteFiles(string path, List<string> fileNames)
        {
            this.Height = 5;
            this.Width = 50;
            this.Title = "Delete";
            this.AdditionalText = "Delete:";
            this.SecondAdditionalText = "*** Selected Files ***";
            this.ForeColor = ConsoleColor.Black;
            this.BackColor = ConsoleColor.Gray;
            this.FileNames = fileNames;
            this.SourcePath = path;
            this.ShowAdditional = true;

            Container container = new Container();

            Button OkButton = new Button((ProgramSettings.PanelWidth / 2) - (this.Width / 2) + 7, (ProgramSettings.PanelHeight / 2) + (this.Height / 2) - 1, "OK");
            Button CancelButton = new Button((ProgramSettings.PanelWidth / 2) - (this.Width / 2) + 30, (ProgramSettings.PanelHeight / 2) + (this.Height / 2) - 1, "Cancel");

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
            foreach (string fileName in this.FileNames)
            {
                if (Directory.Exists(this.SourcePath + '\\' + fileName))
                {
                    if (IsDirectoryEmpty()) 
                    {
                        Application.window = new DeleteFilesNotEmpty(this.SourcePath, this.FileNames);
                        return;
                    }
                    else
                    {
                        try
                        {
                            Directory.Delete(this.SourcePath + '\\' + fileName);
                        }
                        catch
                        {
                            Functions.TextAlert("Directory cannot be deleted!");
                        }
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
                        Functions.TextAlert("File cannot be deleted!");
                    }
                }
                else
                {
                    Functions.TextAlert("Already Exists!");
                }
            }

            StaticPrinter.PrintTable();
            Application.RenewWindow(1);           
        }
        
        private void CancelPressed()
        {
            StaticPrinter.PrintTable();
            Application.RenewWindow(1);           
        }

        // Others

        private bool IsDirectoryEmpty()
        {
            foreach (string fileName in this.FileNames)
            {
                if(Directory.Exists(this.SourcePath + '\\' + fileName))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(this.SourcePath + '\\' + fileName);
                    if (dirInfo.GetFiles().Length > 0) { return true; }
                    if (dirInfo.GetDirectories().Length > 0) { return true; }
                }                
            }
            return false;
        }
    }
}
