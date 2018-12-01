using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Quoridor
{
    class Player
    {
        public Color color { get; protected set; }
        protected Point position;
        protected int goalPosition;
        public int walls;
        public bool actionTaken;
        public Player(Point position, Color color)
        {
            actionTaken = false;
            this.position = position;
            this.color = color;
            walls = 8;
            if(position.Y == 0)
            {
                goalPosition = Map.Rows / 2;
            }
            else
            {
                goalPosition = 0;
            }
        }

        public bool ReachedGoal()
        {
            if(position.Y == goalPosition)
            {
                return true;
            }
            return false;
        }

        public virtual void ActionUpdate()
        {

        }

        public virtual void Update()
        {
            Map.ImHere(position, this);
        }
    }
}
