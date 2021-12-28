using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class CopyFiles : PopUpWindow
    {          
        private string SourcePath;
        private string DestinationPath;

        private TextBox textBox;

        private List<string> FileNames;

        public CopyFiles(string path, List<string> fileNames, string destinationPath)
        {
            this.Height = 8;
            this.Width = 50;
            this.Title = "Copy";
            this.AdditionalText = "Copy:";
            this.SecondAdditionalText = "*** Selected Files ***";
            this.ForeColor = ConsoleColor.Black;
            this.BackColor = ConsoleColor.Gray;
            this.SourcePath = path;
            this.FileNames = fileNames;
            this.DestinationPath = destinationPath;
            this.ShowAdditional = true;

            Container container = new Container();

            TextBox textBox = new TextBox((ProgramSettings.PanelWidth / 2) - (this.Width / 2) + 1, (ProgramSettings.PanelHeight / 2) , destinationPath, "to:");

            Button OkButton = new Button((ProgramSettings.PanelWidth / 2) - (this.Width / 2) + 7, (ProgramSettings.PanelHeight / 2) + (this.Height / 2) - 1, "OK");
            Button CancelButton = new Button((ProgramSettings.PanelWidth / 2) - (this.Width / 2) + 30, (ProgramSettings.PanelHeight / 2) + (this.Height / 2) - 1, "Cancel");

            OkButton.EnterClick += OkPressed;
            CancelButton.EnterClick += CancelPressed;
       
            container.components.Add(OkButton);
            container.components.Add(CancelButton);
            container.components.Add(textBox);
            container.Selected = 2;

            this.component = container;
            this.textBox = textBox;
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
                        this.CopyDirectory(this.SourcePath + '\\' + fileName, this.textBox.Text + '\\' + fileName);
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
                        File.Copy(this.SourcePath + '\\' + fileName, this.textBox.Text + '\\' + fileName);
                    }
                    catch
                    {
                        fileCount++;
                    }
                }
                else
                {
                    Functions.TextAlert("Already Exists!");
                }
            }

            if (directoryCount > 1) { dir = "directories"; }
            if (fileCount > 1) { file = "files"; }

            if (directoryCount > 0 && fileCount > 0) { Functions.TextAlert($"{directoryCount} {dir} and {fileCount} {file} could not be coppied!"); }
            else if (directoryCount > 0) { Functions.TextAlert($"{directoryCount} {dir} could not be coppied!"); }
            else if (fileCount > 0) { Functions.TextAlert($"{fileCount} {file} could not be coppied!"); }

            StaticPrinter.PrintTable();
            Application.RenewWindow();
        }
        
        private void CancelPressed()
        {
            StaticPrinter.PrintTable();
            Application.RenewWindow();
        }

        // Copy All function

        private void CopyDirectory(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        private void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}
