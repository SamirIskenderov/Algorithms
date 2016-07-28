using System;
using System.Collections.Generic;

namespace Algorithms.Library.Menu
{
    public class MenuNode : GraphNode, ICloneable
    {
        public string Text { get; set; }

        public MenuNode() : this(string.Empty, null, Color.White)
        {
            
        }

        public MenuNode(string text, IEnumerable<GraphNode> connections = null, Color color = Color.White) : base(connections, color)
        {
            this.Text = text;
        }

        public override string ToString()
        {
            return this.Text;
        }

        public new IList<MenuNode> Children { get; }
    }
}