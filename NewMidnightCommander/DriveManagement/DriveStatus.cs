using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public static class DriveStatus
    {        
        public static string[] InitialDrives()
        {            
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            string[] drives;

            if (allDrives.Length == 1) 
            {
                string[] d = { allDrives[0].ToString(), allDrives[0].ToString() };     
                drives = d;
            }
            else
            {
                string[] d = { allDrives[0].ToString(), allDrives[1].ToString() };
                drives = d;
            }

            return drives;
        }
    }
}
