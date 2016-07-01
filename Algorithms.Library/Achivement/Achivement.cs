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
        public Achivement(AchiveText achiveText, int points, bool isAvaliable = false, IEnumerable<GraphNode> connections = null, Color color = Color.White) : base(connections, color)
        {
            this.AchiveText = achiveText;
            this.Points = points;
            this.IsAvaliable = isAvaliable;
        }

        public AchiveText AchiveText { get; set; }

        public bool IsAvaliable { get; set; }

        public Action Invoke { get; set; }

        public int Points { get; set; }
    }
}