using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Library.Menu
{
    public class Menu<S> : ICloneable
        where S : MenuNode
    {
        protected readonly Graph<MenuNode> graph;

        public Menu() : this(new Graph<MenuNode>())
        {
        }

        public Menu(IEnumerable<S> nodes) : this(new Graph<MenuNode>(nodes))
        {
        }

        public IList<MenuNode> Nodes => this.graph.Nodes;

        public void Connect(S lhs, S rhs)
            => this.graph.Connect(lhs, rhs);

        protected internal Menu(Graph<MenuNode> graph)
        {
            this.graph = graph;
            this.graph.State = State.Default;
        }

        public void AddNode(S node)
        {
            this.graph.AddNode(node);
        }

        public bool IsRouteBetween(S startNode, S endNode)
        {
            return this.graph.IsRouteBetween(startNode, endNode);
        }

        public void RemoveNode(S node)
        {
            this.graph.RemoveNode(node);
        }


        public bool IsCycle(bool haveClearColor = true)
            => this.graph.IsCycle(haveClearColor);

        public bool IsLooped()
            => this.graph.IsLooped();

        public bool IsNonConnectivity()
            => this.graph.IsNonConnectivity();

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

        #region Clone

        public object Clone()
        {
            return this.ShallowClone();
        }

        /// <summary>
        /// Overload of this.Clone() by return value
        /// </summary>
        /// <returns></returns>
        public Menu<S> ShallowClone()
        {
            Graph<MenuNode> graph = this.graph.ShallowClone();

            return new Menu<S>(graph);
        }

        public Menu<S> DeepClone()
        {
            Graph<MenuNode> graph = this.graph.DeepClone();

            return new Menu<S>(graph);
        }

        #endregion Clone
    }
}