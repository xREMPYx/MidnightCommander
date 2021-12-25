using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    internal interface IComponent
    {
        void Print();
        void HandleKey(ConsoleKeyInfo info);
    }
}
