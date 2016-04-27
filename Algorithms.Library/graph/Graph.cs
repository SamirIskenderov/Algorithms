using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Library
{
    public class Graph
    {
        public Graph() : this(new List<GraphNode>())
        {
        }

        public Graph(List<GraphNode> nodes)
        {
            this.Nodes = nodes.ToList();

            //if ((this.EnsureValidity) && (!this.CheckValidity()))
            //{
            //	throw new ArgumentException("Graph is not valid.");
            //}
        }

        /// <summary>
        /// Can there be any cycles.
        /// Is there's - throws exception.
        /// True by default.
        /// </summary>
        public bool CanBeCyclic { get; set; } = true;

        /// <summary>
        /// Can there be any loops.
        /// Is there's - throws exception.
        /// True by default.
        /// </summary>
        public bool CanBeLooped { get; set; } = true;

        /// <summary>
        /// Can there be non connected points
        /// True by default.
        /// </summary>
        public bool CanBeNonConnectivity { get; set; } = true;

        /// <summary>
        /// If true, on every graph update will invoke this.CheckValidity().
        /// </summary>
        public bool EnsureValidity { get; set; } = true;

        public List<GraphNode> Nodes { get; }

        /// <summary>
        /// Determinated, can graph has non-constant value of vertices.
        /// If 0 - each node of graph can have as many vertices, as it want.
        /// If non 0 - every node must have this number of verticies. If it will not happent to be - throws exception.
        /// </summary>
        public byte Vertices { get; set; } = 0;

        public static bool operator !=(Graph lhsGraph, Graph rhsGraph)
                    => !(lhsGraph == rhsGraph);

        public static bool operator ==(Graph lhsGraph, Graph rhsGraph)
        {
            if (((object)lhsGraph == null) || ((object)rhsGraph == null))
            {
                return false;
            }

            if (lhsGraph.Nodes.Count != rhsGraph.Nodes.Count)
            {
                return false;
            }

            for (int i = 0; i < lhsGraph.Nodes.Count - 1; i++)
            {
                if (lhsGraph.Nodes[i] != rhsGraph.Nodes[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Create empty unbinded node in the end of a graph
        /// </summary>
        /// <returns></returns>
        public bool AddNode()
        {
            this.Nodes.Add(new GraphNode((ushort)this.Nodes.Count));

            return true;
        }

        /// <summary>
        /// Create count empty unbinded node in the end of a graph
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool AddNode(ushort count)
        {
            for (int i = 0; i < count; i++)
            {
                this.Nodes.Add(new GraphNode((ushort)this.Nodes.Count));
            }

            return true;
        }

        /// <summary>
        /// Create empty unbinded node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool AddNode(GraphNode node)
        {
            if (this.Nodes.Contains(node))
            {
                return false;
            }
            this.Nodes.Add(node);

            if ((this.EnsureValidity) && (!this.CheckValidity()))
            {
                this.Nodes.Remove(node);
                throw new ArgumentException("Graph is not valid.");
            }

            return true;
        }

        /// <summary>
        /// Checks, is this graph is valid in term of it's CanBe-keys.
        /// </summary>
        /// <returns></returns>
        public bool CheckValidity()
        {
            if ((!this.CanBeCyclic) && (this.IsCycle()))
            {
                return false;
            }

            if ((!this.CanBeLooped) && (this.IsLooped()))
            {
                return false;
            }

            if ((!this.CanBeNonConnectivity) && (this.IsConnectivity()))
            {
                return false;
            }

            if ((this.Vertices != 0) && this.IsCorrectVertices())
            {
                return false;
            }

            return true;
        }

        public Graph Clone()
        {
            Graph graph = new Graph
            {
                CanBeCyclic = this.CanBeCyclic,
                CanBeLooped = this.CanBeLooped,
                CanBeNonConnectivity = this.CanBeNonConnectivity,
                EnsureValidity = this.EnsureValidity,
                Vertices = this.Vertices,
            };

            foreach (var item in this.Nodes)
            {
                graph.Nodes.Add(item.Clone());
            }

            return graph;
        }

        /// <summary>
        /// Create connection between two nodes.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public bool ConnectNodes(GraphNode lhs, GraphNode rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs), "Node is null");
            }

            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs), "Node is null");
            }

            lhs.AddConnection(rhs.Number);
            rhs.AddConnection(lhs.Number);

            if ((this.EnsureValidity) && (!this.CheckValidity()))
            {
                lhs.RemoveConnection(rhs.Number);
                rhs.RemoveConnection(lhs.Number);
                throw new ArgumentException("Graph is not valid.");
            }

            return true;
        }

        /// <summary>
        /// Create connection between two nodes by it's indexes.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public bool ConnectNodes(ushort lhs, ushort rhs)
        {
            this.Nodes[lhs].AddConnection(rhs);
            this.Nodes[rhs].AddConnection(lhs);

            if ((this.EnsureValidity) && (!this.CheckValidity()))
            {
                this.Nodes[lhs].RemoveConnection(this.Nodes[rhs].Number);
                this.Nodes[rhs].RemoveConnection(this.Nodes[lhs].Number);
                throw new ArgumentException("Graph is not valid.");
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Graph graph = obj as Graph;

            if ((object)graph == null)
            {
                return false;
            }

            if (graph == this)
            {
                return true;
            }

            return this.Equals(graph);
        }

        public bool Equals(Graph graph)
        {
            return
                (this == graph) &&
                (this.CanBeCyclic == graph.CanBeCyclic) &&
                (this.CanBeLooped == graph.CanBeLooped) &&
                (this.CanBeNonConnectivity == graph.CanBeNonConnectivity) &&
                (this.Vertices == graph.Vertices) &&
                (this.EnsureValidity == graph.EnsureValidity);
        }

        public override int GetHashCode()
        {
            int result = 17;

            for (int i = 0; i < this.Nodes.Count; i++)
            {
                result ^= i;
                foreach (var item in this.Nodes[i].Connections)
                {
                    result ^= item;
                }
            }

            return result;
        }

        /// <summary>
        /// Check, is graph solid using DFS.
        /// </summary>
        /// <returns></returns>
        public bool IsConnectivity()
        {
            this.IsCycle(0, 0);
            foreach (var item in this.Nodes)
            {
                if (item.Color == Colores.White)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsCorrectVertices()
        {
            if (this.Vertices == 0)
            {
                return true;
            }

            foreach (var item in this.Nodes)
            {
                if (item.Connections.Count != this.Vertices)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check. is graph cyclic.
        /// Based on deep-first.
        /// </summary>
        /// <returns></returns>
        public bool IsCycle()
        {
            foreach (var item in this.Nodes)
            {
                if (item.Color == Colores.White)
                {
                    if (this.IsCycle(item.Number, item.Number))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsLooped()
        {
            foreach (var node in this.Nodes)
            {
                foreach (var connect in node.Connections)
                {
                    if (node.Number == connect)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Check, is there any route between two nodes.
        /// Based on deep-first.
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="endNode"></param>
        /// <returns></returns>
        public bool IsRouteBetween(GraphNode startNode, GraphNode endNode)
        {
            if ((startNode == null) || (endNode == null))
            {
                throw new ArgumentNullException("Node is null");
            }

            return this.IsRouteBetween(startNode.Number, startNode.Number, endNode.Number);
        }

        /// <summary>
        /// Check, is there any route between two nodes.
        /// Based on deep-first.
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="endNode"></param>
        /// <returns></returns>
        public bool IsRouteBetween(ushort startNode, ushort endNode)
        {
            return this.IsRouteBetween(startNode, startNode, endNode);
        }

        /// <summary>
        /// Return false if there wasn't such node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool Remove(GraphNode node)
        {
            return this.Nodes.Remove(node);
        }

        /// <summary>
        /// Colories every node in graph.
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Colores color)
        {
            foreach (var item in this.Nodes)
            {
                item.Color = color;
            }
        }

        private bool IsCycle(ushort nodeNumber, ushort lastNode)
        {
            bool found = false;

            this.Nodes[nodeNumber].Color = Colores.Grey;

            foreach (var item in this.Nodes[nodeNumber].Connections)
            {
                if (item == lastNode)
                {
                    continue;
                }

                if (this.Nodes[item].Color == Colores.Grey)
                {
                    found = true;
                    return found;
                }

                if (this.Nodes[item].Color != Colores.Black)
                {
                    found = this.IsCycle(item, nodeNumber);
                    if (found)
                    {
                        return found;
                    }
                }
            }

            this.Nodes[nodeNumber].Color = Colores.Black;

            return found;
        }

        private bool IsRouteBetween(ushort nodeNumber, ushort lastNode, ushort wannaget)
        {
            bool found = false;

            this.Nodes[nodeNumber].Color = Colores.Black;

            if (this.Nodes[nodeNumber].Number == wannaget)
            {
                found = true;
                return found;
            }

            foreach (var item in this.Nodes[nodeNumber].Connections)
            {
                if (item == lastNode)
                {
                    continue;
                }

                if (this.Nodes[item].Color != Colores.Black)
                {
                    found = this.IsRouteBetween(item, nodeNumber, wannaget);
                    if (found)
                    {
                        return found;
                    }
                }
            }

            return found;
        }
    }
}