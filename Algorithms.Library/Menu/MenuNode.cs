using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Library.Menu
{
    public class MenuNode : GraphNode, ICloneable
    {
        public string Text { get; set; }

        public MenuNode(string text, int id, IEnumerable<GraphNode> connections = null, Color color = Color.White): base(id, connections, color)
        {
            this.Text = text;
        }
    }
}
