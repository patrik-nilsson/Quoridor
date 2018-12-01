using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quoridor
{
    static class Map
    {
        public static int Columns { get; private set; }
        public static int Rows { get; private set; }
        public static Texture2D texture { get; set; }
        public static Space[,] grid { get; private set; }

        public static void Initialize(int columns, int rows, Texture2D tex)
        {
            Columns = columns * 2 - 1;
            Rows = rows * 2 - 1;
            texture = tex;
            grid = new Space[Columns, Rows];
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    if (y % 2 == 0)
                    {
                        if (x % 2 == 0)
                        {
                            grid[x, y] = new Space(new Point(x * 32 - 14 * x, y * 32 - 14 * y), texture);
                        }
                        else
                        {
                            grid[x, y] = new Wall(new Point(x * 32 - 14 * (x - 1), y * 32 - 14 * y), texture, false);
                        }
                    }
                    else
                    {
                        if (x % 2 == 0)
                        {
                            grid[x, y] = new Wall(new Point(x * 32 - 14 * x, y * 32 - 14 * (y - 1)), texture, true);
                        }
                        else
                        {
                            grid[x, y] = new Empty(new Point(x * 32 - 14 * (x - 1), y * 32), texture);
                        }
                    }
                }
            }
        }

        public static void ImHere(Point position, Player player)
        {
            grid[position.X * 2, position.Y * 2].SetPlayer(player);
        }

        public static bool IsFree(Point currentSpace, Point direction)
        {
            Point space = new Point((currentSpace.X + direction.X) * 2, (currentSpace.Y + direction.Y) * 2);
            if (space.X < 0 || space.Y < 0)
            {
                return false;
            }
            if (space.X > Columns || space.Y > Rows)
            {
                return false;
            }
            if (grid[space.X, space.Y].IsFree())
            {
                return true;
            }
            return false;
        }

        public static bool WallBetween(Point currentSpace, Point direction)
        {
            Point space = new Point((currentSpace.X * 2 + direction.X), (currentSpace.Y * 2 + direction.Y));

            if (grid[space.X, space.Y].wallSet)
            {
                return true;
            }
            return false;
        }

        public static bool SetWall()
        {
            for (int y = 0; y < Rows - 1; y++)
            {
                for (int x = 0; x < Columns - 1; x++)
                {
                    if (grid[x, y] is Wall)
                    {
                        if (grid[x, y].rectangle.Contains(KeyMouseReader.mouseState.Position))
                        {
                            if (grid[x, y].SetWall(x, y))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static void Update()
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    grid[x, y].Update();
                }
            }
        }

        public static void Draw(SpriteBatch sb)
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    if (y % 2 == 0)
                    {
                        if (x % 2 == 0) //If ground...
                        {
                            grid[x, y].Draw(sb);
                        }
                        else //If vertical wall...
                        {
                            grid[x, y].Draw(sb);
                        }
                    }
                    else
                    {
                        if (x % 2 == 0) //If horizontal wall...
                        {
                            grid[x, y].Draw(sb);
                        }
                        else //If Empty...
                        {
                            grid[x, y].Draw(sb);
                        }
                    }
                }
            }
        }
    }
}
