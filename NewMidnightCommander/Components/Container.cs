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
        public static string LeftTablePath { get; set; }
        public static string RightTablePath { get; set; }

        public List<IPanel> components = new();

        public void HandleKey(ConsoleKeyInfo info)
        {
            if(info.Key == ConsoleKey.Tab)
            {
                this.Selected = (this.Selected + 1) % 2;
                IsLeftSelected = !IsLeftSelected;
            }
            else
            {
                this.components[this.Selected].HandleKey(info);
            }

            SetPath();
        }

        public void Print()
        {
            foreach (IPanel component in this.components)
            {
                component.Print();
            }
        }

        public void SetPath()
        {
            LeftTablePath = this.components[0].Path;
            RightTablePath = this.components[1].Path;
        }
    }
}
