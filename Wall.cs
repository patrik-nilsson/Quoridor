using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Quoridor
{
    class Wall : Space
    {
        int addX;
        int addY;
        public Wall(Point position, Texture2D texture, bool horizontal) :base(position, texture)
        {
            wallSet = false;
            if(horizontal)
            {
                size = new Point(32, 5);
                addX = 2;
                addY = 0;
            }
            else
            {
                size = new Point(5, 32);
                addX = 0;
                addY = 2;
            }
            rectangle = new Rectangle(position, size);
        }

        public override bool SetWall(int x, int y)
        {
            if(!wallSet)
            {
                if(Map.grid[x+addX, y+addY].ExtendWall())
                {
                    wallSet = true;
                    return true;
                }
            }
            return false;
        }
        public override bool ExtendWall()
        {
            if (!wallSet)
            {
                wallSet = true;
                return true;
            }
            else
                return false;
        }

        public override void Update()
        {
            if (rectangle.Contains(Mouse.GetState().Position) && !wallSet)
            {
                color = Color.Yellow;
            }
            else 
            {
                color = Color.Black;
            }
            base.Update();
        }

        public override void Draw(SpriteBatch sb)
        {
            if (!wallSet)
            {
                sb.Draw(texture, rectangle, color);
            }
            else
            {
                sb.Draw(texture, rectangle, Color.Red);
            }
        }
    }
}
