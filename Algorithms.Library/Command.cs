using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Library
{
    /// <summary>
    /// A Command pattern
    /// </summary>
    public class Command
    {
        public Command(Action action)
        {
            this.Action = action;
        }

        public Action Action { get; set; }

        public void Exec()
        {
            this.Action?.Invoke();
        }
    }
}
