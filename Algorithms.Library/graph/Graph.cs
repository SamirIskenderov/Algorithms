using System;
using System.Collections.Generic;
using System.Linq;

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

    public class Graph<T> : ICloneable
        where T : GraphNode, new()
    {
        #region Public Constructors

        public Graph() : this(null)
        {
        }

        public Graph(IEnumerable<T> nodes)
        {
            if (nodes == null)
            {
                nodes = new List<T>();
            }

            this.Nodes = nodes.ToList();
            this.State = State.Default;
        }

        #endregion Public Constructors

        #region Public Properties

        public Guid Id { get; } = Guid.NewGuid();
        public IList<T> Nodes { get; private set; }
        public State State { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void AddNode()
        {
            T node = new T();
            this.Nodes.Add(node);
            this.EnshureValid();
        }

        public void AddNode(T node)
        {
            if (this.Nodes.Contains(node))
            {
                throw new ArgumentException($"Graph {this.Id} already contains node {node}");
            }

            this.Nodes.Add(node);

            foreach (var graphNode in node.Connections)
            {
                var connection = graphNode as T;

                if (connection == null)
                {
                    throw new InvalidCastException(nameof(connection));
                }

                if (!this.Nodes.Contains(connection))
                {
                    this.AddNode(connection);
                }
            }

            this.EnshureValid();
        }

        public void Connect(T lhs, T rhs)
        {
            if (lhs == null || rhs == null)
            {
                throw new ArgumentNullException();
            }

            lhs.Connect(rhs);

            this.EnshureValid();
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

            try
            {
                foreach (var item in this.Nodes)
                {
                    if (item.Color != Color.White)
                    {
                        continue;
                    }

                    if (this.IsCycle(item, item))
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
                foreach (var connect in node.Connections)
                {
                    if (node == connect)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Check, is graph solid using DFS.
        /// </summary>
        /// <returns></returns>
        public bool IsNonConnectivity()
        {
            if (this.Nodes.Count == 0)
            {
                return this.State.HasFlag(State.CanBeNonConnectivly);
            }

            try
            {
                T root = this.Nodes[0];

                foreach (var node in this.Nodes)
                {
                    if (!this.IsRouteBetween(node, root))
                    {
                        return true;
                    }
                }

                return false;
            }
            finally
            {
                this.ClearColor();
            }
        }

        /// <summary>
        /// Check, is there any route between two nodes.
        /// Based on deep-first.
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="endNode"></param>
        /// <returns></returns>
        public bool IsRouteBetween(T startNode, T endNode)
        {
            if ((startNode == null) ||
                (endNode == null))
            {
                throw new ArgumentNullException("Node is null");
            }

            return this.IsRouteBetween(startNode, startNode, endNode);
        }

        public void RemoveNode(T node)
        {
            if (!this.Nodes.Contains(node))
            {
                throw new ArgumentException($"Graph {this.Id} has no node {node}");
            }

            foreach (var T in this.Nodes)
            {
                T.Disconnect(node);
            }

            this.Nodes.Remove(node);

            this.EnshureValid();
        }

        /// <summary>
        /// Colories every node in graph.
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Color color)
        {
            foreach (var item in this.Nodes)
            {
                item.Color = color;
            }
        }

        #endregion Public Methods

        #region Private Methods

        protected virtual void EnshureValid()
        {
            if (!this.State.HasFlag(State.CanBeCycle))
            {
                if (this.IsCycle())
                {
                    throw new InvalidOperationException($"Graph {this.Id} state {this.State} is not allow to use this action: graph could became cycle");
                }
            }

            if (!this.State.HasFlag(State.CanBeLooped))
            {
                if (this.IsLooped())
                {
                    throw new InvalidOperationException($"Graph {this.Id} state {this.State} is not allow to use this action: graph could became looped");
                }
            }

            if (!this.State.HasFlag(State.CanBeNonConnectivly))
            {
                if (this.IsNonConnectivity())
                {
                    throw new InvalidOperationException($"Graph {this.Id} state {this.State} is not allow to use this action: graph could became non-connectivited");
                }
            }
        }

        /// <summary>
        /// Set color of all nodes of this graph to Color.White
        /// </summary>
        private void ClearColor()
        {
            foreach (var T in this.Nodes)
            {
                T.Color = Color.White;
            }
        }

        private bool IsCycle(T node, T lastNode)
        {
            node.Color = Color.Grey;

            foreach (var graphNode in node.Connections)
            {
                var item = graphNode as T;
                if (item == null)
                {
                    throw new InvalidCastException(nameof(item));
                }

                if ((item == lastNode) ||
                    (item == node))
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

        private bool IsRouteBetween(T nodeNumber, T lastNode, T wannaget, bool haveClearColor = true)
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

                foreach (var graphNode in nodeNumber.Connections)
                {
                    var item = graphNode as T;
                    if (item == null)
                    {
                        throw new InvalidCastException(nameof(item));
                    }

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
        public Graph<T> CloneDirectly()
        {
            return new Graph<T>(this.Nodes);
        }

        public Graph<T> DeepClone()
        {
            IList<T> newNodes = this.Nodes.ToList();

            return new Graph<T>(newNodes)
            {
                State = this.State,
            };
        }

        #endregion Clone
    }
}