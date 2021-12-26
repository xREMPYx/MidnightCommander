using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class Copy : PopUpWindow
    {
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }

        private TextBox textBox;
        public Copy(string sourcePath, string destinationPath)
        {
            this.Height = 8;
            this.Width = 50;
            this.Title = "Copy";
            this.AdditionalText = "Copy:";
            this.SecondAdditionalText = sourcePath;          
            this.ForeColor = ConsoleColor.Black;
            this.BackColor = ConsoleColor.Gray;
            this.SourcePath = sourcePath;
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

        private void OkPressed()
        {
            if (!Directory.Exists(this.textBox.Text) && !File.Exists(this.textBox.Text))
            {
                try { File.Copy(this.SourcePath, this.textBox.Text); }
                catch { this.CopyDirectory(this.SourcePath, this.textBox.Text);  }
            }
            StaticPrinter.PrintTable();
            Application.RenewWindow(1);           
        }
        
        private void CancelPressed()
        {
            StaticPrinter.PrintTable();
            Application.RenewWindow(1);           
        }

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
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}
