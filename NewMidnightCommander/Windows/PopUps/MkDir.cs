using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class MkDir : PopUpWindow
    {
        private TextBox textBox;
        public MkDir(string path)
        {
            this.Height = 5;
            this.Width = 50;
            this.Title = "MkDir";
            this.ForeColor = ConsoleColor.Black;
            this.BackColor = ConsoleColor.Gray;
            this.ShowAdditional = false;

            Container container = new Container();

            TextBox textBox = new TextBox((ProgramSettings.PanelWidth / 2) - (this.Width / 2) + 1, (ProgramSettings.PanelHeight / 2) - 1, path, "Make a new directory:");

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
            Directory.CreateDirectory(this.textBox.Text);
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
