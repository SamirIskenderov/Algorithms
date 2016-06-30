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

    public class GraphNode : ICloneable, IComparable<GraphNode>, IComparable
    {
        #region Public Constructors

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

        public object Clone()
        {
            return (object)this.CloneDirectly();
        }

        /// <summary>
        /// Overload of this.Clone() by return value
        /// </summary>
        /// <returns></returns>
        public GraphNode CloneDirectly()
        {
            return new GraphNode(this.Connections, this.Color);
        }

        public GraphNode DeepClone()
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

        #region Equals

        public static bool operator ==(GraphNode lhs, GraphNode rhs)
        {
            return lhs?.CompareTo(rhs) == 0;
        }

        public static bool operator !=(GraphNode lhs, GraphNode rhs)
        {
            return !(lhs == rhs);
        }

        public int CompareTo(GraphNode other)
        {
            return (object.ReferenceEquals(this, other) ? 0 : -1);
        }

        // due to shallow hash of this.Connections.GetHashCode(), hashes will always be different.
        public override bool Equals(object obj)
        {
            GraphNode node = obj as GraphNode;

            return node != null && this.Equals(node);
        }

        public bool Equals(GraphNode node)
        {
            return this.CompareTo(node) == 0;
        }

        public int CompareTo(object obj)
        {
            GraphNode node = obj as GraphNode;

            if (node == null)
            {
                return -1;
            }

            return this.CompareTo(node);
        }

        #endregion Equals

    }
}