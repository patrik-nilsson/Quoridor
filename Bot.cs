using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Quoridor
{
    class Bot : Player
    {
        string direction;
        List<LinkedList<Node>> paths;
        LinkedList<Node> moves;
        Point optimalDirection;
        Point wrongDirection;
        Point left;
        Point right;
        int index;
        public Bot(Point position, Color color) : base(position, color)
        {
            paths = new List<LinkedList<Node>>();
            moves = new LinkedList<Node>();
            if (goalPosition == 0)
            {
                optimalDirection = new Point(0, -1);
                wrongDirection = new Point(0, 1);
            }
            else
            {
                optimalDirection = new Point(0, 1);
                optimalDirection = new Point(0, -1);
            }
            left = new Point(-1, 0);
            right = new Point(1, 0);
        }

        public override void ActionUpdate()
        {
            paths.Clear();
            moves.Clear();
            index = 0;
            DrawInitialPaths();
            FindPath();
            position = moves.First.Value.position;
            moves.RemoveFirst();
            actionTaken = true;
        }

        public void DrawInitialPaths()
        {
            if (Map.IsFree(position, optimalDirection))
            {
                if (!Map.WallBetween(position, optimalDirection))
                {
                    paths.Add(new LinkedList<Node>());
                    paths[index].AddFirst(new Node(1, Math.Abs(position.Y + optimalDirection.Y - goalPosition), new Point(position.X, position.Y + optimalDirection.Y)));
                    index++;
                }
            }
            if (Map.IsFree(position, wrongDirection))
            {
                if (!Map.WallBetween(position, wrongDirection))
                {
                    paths.Add(new LinkedList<Node>());
                    paths[index].AddFirst(new Node(1, Math.Abs(position.Y + wrongDirection.Y - goalPosition), new Point(position.X, position.Y + wrongDirection.Y)));
                    index++;
                }
            }
            if (Map.IsFree(position, left))
            {
                if (!Map.WallBetween(position, left))
                {
                    paths.Add(new LinkedList<Node>());
                    paths[index].AddFirst(new Node(1, Math.Abs(position.Y - goalPosition), new Point(position.X + left.X, position.Y)));
                    index++;
                }
            }
            if (Map.IsFree(position, right))
            {
                if (!Map.WallBetween(position, right))
                {
                    paths.Add(new LinkedList<Node>());
                    paths[index].AddFirst(new Node(1, Math.Abs(position.Y - goalPosition), new Point(position.X + right.X, position.Y)));
                    index++;
                }
            }
        }

        public int GetLowestWeight()
        {
            int x = 0;
            for (int i = 0; i < paths.Count; i++)
            {
                if (paths[x].First.Value.weight > paths[i].First.Value.weight)
                {
                    x = i;
                }
                //for (int j = i; j < paths.Count; j++)
                //{
                //    if (paths[i].First.Value.weight < paths[j].First.Value.weight)
                //    {
                //        x = i;
                //    }
                //    else if (paths[i].First.Value.weight > paths[j].First.Value.weight)
                //    {
                //        x = j;
                //    }
                //}
                //if (paths[i].First.Value.position.Y == goalPosition)
                //{
                //    x = i;
                //    break;
                //}
            }
            return x;
        }
        public string GetDirection(Point directionValue)
        {
            if (directionValue.Y == optimalDirection.Y)
            {
                return "forward";
            }
            if (directionValue.Y == wrongDirection.Y)
            {
                return "back";
            }
            if (directionValue.X == -1)
            {
                return "left";
            }
            if (directionValue.X == 1)
            {
                return "right";
            }
            return "none?";
        }
        public void FindPath()
        {
            int currentPath = GetLowestWeight();
            if (paths[currentPath].Count > 1)
            {
                Point directionValue = paths[currentPath].First.Value.position - paths[currentPath].First.Next.Value.position;
                direction = GetDirection(directionValue);
            }
            else
            {
                direction = "none";
            }
            bool firstStepAdded = false;
            if (direction != "back" && Map.IsFree(paths[currentPath].First.Value.position, optimalDirection))
            {
                if (!Map.WallBetween(paths[currentPath].First.Value.position, optimalDirection))
                {
                    paths[currentPath].AddFirst(new Node(paths[currentPath].First.Value.stepsFromStart + 1, Math.Abs(paths[currentPath].First.Value.position.Y + optimalDirection.Y - goalPosition), new Point(paths[currentPath].First.Value.position.X, paths[currentPath].First.Value.position.Y + optimalDirection.Y)));
                    firstStepAdded = true;
                }
            }
            if (direction != "right" && Map.IsFree(paths[currentPath].First.Value.position, left))
            {
                if (!Map.WallBetween(paths[currentPath].First.Value.position, left))
                {
                    if (!firstStepAdded)
                    {
                        paths[currentPath].AddFirst(new Node(paths[currentPath].First.Value.stepsFromStart + 1, Math.Abs(paths[currentPath].First.Value.position.Y - goalPosition), new Point(paths[currentPath].First.Value.position.X + left.X, paths[currentPath].First.Value.position.Y)));
                        firstStepAdded = true;
                    }
                    else
                    {
                        paths.Add(new LinkedList<Node>());
                        foreach (Node n in paths[currentPath])
                        {
                            paths[index].AddLast(n);
                        }
                        paths[index].AddFirst(new Node(paths[currentPath].First.Value.stepsFromStart + 1, Math.Abs(paths[currentPath].First.Value.position.Y - goalPosition), new Point(paths[currentPath].First.Value.position.X + left.X, paths[currentPath].First.Value.position.Y)));
                        index++;
                    }
                }
            }
            if (direction != "left" && Map.IsFree(paths[currentPath].First.Value.position, right))
            {
                if (!Map.WallBetween(paths[currentPath].First.Value.position, right))
                {
                    if (!firstStepAdded)
                    {
                        paths[currentPath].AddFirst(new Node(paths[currentPath].First.Value.stepsFromStart + 1, Math.Abs(paths[currentPath].First.Value.position.Y - goalPosition), new Point(paths[currentPath].First.Value.position.X + right.X, paths[currentPath].First.Value.position.Y)));
                        firstStepAdded = true;
                    }
                    else
                    {
                        paths.Add(new LinkedList<Node>());
                        foreach (Node n in paths[currentPath])
                        {
                            paths[index].AddLast(n);
                        }
                        paths[index].AddFirst(new Node(paths[currentPath].First.Value.stepsFromStart + 1, Math.Abs(paths[currentPath].First.Value.position.Y - goalPosition), new Point(paths[currentPath].First.Value.position.X + right.X, paths[currentPath].First.Value.position.Y)));
                        index++;
                    }
                }
            }
            if (direction != "forward" && Map.IsFree(paths[currentPath].First.Value.position, wrongDirection))
            {
                if (!Map.WallBetween(paths[currentPath].First.Value.position, wrongDirection))
                {
                    if (!firstStepAdded)
                    {
                        paths[currentPath].AddFirst(new Node(paths[currentPath].First.Value.stepsFromStart + 1, Math.Abs(paths[currentPath].First.Value.position.Y + wrongDirection.Y - goalPosition), new Point(paths[currentPath].First.Value.position.X, paths[currentPath].First.Value.position.Y + wrongDirection.Y)));
                        firstStepAdded = true;
                    }
                    else
                    {
                        paths.Add(new LinkedList<Node>());
                        foreach (Node n in paths[currentPath])
                        {
                            paths[index].AddLast(n);
                        }
                        paths[index].AddFirst(new Node(paths[currentPath].First.Value.stepsFromStart + 1, Math.Abs(paths[currentPath].First.Value.position.Y + wrongDirection.Y - goalPosition), new Point(paths[currentPath].First.Value.position.X, paths[currentPath].First.Value.position.Y + wrongDirection.Y)));
                        index++;
                    }
                }
            }
            if (paths[currentPath].First.Value.position.Y == goalPosition)
            {
                foreach (Node n in paths[currentPath])
                {
                    moves.AddFirst(n);
                }
            }
            else
            {
                if (!firstStepAdded)
                {
                    paths.RemoveAt(currentPath);
                    index--;
                }
                FindPath();
            }
        }
    }
}
