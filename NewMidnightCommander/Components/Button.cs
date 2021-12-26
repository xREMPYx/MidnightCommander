using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class Button : IComponent
    {
        public event Action EnterClick;
        public string Label { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public ConsoleColor ForeColor { get; set; } = ProgramSettings.ForeBoxColor;
        public ConsoleColor BackColor { get; set; } = ProgramSettings.BackBoxColor;
        public ConsoleColor SelectedColor { get; set; } = ProgramSettings.SelectedBoxColor;

        public Button(int x, int y, string label)
        {
            this.PositionX = x;
            this.PositionY = y;
            this.Label = label;
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            switch (info.Key)
            {
                case ConsoleKey.Enter: EnterClick(); break;
            }
        }

        public void Print(bool active)
        {
            string label = Label;

            if (active) 
            { 
                label = "[ < " + Label + " > ]";
                Console.ForegroundColor = this.SelectedColor;
            }
            else
            {
                Console.ForegroundColor = this.ForeColor;
                label = "[   " + Label + "   ]";
            }

            Console.BackgroundColor = this.BackColor;
            Functions.Write(PositionX,PositionY,label);
            Console.BackgroundColor = this.BackColor;
            Console.ForegroundColor = this.ForeColor;
        }
    }
}
