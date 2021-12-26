using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public interface IComponent
    {
        public void Print(bool active);
        public void HandleKey(ConsoleKeyInfo info);
    }
}
