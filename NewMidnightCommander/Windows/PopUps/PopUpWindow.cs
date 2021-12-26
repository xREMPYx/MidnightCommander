using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public abstract class PopUpWindow : Window
    {
        public string Title { get; set; } 
        public string AdditionalText { get; set; }
        public string SecondAdditionalText { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool ShowAdditional { get; set; }
        public ConsoleColor ForeColor { get; set; } = ProgramSettings.ForeBoxColor;
        public ConsoleColor BackColor { get; set; } = ProgramSettings.BackBoxColor;
        public ConsoleColor TitleColor { get; set; } = ProgramSettings.TitleBoxColor;
        public ConsoleColor ItemBackColor { get; set; } = ProgramSettings.TitleBoxColor;

        public void PrintBox()
        {
            Console.ForegroundColor = this.ForeColor;
            Console.BackgroundColor = this.BackColor;

            for (int y = (ProgramSettings.PanelHeight / 2) - (this.Height / 2); y < (ProgramSettings.PanelHeight / 2) - (this.Height / 2) + this.Height; y++)
            {
                Functions.Write((ProgramSettings.PanelWidth / 2) - (this.Width / 2), y, " ".PadRight(this.Width));
            }

            for (int y = (ProgramSettings.PanelHeight / 2) - (this.Height / 2) + 1; y < (ProgramSettings.PanelHeight / 2) - (this.Height / 2) + this.Height; y++)
            {
                Functions.Write((ProgramSettings.PanelWidth / 2) - (this.Width / 2), y, "│");
                Functions.Write((ProgramSettings.PanelWidth / 2) + (this.Width / 2), y, "│");
            }

            for (int x = (ProgramSettings.PanelWidth / 2) - (this.Width / 2) + 1; x < (ProgramSettings.PanelWidth / 2) - (this.Width / 2) + this.Width; x++)
            {
                Functions.Write(x, (ProgramSettings.PanelHeight / 2) - (this.Height / 2), "─");
                Functions.Write(x, (ProgramSettings.PanelHeight / 2) + (this.Height / 2), "─");
            }

            Functions.Write((ProgramSettings.PanelWidth / 2) - (this.Width / 2), (ProgramSettings.PanelHeight / 2) - (this.Height / 2), "┌");
            Functions.Write((ProgramSettings.PanelWidth / 2) + (this.Width / 2), (ProgramSettings.PanelHeight / 2) - (this.Height / 2), "┐");
            Functions.Write((ProgramSettings.PanelWidth / 2) - (this.Width / 2), (ProgramSettings.PanelHeight / 2) + (this.Height / 2), "└");
            Functions.Write((ProgramSettings.PanelWidth / 2) + (this.Width / 2), (ProgramSettings.PanelHeight / 2) + (this.Height / 2), "┘");

            Console.ForegroundColor = this.TitleColor;
            Functions.Write((ProgramSettings.PanelWidth / 2) - (this.Title.Length / 2), (ProgramSettings.PanelHeight / 2) - (this.Height / 2), this.Title);

            if (this.ShowAdditional)
            {
                Console.ForegroundColor = this.ForeColor;

                Functions.Write((ProgramSettings.PanelWidth / 2) - (this.Width / 2) + 1, (ProgramSettings.PanelHeight / 2) - (this.Height / 2) + 1, this.AdditionalText);
                Console.BackgroundColor = this.ItemBackColor;                

                if(this.SecondAdditionalText.Length > 49) 
                {
                    this.SecondAdditionalText = this.SecondAdditionalText.Substring(this.SecondAdditionalText.Length - 49, 49);
                }

                Functions.Write((ProgramSettings.PanelWidth / 2) - (this.Width / 2) + 1, (ProgramSettings.PanelHeight / 2) - (this.Height / 2) + 2, this.SecondAdditionalText.PadRight(49));
            }                  
        }
    }
}
