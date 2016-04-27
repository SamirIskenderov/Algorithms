using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Library
{
    public enum Colores
    {
        None = 0,
        White = 1,
        Grey = 2,
        Black = 3,
    }

    public class GraphNode
    {
        public GraphNode Clone()
        {
            GraphNode node = new GraphNode(this.Number);
            node.Color = this.Color;

            foreach (var item in this.Connections)
            {
                node.AddConnection(item);
            }

            return node;
        }

        public GraphNode(ushort number) : this(number, new List<ushort>())
        {
        }

        public static bool operator !=(GraphNode lhsGraph, GraphNode rhsGraph)
            => !(lhsGraph == rhsGraph);

        public static bool operator ==(GraphNode lhsGraph, GraphNode rhsGraph)
        {
            if (((object)lhsGraph == null) || ((object)rhsGraph == null))
            {
                return false;
            }

            if (lhsGraph.Number != rhsGraph.Number)
            {
                return false;
            }

            if (lhsGraph.Connections.Count != rhsGraph.Connections.Count)
            {
                return false;
            }

            for (int i = 0; i < lhsGraph.Connections.Count; i++)
            {
                if (lhsGraph.Connections[i] != rhsGraph.Connections[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int result = 17;

            for (int i = 0; i < this.Connections.Count; i++)
            {
                result ^= i;
                foreach (var item in this.Connections)
                {
                    result ^= item;
                }
            }

            return result ^ this.Number;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            GraphNode node = obj as GraphNode;

            if (node == null)
            {
                return false;
            }

            if (node == this)
            {
                return true;
            }

            return this == node &&
                this.Color == node.Color;
        }

        public GraphNode(ushort number, List<ushort> connections)
        {
            this.Number = number;
            this.Connections = connections.ToList();
            this.Color = Colores.White;
        }

        public Colores Color { get; set; }

        public List<ushort> Connections { get; private set; }

        public ushort Number { get; private set; }

        /// <summary>
        /// One-way bind this node with node with index index.
        /// Return false if connection already exists.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool AddConnection(ushort index)
        {
            if (this.Connections.Contains(index))
            {
                return false;
            }

            this.Connections.Add(index);
            return true;
        }

        public GraphNode GetNew()
        {
            return new GraphNode((ushort)this.Connections.Count);
        }

        /// <summary>
        /// One-way unbind this node with node with index index.
        /// Return false, if there wasn't such connection.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool RemoveConnection(ushort index)
        {
            return this.Connections.Remove(index);
        }
    }
}