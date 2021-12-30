using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMidnightCommander
{
    public class TextFileEditorWindow : Window
    {
        public TextFileEditorWindow(string path)
        {
            TextFileEditor textFileEditor = new(path);

            this.Component = textFileEditor;
        }
    }
}
