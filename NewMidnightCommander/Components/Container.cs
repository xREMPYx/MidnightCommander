using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class Container : IComponent
    {
        private int Selected { get; set; } = 0;
        public static bool IsLeftSelected { get; set; } = true;

        public List<IComponent> components = new();

        public void HandleKey(ConsoleKeyInfo info)
        {
            if(info.Key == ConsoleKey.Tab)
            {
                Selected = (Selected + 1) % 2;
                IsLeftSelected = !IsLeftSelected;
            }
            else
            {
                components[Selected].HandleKey(info);
            }
        }

        public void Print()
        {
            foreach (IComponent component in components)
            {
                component.Print();
            }
        }
    }
}
