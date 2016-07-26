using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Library
{
    /// <summary>
    /// For Strategy class. Different strategies have to be in different scopes
    /// </summary>
    public class Scope
    {
        public Scope(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }
    }
}
