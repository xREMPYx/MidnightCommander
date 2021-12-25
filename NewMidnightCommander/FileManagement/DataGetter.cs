using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public static class DataGetter
    {
        public static List<string[]> Files(string path)
        {
            List<string[]> files = new List<string[]>();

            DirectoryInfo dir = new DirectoryInfo(path);

            foreach (DirectoryInfo directory in dir.GetDirectories())
            {
                string[] directoryRow = {'\\' + directory.Name.ToString() , string.Empty, directory.LastWriteTimeUtc.ToShortDateString() };
                files.Add(directoryRow);
            }
            foreach (FileInfo file in dir.GetFiles())
            {
                string[] fileRow = { ' ' + file.Name, ItemSize(file.Length), file.LastWriteTimeUtc.ToShortDateString() };
                files.Add(fileRow);
            }

            return files;
        }

        public static string ItemSize(long itemSize)
        {
            if (itemSize > 1073741824) //Giga Bytes
            {
                Decimal FileSize = Decimal.Divide(itemSize, 1073741824);
                return String.Format("{0:##.##} GB", FileSize);
            }
            else if (itemSize > 1048576) //Mega Bytes
            {
                Decimal FileSize = Decimal.Divide(itemSize, 1048576);
                return String.Format("{0:##.##} MB", FileSize);
            }
            else if (itemSize > 1024) //Kilo Bytes
            {
                Decimal FileSize = Decimal.Divide(itemSize, 1024);
                return String.Format("{0:##.##} KB", FileSize);
            }
            else if (itemSize > 0)           
            {
                Decimal FileSize = itemSize;
                return String.Format("{0:##.##} Bytes", FileSize);
            }
            return "0 Bytes".ToString();
        }
    }
}
