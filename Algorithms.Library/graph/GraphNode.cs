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

            this.Children = connections.ToList();
        }

        #endregion Public Constructors

        #region Public Properties

        public Color Color { get; set; }

        public IList<GraphNode> Children { get; }

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
            return new GraphNode(this.Children, this.Color);
        }

        public virtual GraphNode DeepClone()
        {
            IList<GraphNode> connection = this.Children.ToList();

            return new GraphNode(connection, this.Color);
        }

        #endregion Clone

        #region Public Methods

        /// <summary>
        /// This method add connection to this. In result nodes appears in Connections property of each other.
        /// </summary>
        /// <param name="node"></param>
        internal void Connect(GraphNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }

            this.Children.Add(node);
            node.Children.Add(this);
        }

        internal void Disconnect(GraphNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }

            this.Children.Remove(node);
            node.Children.Remove(this);
        }

        #endregion Public Methods
    }
}