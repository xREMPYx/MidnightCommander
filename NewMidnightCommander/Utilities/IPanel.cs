using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public interface IPanel
    {
        string Path { get; set; }
        void Print();
        void HandleKey(ConsoleKeyInfo info);
    }
}
