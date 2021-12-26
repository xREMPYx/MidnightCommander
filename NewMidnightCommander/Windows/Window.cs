using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public abstract class Window
    {
        public IComponent component { get; set; }

        public virtual void Print()
        {
            component.Print(true);
        }

        public virtual void HandleKey(ConsoleKeyInfo info)
        {
            component.HandleKey(info);
        }
    }
}
