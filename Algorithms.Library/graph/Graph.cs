﻿using Algorithms.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Library
{
    [Flags]
    public enum State
    {
        Default = 0,
        CanBeCycle = 1,
        CanBeLooped = 2,
        CanBeNonConnectivly = 4,
    }

    public class Graph : ICloneable
    {
        #region Public Constructors

        public Graph()
        {
            this.Nodes = new Dictionary<int, GraphNode>();
            this.State = State.Default;
        }

        public Graph(IEnumerable<GraphNode> nodes)
        {
            if (nodes == null)
            {
                nodes = new List<GraphNode>();
            }

            this.Nodes = new Dictionary<int, GraphNode>();

            foreach (var node in nodes)
            {
                this.Nodes.Add(new KeyValuePair<int, GraphNode>(this.Nodes.Count, node));
            }
        }

        public Graph(IDictionary<int, GraphNode> nodes)
        {
            if (nodes == null)
            {
                nodes = new Dictionary<int, GraphNode>();
            }

            this.Nodes = new Dictionary<int, GraphNode>();

            foreach (var node in nodes)
            {
                this.Nodes.Add(node);
            }
        }

        #endregion Public Constructors

        #region Public Properties

        public Guid Id { get; set; }
        public IDictionary<int, GraphNode> Nodes { get; private set; }
        public State State { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Colories every node in graph.
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Color color)
        {
            foreach (var item in this.Nodes)
            {
                item.Value.Color = color;
            }
        }

        /// <summary>
        /// Check, is graph solid using DFS.
        /// </summary>
        /// <returns></returns>
        public bool IsConnectivity()
        {
            try
            {
                var node = this.Nodes[0];
                this.IsCycle(node, node);

                return this.Nodes.All(item => item.Value.Color != Color.White);
            }
            finally
            {
                this.ClearColor();
            }
        }

        public void AddNode()
        {
            GraphNode node = new GraphNode();
            this.Nodes.Add(new KeyValuePair<int, GraphNode>(this.Nodes.Count, node));
        }

        public void AddNode(GraphNode node)
        {
            if (this.Nodes.Values.Contains(node))
            {
                throw new ArgumentException($"Graph {this.Id} already contains node {node}");
            }

            this.Nodes.Add(new KeyValuePair<int, GraphNode>(this.Nodes.Count, node));

            foreach (var connection in node.Connections)
            {
                if (!this.Nodes.Values.Contains(connection))
                {
                    this.AddNode(connection);
                }
            }
        }

        public void Connect(GraphNode lhs, GraphNode rhs)
        {
            if (lhs == null || rhs == null)
            {
                throw new ArgumentNullException();
            }

            lhs.Connect(rhs);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(256);

            return sb.ToString();
        }

        /// <summary>
        /// Check. is graph cyclic.
        /// Based on deep-first.
        /// </summary>
        /// <returns></returns>
        public bool IsCycle(bool haveClearColor = true)
        {
            if (haveClearColor)
            {
                this.ClearColor();
            }
            // TODO what
            try
            {
                foreach (var item in this.Nodes)
                {
                    if (item.Value.Color != Color.White)
                    {
                        continue;
                    }

                    if (this.IsCycle(item.Value, item.Value))
                    {
                        return true;
                    }
                }

                return false;
            }
            finally
            {
                if (haveClearColor)
                {
                    this.ClearColor();
                }
            }
        }

        /// <summary>
        /// Check, is this graph looped.
        /// O(nodes * nodeConnections)
        /// </summary>
        /// <returns></returns>
        public bool IsLooped()
        {
            foreach (var node in this.Nodes)
            {
                foreach (var connect in node.Value.Connections)
                {
                    if (node.Value == connect)
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
            if ((startNode == null) ||
                (endNode == null))
            {
                throw new ArgumentNullException("Node is null");
            }

            return this.IsRouteBetween(startNode, startNode, endNode);
        }

        public void RemoveNode(GraphNode node)
        {
            if (!this.Nodes.Values.Contains(node))
            {
                throw new ArgumentException($"Graph {this.Id} has no node {node}");
            }

            foreach (var graphNode in this.Nodes)
            {
                graphNode.Value.Disconnect(node);
            }

            KeyValuePair<int, GraphNode> a = this.Nodes.First(n => n.Value == node);

            this.Nodes.Remove(a);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Set color of all nodes of this graph to Color.White
        /// </summary>
        private void ClearColor()
        {
            foreach (var graphNode in this.Nodes)
            {
                graphNode.Value.Color = Color.White;
            }
        }

        private bool EnshureValid()
        {
            throw new System.NotImplementedException();
        }

        private bool IsCycle(GraphNode node, GraphNode lastNode)
        {
            node.Color = Color.Grey;

            foreach (var item in node.Connections)
            {
                if (item == lastNode)
                {
                    continue;
                }

                if (item.Color == Color.Grey)
                {
                    return true;
                }

                if (item.Color != Color.Black)
                {
                    if (this.IsCycle(item, node))
                    {
                        return true;
                    }
                }
            }

            node.Color = Color.Black;

            return false;
        }

        private bool IsRouteBetween(GraphNode nodeNumber, GraphNode lastNode, GraphNode wannaget, bool haveClearColor = true)
        {
            if (haveClearColor)
            {
                this.ClearColor();
            }
            try
            {
                nodeNumber.Color = Color.Black;

                if (nodeNumber == wannaget)
                {
                    return true;
                }

                foreach (var item in nodeNumber.Connections)
                {
                    if (item == lastNode)
                    {
                        continue;
                    }

                    if (item.Color != Color.Black)
                    {
                        if (this.IsRouteBetween(item, nodeNumber, wannaget))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            finally
            {
                if (haveClearColor)
                {
                    this.ClearColor();
                }
            }
        }

        #endregion Private Methods

        #region Clone

        public object Clone()
        {
            return (object)this.CloneDirectly();
        }

        /// <summary>
        /// Overload of this.Clone() by return value
        /// </summary>
        /// <returns></returns>
        public Graph CloneDirectly()
        {
            return new Graph(this.Nodes);
        }

        public Graph DeepClone()
        {
            IDictionary<int, GraphNode> newNodes = this.Nodes;

            return new Graph(newNodes)
            {
                Id = this.Id,
                State = this.State,
            };
        }

        #endregion Clone
    }
}