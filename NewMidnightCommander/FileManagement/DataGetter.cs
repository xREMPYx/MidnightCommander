using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    internal static class DataGetter
    {
        public static List<string> Files(string path)
        {
            List<string> files = new List<string>();

            DirectoryInfo dir = new DirectoryInfo(path);

            foreach (DirectoryInfo directory in dir.GetDirectories())
            {
                files.Add($@"\{directory.Name}");
            }
            foreach (FileInfo file in dir.GetFiles())
            {
                files.Add($" {file.Name}");
            }

            return files;
        }
    }
}
