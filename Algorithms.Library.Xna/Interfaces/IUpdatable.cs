using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Algorithms.Library.Xna
{
    public interface IUpdatable
    {
        void Update(GameTime gameTime);
    }
}
