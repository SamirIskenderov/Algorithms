using System;
using System.Collections.Generic;

namespace Algorithms.Library.Achivement
{
    public class AchiveText
    {
        public string Name { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
    }

    public class Achivement : GraphNode, ICloneable
    {
        public Achivement(): this(new AchiveText(), points: 0, isAvaliable: false, connections: null, color: Color.White)
        {
            
        }

        public Achivement(AchiveText achiveText, int points, bool isAvaliable = false, IEnumerable<GraphNode> connections = null, Color color = Color.White) : base(connections, color)
        {
            this.AchiveText = achiveText;
            this.Points = points;
            this.IsAvaliable = isAvaliable;
        }


        public AchiveText AchiveText { get; set; }

        public bool IsAvaliable { get; set; }

        public int Points { get; set; }
    }
}