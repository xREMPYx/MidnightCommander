using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public abstract class Window
    {
        public List<IComponent> components = new();

        public virtual void Print()
        {
            foreach (IComponent component in components)
            {
                component.Print();
            }
        }

        public virtual void HandleKey(ConsoleKeyInfo info)
        {
            foreach (IComponent component in components)
            {
                component.HandleKey(info);
            }
        }
    }
}
