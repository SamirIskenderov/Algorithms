using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Algorithms.Library
{
    public enum Color
    {
        Black = 0,
        Grey = 1,
        White = 2,
    }

    public abstract class GraphNode<U> : ICloneable
        where U : GraphNode<U>
    {
        #region Public Constructors

        public GraphNode() : this(null, Color.White)
        {
            
        }

        public GraphNode(IEnumerable<U> connections = null, Color color = Color.White)
        {
            this.Color = color;

            if (connections == null)
            {
                connections = new List<U>();
            }

            this.Children = connections.ToList();
        }

        #endregion Public Constructors

        #region Public Properties

        public Color Color { get; set; }

        public IList<U> Children { get; }

        #endregion Public Properties

        #region Clone

        public virtual object Clone()
        {
            return (object)this.ShallowClone();
        }

        /// <summary>
        /// Overload of this.Clone() by return value
        /// </summary>
        /// <returns></returns>
        public virtual GraphNode<U> ShallowClone()
        {
            var str = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<GraphNode<U>>(str);
        }

        #endregion Clone

        #region Public Methods

        /// <summary>
        /// This method add connection to this. In result nodes appears in Connections property of each other.
        /// </summary>
        /// <param name="node"></param>
        internal void Connect(U node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }

            this.Children.Add(node);
            node.Children.Add(this as U);
        }

        internal void Disconnect(U node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }

            this.Children.Remove(node);
            node.Children.Remove(this as U);
        }

        #endregion Public Methods
    }
}