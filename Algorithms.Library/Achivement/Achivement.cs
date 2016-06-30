using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Library.Achivement
{
    public class Achivement : GraphNode, ICloneable
    {
        public Achivement() : base()
        {
            
        }

        public Achivement(IEnumerable<GraphNode> connections = null, Color color = Color.White) : base(connections, color)
        {
        }

        public string HeaderText { get; set; }

        public string Description { get; set; }

        public bool IsAvaliable { get; set; }

        public int Points { get; set; }
    }
}
