﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Library.Achivement
{
    public class AchivementTree<T> : ICloneable
        where T : Achivement
    {
        #region Private Fields

        protected readonly Graph<Achivement> graph;

        #endregion Private Fields

        #region Public Constructors

        public AchivementTree() : this(new Graph<Achivement>())
        {
        }

        public AchivementTree(IEnumerable<T> nodes) : this(new Graph<Achivement>(nodes))
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public IList<Achivement> Nodes => this.graph.Nodes;

        #endregion Public Properties

        #region Protected Internal Constructors

        protected internal AchivementTree(Graph<Achivement> graph)
        {
            this.graph = graph;
            this.graph.State = State.CanBeCycle | State.CanBeNonConnectivly;
        }

        #endregion Protected Internal Constructors

        #region Public Methods

        public State State
        {
            get
            {
                return this.graph.State;
            }
            set
            {
                this.graph.State = value;
            }
        }

        public void AddNode(T achive)
        {
            this.graph.AddNode(achive);
        }

        public void Connect(T lhs, T rhs)
        {
            this.graph.Connect(lhs, rhs);
        }

        public bool IsAllowedRouteBetween(T startNode, T endNode)
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

        public bool IsLooped()
                    => this.graph.IsLooped();

        public bool IsNonConnectivity()
        {
            return this.graph.IsNonConnectivity();
        }

        public bool IsRouteBetween(T startNode, T endNode)
        {
            return this.graph.IsRouteBetween(startNode, endNode);
        }

        public void RemoveNode(T node)
        {
            this.graph.RemoveNode(node);
        }

        public void Toggle(T achive)
        {
            if (this.IsAllowedRouteBetween(this.graph.Nodes[0], this.graph.Nodes[0], achive) &&
                (achive.Children.Cast<T>().Count(n => n.IsAvaliable) == 1))
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

        private bool IsAllowedRouteBetween(Achivement nodeNumber, Achivement lastNode, Achivement wannaget, bool haveClearColor = true)
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

                foreach (var item in nodeNumber.Children)
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
            return this.ShallowClone();
        }

        /// <summary>
        /// Overload of this.Clone() by return value
        /// </summary>
        /// <returns></returns>
        public AchivementTree<T> ShallowClone()
        {
            Graph<Achivement> graph = this.graph.ShallowClone();

            return new AchivementTree<T>(graph);
        }

        public AchivementTree<T> DeepClone()
        {
            Graph<Achivement> graph = this.graph.DeepClone();

            return new AchivementTree<T>(graph);
        }

        #endregion Clone
    }
}