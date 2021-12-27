using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class TextBox : IComponent
    {
        public string Text { get; set; }

        private string Title;
        private int PositionX;
        private int PositionY;

        public TextBox(int x, int y, string inText, string title)
        {
            this.PositionX = x;
            this.PositionY = y;
            this.Text = inText;
            this.Title = title;
        }

        public void Print(bool active)
        {
            if (active)
            {             
                Console.ForegroundColor = ProgramSettings.SelectedBoxColor;
                Console.CursorVisible = true;
            }
            else
            {                
                Console.ForegroundColor = ProgramSettings.ForeBoxColor;
                Console.CursorVisible = false;
            }

            Console.BackgroundColor = ProgramSettings.BackBoxColor;

            Functions.Write(PositionX, PositionY, Title);

            Console.BackgroundColor = ProgramSettings.TitleBoxColor;
            Console.ForegroundColor = ProgramSettings.ForeBoxColor;

            string text = Text;
            if(Text.Length > 48) 
            {
                text = text.Substring(Text.Length - 48, 48); 
            }
            Functions.Write(PositionX, PositionY + 1, text.PadRight(49));

            if (active)
            { 
                Console.SetCursorPosition(PositionX + text.Length, PositionY + 1); 
            }
            else 
            {
                Functions.ReadKeyError(); 
            }
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.Backspace) { RemoveChar(); }
            else if (info.Key != ConsoleKey.Tab){ AddChar(info); }
        }

        // Text Actions

        private void AddChar(ConsoleKeyInfo info)
        {
            Text += info.KeyChar.ToString();
        }

        private void RemoveChar()
        {
            if (Text.Length > 0)
            {
                Text = Text.Remove(Text.Length - 1);
            }
        }
    }
}
