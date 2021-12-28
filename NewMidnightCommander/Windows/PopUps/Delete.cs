using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class Delete : PopUpWindow
    {
        public string SourceItemPath { get; set; }

        public Delete(string sourceItemPath)
        {
            this.Height = 5;
            this.Width = 50;
            this.Title = "Delete";
            this.AdditionalText = "Delete:";
            this.SecondAdditionalText = sourceItemPath;
            this.ForeColor = ConsoleColor.Black;
            this.BackColor = ConsoleColor.Gray;
            this.SourceItemPath = sourceItemPath;
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
            if (Directory.Exists(this.SourceItemPath))
            {
                if (IsDirectoryEmpty())
                {
                    Application.window = new DeleteNotEmpty(this.SourceItemPath);
                    return;
                }
                else
                {
                    try
                    {
                        Directory.Delete(this.SourceItemPath);
                    }
                    catch
                    {
                        Functions.TextAlert("Directory cannot be deleted!");
                    }
                }
            }

            else if (File.Exists(this.SourceItemPath))
            {
                try
                {
                    File.Delete(this.SourceItemPath);
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
            if (Directory.Exists(this.SourceItemPath))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(this.SourceItemPath);
                if (dirInfo.GetFiles().Length > 0) { return true; }
                if (dirInfo.GetDirectories().Length > 0) { return true; }
            }
            return false;
        }
    }
}
