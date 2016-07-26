using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Library.Achivement
{
    public class AchivementTree : ICloneable
    {
        #region Private Fields

        private readonly Graph<Achivement> graph;

        #endregion Private Fields

        #region Public Constructors

        public AchivementTree() : this(new Graph<Achivement>())
        {
        }

        public AchivementTree(IEnumerable<Achivement> nodes) : this(new Graph<Achivement>(nodes))
        {
        }

        #endregion Public Constructors

        public IEnumerable<Achivement> Nodes => this.graph.Nodes.Cast<Achivement>();


        #region Protected Internal Constructors

        protected internal AchivementTree(Graph<Achivement> graph)
        {
            this.graph = graph;
            this.graph.State = State.CanBeCycle | State.CanBeNonConnectivly;
        }

        #endregion Protected Internal Constructors

        #region Public Methods

        public void AddNode()
        {
            this.graph.AddNode();
        }

        public void AddNode(Achivement achive)
        {
            this.graph.AddNode(achive);
        }

        public void Connect(Achivement lhs, Achivement rhs)
        {
            this.graph.Connect(lhs, rhs);
        }

        public bool IsAllowedRouteBetween(Achivement startNode, Achivement endNode)
        {
            if ((startNode == null) ||
                (endNode == null))
            {
                throw new ArgumentNullException("Node is null");
            }

            return this.IsAllowedRouteBetween(startNode, startNode, endNode);
        }

        public bool IsCycle()
        {
            return this.graph.IsCycle();
        }

        public bool IsNonConnectivity()
        {
            return this.graph.IsNonConnectivity();
        }

        public bool IsRouteBetween(Achivement startNode, Achivement endNode)
        {
            return this.graph.IsRouteBetween(startNode, endNode);
        }

        public void RemoveNode(Achivement node)
        {
            this.graph.RemoveNode(node);
        }

        public void Toggle(Achivement achive)
        {
            if (this.IsAllowedRouteBetween(this.graph.Nodes[0], this.graph.Nodes[0], achive) &&
                (achive.Connections.Cast<Achivement>().Count(n => n.IsAvaliable) == 1))
            {
                achive.IsAvaliable = !achive.IsAvaliable;
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void ClearColor()
        {
            foreach (var graphNode in this.graph.Nodes)
            {
                graphNode.Color = Color.White;
            }
        }

        private bool IsAllowedRouteBetween(GraphNode nodeNumber, GraphNode lastNode, GraphNode wannaget, bool haveClearColor = true)
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
                        if (this.IsAllowedRouteBetween(item, nodeNumber, wannaget))
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
        public AchivementTree CloneDirectly()
        {
            return new AchivementTree(this.graph.Nodes);
        }

        public AchivementTree DeepClone()
        {
            Graph<Achivement> graph = this.graph.DeepClone();

            return new AchivementTree(graph);
        }

        #endregion Clone
    }
}