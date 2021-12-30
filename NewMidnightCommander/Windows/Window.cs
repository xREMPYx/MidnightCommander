using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public abstract class Window
    {
        public IComponent Component { get; set; }

        public virtual void Print()
        {
            this.Component.Print(true);
        }

        public virtual void HandleKey(ConsoleKeyInfo info)
        {
            this.Component.HandleKey(info);
        }
    }
}
