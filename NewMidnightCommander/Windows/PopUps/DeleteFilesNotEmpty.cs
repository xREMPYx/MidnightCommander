﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class DeleteFilesNotEmpty : PopUpWindow
    {
        private string Path;

        private List<string> SourcePaths;

        public DeleteFilesNotEmpty(string path, List<string> sourcePaths)
        {
            this.Height = 5;
            this.Width = 50;
            this.Title = "Delete Alert";
            this.AdditionalText = "Directories are not empty!".PadLeft(this.Width/2 + 8);
            this.SecondAdditionalText = "Are you sure you want to delete it?".PadLeft(this.Width - 8);
            this.ForeColor = ConsoleColor.Black;
            this.BackColor = ConsoleColor.DarkRed;
            this.TitleColor = ConsoleColor.Black;
            this.ItemBackColor = ConsoleColor.DarkRed;
            this.SourcePaths = sourcePaths;
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

            this.component = container;
            this.PrintBox();           
        }

        // Button methods

        private void OkPressed()
        {
            try 
            {
                foreach (var path in this.SourcePaths)
                {
                    File.Delete(this.Path + '\\' + path);
                }                
            }
            catch
            {
                try
                {
                    foreach (var path in this.SourcePaths)
                    {
                        Directory.Delete(this.Path + '\\' + path, true);
                    }                   
                }
                catch
                {
                    Functions.TextAlert("Error!");
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