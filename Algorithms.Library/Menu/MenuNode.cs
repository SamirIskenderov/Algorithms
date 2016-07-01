using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Library.Menu
{
    public class MenuNode : GraphNode, ICloneable
    {
        public string Text { get; set; }

        public MenuNode(string text, IEnumerable<GraphNode> connections = null, Color color = Color.White): base(connections, color)
        {
            this.Text = text;
        }

        public override string ToString()
        {
            return this.Text;
        }

        public Action Invoke { get; set; }
    }
}
