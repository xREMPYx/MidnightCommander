using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    internal class MarkFiles
    {
        public List<string> Files { get; set; }
        public List<string> MarkedFileNames { get; set; }
        public int startFileSelected { get; set; }
        public int lastFileSelected { get; set; }

        public MarkFiles(List<string[]> Files)
        {
            List<string> itemNames = new();
            for (int i = 0; i < Files.Count; i++)
            {
                itemNames.Add(Files[i][0].ToString().Trim(' ').Trim('\\'));
            }
            this.Files = itemNames;
        }

        public void AddFiles()
        {
            int startFileSel;
            int lastFileSel;

            if(this.lastFileSelected >= this.startFileSelected) 
            {
                startFileSel = this.startFileSelected;
                lastFileSel = this.lastFileSelected;
            }
            else
            {
                startFileSel = this.lastFileSelected;
                lastFileSel = this.startFileSelected;
            }
            this.MarkedFileNames = new();
            for (int i = startFileSel; i < lastFileSel + 1; i++)
            {
                this.MarkedFileNames.Add(this.Files[i]);
            }
        }
    }
}
