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
        public List<string> MarkedFiles { get; set; }
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

            if(lastFileSelected >= startFileSelected) 
            {
                startFileSel = startFileSelected;
                lastFileSel = lastFileSelected;
            }
            else
            {
                startFileSel = lastFileSelected;
                lastFileSel = startFileSelected;
            }
            MarkedFiles = new();
            for (int i = startFileSel; i < lastFileSel + 1; i++)
            {
                MarkedFiles.Add(Files[i]);
            }
        }
    }
}
