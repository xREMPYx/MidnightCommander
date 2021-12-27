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

        public virtual void HandleKey(ConsoleKeyInfo info)
        {
            this.components[this.Selected].HandleKey(info);

            if (info.Key == ConsoleKey.Tab)
            {
                this.Selected = (this.Selected + 1) % this.components.Count;
            }
        }

        public virtual void Print(bool active)
        {
            int index = 0;
    
            foreach (IComponent component in this.components)
            {
                Console.CursorVisible = false;
                component.Print(index == this.Selected);
                index++;    
            }
        }
    }
}
