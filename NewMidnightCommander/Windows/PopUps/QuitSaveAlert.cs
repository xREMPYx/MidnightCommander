using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class QuitSaveAlert : PopUpWindow
    {        
        private string Path;

        private List<string> TextList;
        public QuitSaveAlert(string path, List<string> textList)
        {
            this.Height = 5;
            this.Width = 50;
            this.Title = "Save Alert";
            this.AdditionalText = "Your text is not saved!".PadLeft(this.Width/2 + 10);
            this.SecondAdditionalText = "Do you want to save it?".PadLeft(this.Width/2 + 10);
            this.ForeColor = ConsoleColor.Black;
            this.BackColor = ConsoleColor.DarkRed;
            this.TitleColor = ConsoleColor.Black;
            this.ItemBackColor = ConsoleColor.DarkRed;
            this.TextList = textList;
            this.Path = path;
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

            this.Component = container;
            this.PrintBox();           
        }

        // Button methods

        private void OkPressed()
        {
            using (StreamWriter streamWriter = new StreamWriter(this.Path))
            {
                foreach (string line in this.TextList)
                {
                    streamWriter.WriteLine(line);
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
    }
}
