using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Library.Menu
{
    public class Menu<T> : ICloneable
        where T : MenuNode
    {
        protected readonly Graph<T> graph;

        public Menu() : this(new Graph<T>())
        {
        }

        public Menu(IEnumerable<T> nodes) : this(new Graph<T>(nodes))
        {
        }

        public IList<T> Nodes => this.graph.Nodes;

        public void Connect(T lhs, T rhs)
            => this.graph.Connect(lhs, rhs);

        protected internal Menu(Graph<T> graph)
        {
            this.graph = graph;
            this.graph.State = State.Default;
        }

        public void AddNode(T node)
        {
            this.graph.AddNode(node);
        }

        public bool IsRouteBetween(T startNode, T endNode)
        {
            return this.graph.IsRouteBetween(startNode, endNode);
        }

        public void RemoveNode(T node)
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
        public Menu<T> ShallowClone()
        {
            return new Menu<T>(this.graph.Nodes);
        }

        public Menu<T> DeepClone()
        {
            Graph<T> graph = this.graph.DeepClone();

            return new Menu<T>(graph);
        }

        #endregion Clone
    }
}