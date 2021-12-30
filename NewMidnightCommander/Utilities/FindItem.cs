using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class FindItem
    {
        private int index = 0;
        public List<string> Matches { get; set; }
        public List<string> ItemNames { get; set; }
        public StringBuilder sb { get; set; }

        public FindItem(List<string[]> Files)
        {
            List<string> itemNames = new();
            for (int i = 0; i < Files.Count; i++)
            {
                itemNames.Add(Files[i][0].ToString().Trim(' ').Trim('\\').ToLower());
            }
            this.Matches = itemNames;
            this.ItemNames = itemNames;
            this.sb = new();
        }

        public void Find(ConsoleKeyInfo info)
        {
            bool shouldBeAdded = false;
            List<string> matches = new();
            for (int i = 0; i < this.Matches.Count; i++)
            {
                if (this.index > this.Matches[i].Length - 1) { }
                else { if (this.Matches[i][this.index].ToString() == info.KeyChar.ToString()) { matches.Add(this.Matches[i]); shouldBeAdded = true; } }                
            }
            if (shouldBeAdded) { this.sb.Append(info.KeyChar.ToString()); this.index++; this.Matches = matches; }         
        }

        public int SelectedForFileManager(int selected)
        {
            if(this.ItemNames.Count == this.Matches.Count) { return selected; }

            for (int i = 0; i < this.ItemNames.Count; i++)
            {
                if(this.sb.Length <= this.ItemNames[i].Length)
                {
                    int count = 0;
                    for (int d = 0; d < this.sb.Length; d++)
                    {
                        if (this.ItemNames[i][d].ToString() == this.sb[d].ToString()) { count++; }
                    }
                    if(count >= this.sb.Length) { return i; }
                }             
            }
            return selected;
        }
    }
}
