using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Quoridor
{
    class Human : Player
    {
        public Human(Point position, Color color):base(position,color)
        {

        }

        public override void Update()
        {
            base.Update();
            if (!actionTaken)
            {
                if (KeyMouseReader.KeyPressed(Keys.W))
                {
                    if (Map.IsFree(position, new Point(0, -1)))
                    {
                        if (!Map.WallBetween(position, new Point(0, -1)))
                        {
                            position.Y -= 1;
                            actionTaken = true;
                        }
                    }
                }
                else if (KeyMouseReader.KeyPressed(Keys.S))
                {
                    if (Map.IsFree(position, new Point(0, 1)))
                    {
                        if (!Map.WallBetween(position, new Point(0, 1)))
                        {
                            position.Y += 1;
                            actionTaken = true;
                        }
                    }
                }
                else if (KeyMouseReader.KeyPressed(Keys.A))
                {
                    if (Map.IsFree(position, new Point(-1, 0)))
                    {
                        if (!Map.WallBetween(position, new Point(-1, 0)))
                        {
                            position.X -= 1;
                            actionTaken = true;
                        }
                    }
                }
                else if (KeyMouseReader.KeyPressed(Keys.D))
                {
                    if (Map.IsFree(position, new Point(1, 0)))
                    {
                        if (!Map.WallBetween(position, new Point(1, 0)))
                        {
                            position.X += 1;
                            actionTaken = true;
                        }
                    }
                }
                else if (KeyMouseReader.LeftClick())
                {
                    if (walls > 0)
                    {
                        if (Map.SetWall())
                        {
                            walls--;
                            actionTaken = true;
                        }
                    }
                }
            }
        }
    }
}
