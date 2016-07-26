using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Library.Menu
{
    public class Menu<T> : ICloneable
        where T : MenuNode, new()
    {
        private readonly Graph<T> graph;

        public Menu() : this(new Graph<T>())
        {
        }

        public Menu(IEnumerable<T> nodes) : this(new Graph<T>(nodes))
        {
        }

        public IEnumerable<T> Nodes => this.graph.Nodes;

        public void Connect(T lhs, T rhs)
            => this.graph.Connect(lhs, rhs);

        protected internal Menu(Graph<T> graph)
        {
            this.graph = graph;
            this.graph.State = State.Default;
        }

        public void AddNode()
        {
            this.graph.AddNode();
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

        #region Clone

        public object Clone()
        {
            return (object)this.CloneDirectly();
        }

        /// <summary>
        /// Overload of this.Clone() by return value
        /// </summary>
        /// <returns></returns>
        public Menu<T> CloneDirectly()
        {
            return new Menu<T>(this.graph.Nodes.Cast<T>());
        }

        public Menu<T> DeepClone()
        {
            Graph<T> graph = this.graph.DeepClone();

            return new Menu<T>(graph);
        }

        #endregion Clone
    }
}