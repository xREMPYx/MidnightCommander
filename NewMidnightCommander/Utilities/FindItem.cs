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

        public StringBuilder sb = new();

        public FindItem(List<string[]> Files)
        {
            List<string> itemNames = new();
            for (int i = 0; i < Files.Count; i++)
            {
                itemNames.Add(Files[i][0].ToString().Trim(' ').Trim('\\').ToLower());
            }
            this.Matches = itemNames;
            this.ItemNames = itemNames;
        }

        public void Find(ConsoleKeyInfo info)
        {
            bool shouldBeAdded = false;
            List<string> matches = new();
            for (int i = 0; i < Matches.Count; i++)
            {
                if (index > Matches[i].Length - 1) { }
                else { if (Matches[i][index].ToString() == info.KeyChar.ToString()) { matches.Add(Matches[i]); shouldBeAdded = true; } }                
            }
            if (shouldBeAdded) { sb.Append(info.KeyChar.ToString()); index++; this.Matches = matches; }         
        }

        public int selectedForFileManager(int selected)
        {
            if(ItemNames.Count == Matches.Count) { return selected; }

            for (int i = 0; i < ItemNames.Count; i++)
            {
                if(sb.Length <= ItemNames[i].Length)
                {
                    int count = 0;
                    for (int d = 0; d < sb.Length; d++)
                    {
                        if (ItemNames[i][d].ToString() == sb[d].ToString()) { count++; }
                    }
                    if(count >= sb.Length) { return i; }
                }             
            }
            return selected;
        }
    }
}
