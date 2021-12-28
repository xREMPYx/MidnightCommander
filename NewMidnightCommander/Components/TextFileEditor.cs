using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class TextFileEditor : IComponent
    {
        private string Path;

        private int PositionX;
        private int PositionY;
        private int PositionXTop;
        private int PositionYTop;

        private List<string> TextList;

        public TextFileEditor(string path)
        {
            this.Path = path;
            this.TextList = new();         
            this.PositionX = 0;
            this.PositionY = 0;
            this.LoadText();
        }
   
        public void HandleKey(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.UpArrow) { MoveUp(); }
            else if (info.Key == ConsoleKey.DownArrow) { MoveDown(); }
            else if (info.Key == ConsoleKey.LeftArrow) { MoveLeft(); }
            else if (info.Key == ConsoleKey.RightArrow) { MoveRight(); }
            else if (info.Key == ConsoleKey.Backspace) { RemoveChar(); }
            else if (info.Key == ConsoleKey.Enter) { Enter(); }
            else { AddChar(info); }

            Console.CursorVisible = false;
        }

        public void Print(bool active)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ProgramSettings.TextEditorForeColor;
            Console.BackgroundColor = ProgramSettings.TextEditorBackColor;

            for (int y = 0 + this.PositionYTop; y < ProgramSettings.PanelHeight + this.PositionYTop - 1; y++)
            {
                if(y < this.TextList.Count)
                {
                    Functions.Write(0, y + 1 - this.PositionYTop, VisibleLine(this.TextList[y]).PadRight(ProgramSettings.PanelWidth));
                }
                else
                {
                    Functions.Write(0, y + 1 - this.PositionYTop, string.Empty.PadRight(ProgramSettings.PanelWidth));
                }              
            }
            Console.CursorVisible = true;
            Console.SetCursorPosition(this.PositionX - this.PositionXTop, this.PositionY + 1 - this.PositionYTop);
        }

        private string VisibleLine(string line)
        {
            if(line.Length < this.PositionXTop) { return string.Empty; }

            int subNumber = line.Length - this.PositionXTop;

            return line.Substring(this.PositionXTop, subNumber);
        }

        private void LoadText()
        {
            this.TextList = File.ReadAllLines(this.Path).ToList();

            foreach (string line in this.TextList)
            {
                if(line == string.Empty) { this.TextList.Remove(line); }
            }
        }

        // Move Actions

        private void MoveUp()
        {
            if(this.PositionY > 0) 
            { 
                this.PositionY--; 
            }
            if(this.PositionX > this.TextList[this.PositionY].Length)
            {
                this.PositionX = this.TextList[this.PositionY].Length;
            }
        }

        private void MoveDown()
        {
            if (this.PositionY < this.TextList.Count - 1) 
            {
                this.PositionY++; 
            }
            if (this.PositionX > this.TextList[this.PositionY].Length)
            {
                this.PositionX = this.TextList[this.PositionY].Length;
            }
        }

        private void MoveLeft()
        {
            if (this.PositionX > 0)
            {
                this.PositionX--; 
            }
            if (this.PositionX == this.PositionXTop - 1)
            {
                this.PositionXTop--; 
            }
        }

        private void MoveRight()
        {
            if (this.PositionX < this.TextList[this.PositionY].Length) 
            { 
                this.PositionX++;
            }
            if (this.PositionX == this.PositionXTop + ProgramSettings.PanelWidth)
            {
                this.PositionXTop++; 
            }
        }   

        // Text Edit Actions

        private void AddChar(ConsoleKeyInfo info)
        {
            if(info.Key == ConsoleKey.Backspace) { return; }

            this.TextList[this.PositionY] = this.TextList[this.PositionY].Insert(this.PositionX, info.KeyChar.ToString());

            this.MoveRight();
        }

        private void RemoveChar()
        {
            if (this.PositionX > 0)
            {
                if (this.TextList[this.PositionY].Length != 0)
                {
                    this.TextList[this.PositionY] = this.TextList[this.PositionY].Remove(this.PositionX - 1, 1);
                }                   

                if (this.PositionX > 0) { this.MoveLeft(); }
            }
            else if (this.PositionX == 0 && this.PositionY != 0)
            {
                if(this.TextList[this.PositionY].Length == 0)
                {
                    this.TextList.RemoveAt(this.PositionY);
                    this.PositionY--;
                    this.PositionX = this.TextList[this.PositionY].Length;
                }
            }
            else if (this.PositionX == 0 && this.PositionY == 0 && this.TextList.Count > 0 && this.TextList[this.PositionY].Length < 1)
            {
                this.TextList.RemoveAt(this.PositionY);
            }       
        }

        private void Enter()
        {
            if (this.PositionX == 0)
            {
                this.TextList.Insert(this.PositionY, string.Empty);
            }
            else if (this.PositionX == this.TextList[this.PositionY].Length)
            {
                this.TextList.Insert(this.PositionY + 1, string.Empty);
            }
            else
            {
                string firstPart = this.TextList[this.PositionY].Substring(0, this.PositionX);
                string secondPart = this.TextList[this.PositionY].Substring(this.PositionX, this.TextList[this.PositionY].Length - this.PositionX);

                this.TextList[this.PositionY] = firstPart;
                this.TextList.Insert(this.PositionY + 1, secondPart);               
            }

            this.PositionX = 0;
            this.PositionY++;
        }
    }
}
