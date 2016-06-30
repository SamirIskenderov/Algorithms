using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Library.Menu
{
    public class Menu : ICloneable
    {
        private readonly Graph graph;

        public Menu() : this(new Graph())
        {
        }

        public Menu(IEnumerable<GraphNode> nodes) : this(new Graph(nodes))
        {
        }

        protected internal Menu(Graph graph)
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
            return new Menu(this.graph.Nodes);
        }

        public Menu DeepClone()
        {
            Graph graph = this.graph.DeepClone();

            return new Menu(graph);
        }

        #endregion Clone
    }
}
