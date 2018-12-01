using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quoridor
{
    class Empty : Space
    {
        public Empty(Point position, Texture2D texture) :base(position, texture)
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            
        }
    }
}
