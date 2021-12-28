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
                Console.ForegroundColor = ProgramSettings.BoxSelectedColor;
                Console.CursorVisible = true;
            }
            else
            {                
                Console.ForegroundColor = ProgramSettings.BoxForeColor;
                Console.CursorVisible = false;
            }

            Console.BackgroundColor = ProgramSettings.BoxBackColor;

            Functions.Write(this.PositionX, this.PositionY, this.Title);

            Console.BackgroundColor = ProgramSettings.BoxTitleColor;
            Console.ForegroundColor = ProgramSettings.BoxForeColor;

            string text = Text;
            if(this.Text.Length > 48) 
            {
                text = text.Substring(this.Text.Length - 48, 48); 
            }
            Functions.Write(this.PositionX, this.PositionY + 1, text.PadRight(49));

            if (active)
            { 
                Console.SetCursorPosition(this.PositionX + text.Length, this.PositionY + 1); 
            }
            else 
            {
                Functions.ReadKeyError(); 
            }
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.Backspace) { RemoveChar(); }
            else if (info.Key != ConsoleKey.Tab && info.Key != ConsoleKey.Enter) { AddChar(info); }
        }

        // Text Actions

        private void AddChar(ConsoleKeyInfo info)
        {
            this.Text += info.KeyChar.ToString();
        }

        private void RemoveChar()
        {
            if (this.Text.Length > 0)
            {
                this.Text = this.Text.Remove(this.Text.Length - 1);
            }
        }
    }
}
