using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class RenMov : PopUpWindow
    {
        public string SourcePath { get; set; }

        private TextBox textBox;

        public RenMov(string sourcePath)
        {
            this.Height = 8;
            this.Width = 50;
            this.Title = "RenMov";
            this.AdditionalText = "Rename or move:";
            this.SecondAdditionalText = sourcePath;          
            this.ForeColor = ConsoleColor.Black;
            this.BackColor = ConsoleColor.Gray;
            this.SourcePath = sourcePath;
            this.ShowAdditional = true;

            Container container = new Container();

            TextBox textBox = new TextBox((ProgramSettings.PanelWidth / 2) - (this.Width / 2) + 1, (ProgramSettings.PanelHeight / 2) , sourcePath, "to:");

            Button OkButton = new Button((ProgramSettings.PanelWidth / 2) - (this.Width / 2) + 7, (ProgramSettings.PanelHeight / 2) + (this.Height / 2) - 1, "OK");
            Button CancelButton = new Button((ProgramSettings.PanelWidth / 2) - (this.Width / 2) + 30, (ProgramSettings.PanelHeight / 2) + (this.Height / 2) - 1, "Cancel");

            OkButton.EnterClick += OkPressed;
            CancelButton.EnterClick += CancelPressed;
       
            container.components.Add(OkButton);
            container.components.Add(CancelButton);
            container.components.Add(textBox);
            container.Selected = 2;

            this.Component = container;
            this.textBox = textBox;
            this.PrintBox();           
        }

        // Button methods

        private void OkPressed()
        {
            if (!Directory.Exists(this.textBox.Text) && !File.Exists(this.textBox.Text))
            {
                try { File.Move(this.SourcePath, this.textBox.Text); }
                catch { try { Directory.Move(this.SourcePath, this.textBox.Text); } catch { Functions.TextAlert("Error!"); }  }
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
    }
}
