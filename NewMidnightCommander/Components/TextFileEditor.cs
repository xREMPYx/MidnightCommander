﻿using System;
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
        private List<string> InitialTextList;

        public TextFileEditor(string path)
        {
            this.Path = path;
            this.TextList = new();         
            this.PositionX = 0;
            this.PositionY = 0;
            this.TextList = this.InitializeTextList();
            this.InitialTextList = this.InitializeTextList();
        }
   
        public void HandleKey(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.UpArrow) { MoveUp(); }
            else if (info.Key == ConsoleKey.DownArrow) { MoveDown(); }
            else if (info.Key == ConsoleKey.LeftArrow) { MoveLeft(); }
            else if (info.Key == ConsoleKey.RightArrow) { MoveRight(); }
            else if (info.Key == ConsoleKey.Home) { MoveToRowStart(); }
            else if (info.Key == ConsoleKey.End) { MoveToRowEnd(); }
            else if (info.Key == ConsoleKey.PageUp) { MoveToTopRow(); }
            else if (info.Key == ConsoleKey.PageDown) { MoveToLastRow(); }        
            else if (info.Key == ConsoleKey.Backspace) { RemoveChar(); }
            else if (info.Key == ConsoleKey.Enter) { Enter(); }
            else if (info.Key == ConsoleKey.F2) { Save(); }
            else if (info.Key == ConsoleKey.F10) { Quit(); }
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

        private List<string> InitializeTextList()
        {
            List<string> textList = File.ReadAllLines(this.Path).ToList();

            foreach (string line in textList)
            {
                if(line == string.Empty) { this.TextList.Remove(line); }
            }

            textList.Add(string.Empty);

            return textList;
        }

        // Move Actions

        private void MoveUp()
        {
            if(this.PositionY > 0) 
            { 
                this.PositionY--; 
            }
            if(this.PositionY == this.PositionYTop - 1)
            {
                this.PositionYTop--;
            }
            if(this.PositionX > this.TextList[this.PositionY].Length - 1)
            {
                this.MoveToRowEnd();
            }
        }

        private void MoveDown()
        {
            if(this.PositionY < this.TextList.Count - 1) 
            {
                this.PositionY++; 
            }
            if(this.PositionY > this.PositionYTop + ProgramSettings.PanelHeight - 2)
            {
                this.PositionYTop++;
            }
            if(this.PositionX > this.TextList[this.PositionY].Length)
            {
                this.MoveToRowEnd();
            }
        }

        private void MoveLeft()
        {
            if(this.PositionX > 0)
            {
                this.PositionX--; 
            }
            if(this.PositionX == this.PositionXTop - 1)
            {
                this.PositionXTop--; 
            }
        }

        private void MoveRight()
        {
            if(this.PositionX < this.TextList[this.PositionY].Length) 
            { 
                this.PositionX++;
            }
            if(this.PositionX == this.PositionXTop + ProgramSettings.PanelWidth)
            {
                this.PositionXTop++; 
            }
        }   

        private void MoveToRowStart()
        {
            this.PositionX = 0;
            this.PositionXTop = 0;
        }

        private void MoveToRowEnd()
        {
            this.PositionX = this.TextList[this.PositionY].Length;
            this.PositionXTop = this.TextList[this.PositionY].Length + 1 - ProgramSettings.PanelWidth;
            if(this.PositionXTop < 0) { this.PositionXTop = 0; }
        }

        private void MoveToTopRow()
        {
            this.PositionY = 0;
            this.PositionYTop = 0;
            if(this.PositionX > this.TextList[this.PositionY].Length) { this.MoveToRowEnd(); }
        }
        
        private void MoveToLastRow()
        {
            this.PositionY = this.TextList.Count - 1;
            this.PositionYTop = this.TextList.Count - ProgramSettings.PanelHeight + 1;
            if(this.PositionYTop < 0) { this.PositionYTop = 0; }
            if (this.PositionX > this.TextList[this.PositionY].Length) { this.MoveToRowEnd(); }
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
                    this.MoveUp();
                    this.MoveToRowEnd();
                }
            }
            else if (this.PositionX == 0 && this.PositionY == 0 && this.TextList.Count > 1 && this.TextList[this.PositionY].Length < 1)
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
            this.PositionXTop = 0;
            this.MoveDown();
        }

        private void Save()
        {
            using (StreamWriter streamWriter = new StreamWriter(this.Path))
            {
                foreach (string line in this.TextList)
                {
                    streamWriter.WriteLine(line);
                }
            }
            this.InitialTextList = this.TextList;
        }

        private void Quit()
        {
            if(!Enumerable.SequenceEqual(this.TextList, this.InitialTextList))
            {
                Application.window = new QuitSaveAlert(this.Path, this.TextList);
            }
            else
            {
                StaticPrinter.PrintTable();
                Application.RenewWindow(1);
            }
        }
    }
}
