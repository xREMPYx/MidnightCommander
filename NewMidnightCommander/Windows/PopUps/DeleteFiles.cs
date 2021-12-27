using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class DeleteFiles : PopUpWindow
    {
        private string Path;
        private List<string> SourcePaths;

        public DeleteFiles(string path, List<string> sourcePaths)
        {
            this.Height = 5;
            this.Width = 50;
            this.Title = "Delete";
            this.AdditionalText = "Delete:";
            this.SecondAdditionalText = "*** Selected Files ***";
            this.ForeColor = ConsoleColor.Black;
            this.BackColor = ConsoleColor.Gray;
            this.SourcePaths = sourcePaths;
            this.Path = path;
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
            try
            {
                foreach (string path in this.SourcePaths)
                {
                    File.Delete(this.Path + '\\' + path);
                }               
            }
            catch 
            {
                try
                {
                    foreach (string path in this.SourcePaths)
                    {
                        Directory.Delete(this.Path + '\\' + path);
                    }
                }
                catch 
                {
                    Application.RenewWindow();
                    Application.window = new DeleteFilesNotEmpty(this.Path, this.SourcePaths);
                    return;
                }               
            }
            StaticPrinter.PrintTable();
            Application.RenewWindow();           
        }
        
        private void CancelPressed()
        {
            StaticPrinter.PrintTable();
            Application.RenewWindow();           
        }
    }
}
