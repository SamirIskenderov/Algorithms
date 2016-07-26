using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Library
{
    public enum Color
    {
        Black = 0,
        Grey = 1,
        White = 2,
    }

    public class GraphNode : ICloneable
    {
        #region Public Constructors

        public GraphNode() : this(null, Color.White)
        {
            
        }

        public GraphNode(IEnumerable<GraphNode> connections = null, Color color = Color.White)
        {
            this.Color = color;

            if (connections == null)
            {
                connections = new List<GraphNode>();
            }

            this.Connections = connections.ToList();
        }

        #endregion Public Constructors

        #region Public Properties

        public Color Color { get; set; }
        public IList<GraphNode> Connections { get; private set; }

        #endregion Public Properties

        #region Clone

        public virtual object Clone()
        {
            return (object)this.CloneDirectly();
        }

        /// <summary>
        /// Overload of this.Clone() by return value
        /// </summary>
        /// <returns></returns>
        public virtual GraphNode CloneDirectly()
        {
            return new GraphNode(this.Connections, this.Color);
        }

        public virtual GraphNode DeepClone()
        {
            IList<GraphNode> connection = this.Connections.ToList();

            return new GraphNode(connection, this.Color);
        }

        #endregion Clone

        #region Public Methods

        /// <summary>
        /// This method add connection to this. In result nodes appears in Connections property of each other.
        /// </summary>
        /// <param name="node"></param>
        public void Connect(GraphNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }

            this.Connections.Add(node);
            node.Connections.Add(this);
        }

        public void Disconnect(GraphNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }

            this.Connections.Remove(node);
            node.Connections.Remove(this);
        }

        #endregion Public Methods
    }
}