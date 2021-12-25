using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class DefaultWindow : Window
    {
        public DefaultWindow()
        {
            Panel leftPanel = new(true);
            Panel rightPanel = new(false);

            Container container = new();

            container.components.Add(leftPanel);
            container.components.Add(rightPanel);

            this.components.Add(container);

            StaticPrinter.PrintTable();
        }
    }
}
