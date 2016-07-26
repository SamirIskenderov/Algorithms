using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Algorithms.Library.Xna
{
    public interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch);
    }
}
