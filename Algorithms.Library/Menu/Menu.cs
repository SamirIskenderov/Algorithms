using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Library.Menu
{
    public class Menu : ICloneable
    {
        private readonly Graph<MenuNode> graph;

        public Menu() : this(new Graph<MenuNode>())
        {
        }

        public Menu(IEnumerable<MenuNode> nodes) : this(new Graph<MenuNode>(nodes))
        {
        }

        public IEnumerable<MenuNode> Nodes => this.graph.Nodes;

        public void Connect(MenuNode lhs, MenuNode rhs)
            => this.graph.Connect(lhs, rhs);

        protected internal Menu(Graph<MenuNode> graph)
        {
            this.graph = graph;
            this.graph.State = State.Default;
        }

        public void AddNode()
        {
            this.graph.AddNode();
        }

        public void AddNode(MenuNode node)
        {
            this.graph.AddNode(node);
        }

        public bool IsRouteBetween(MenuNode startNode, MenuNode endNode)
        {
            return this.graph.IsRouteBetween(startNode, endNode);
        }

        public void RemoveNode(MenuNode node)
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
        public Menu CloneDirectly()
        {
            return new Menu(this.graph.Nodes.Cast<MenuNode>());
        }

        public Menu DeepClone()
        {
            Graph<MenuNode> graph = this.graph.DeepClone();

            return new Menu(graph);
        }

        #endregion Clone
    }
}