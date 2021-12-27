using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class Delete : PopUpWindow
    {
        public string SourcePath { get; set; }

        public Delete(string sourcePath)
        {
            this.Height = 5;
            this.Width = 50;
            this.Title = "Delete";
            this.AdditionalText = "Delete:";
            this.SecondAdditionalText = sourcePath;
            this.ForeColor = ConsoleColor.Black;
            this.BackColor = ConsoleColor.Gray;
            this.SourcePath = sourcePath;
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
                File.Delete(this.SourcePath);
            }
            catch 
            {
                try
                {
                    Directory.Delete(this.SourcePath);
                }
                catch 
                {
                    Application.RenewWindow();
                    Application.window = new DeleteNotEmpty(this.SourcePath);
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
