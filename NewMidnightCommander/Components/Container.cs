using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class Container : IComponent 
    {
        public int Selected { get; set; } = 0;
        
        public List<IComponent> components = new();

        public void HandleKey(ConsoleKeyInfo info)
        {
            this.components[this.Selected].HandleKey(info);

            if (info.Key == ConsoleKey.Tab)
            {
                this.Selected = (this.Selected + 1) % this.components.Count;
            }
        }

        public void Print(bool active)
        {
            int index = 0;
            foreach (IComponent component in this.components)
            {
                component.Print(index == this.Selected);
                index++;
            }
        }
    }
}
