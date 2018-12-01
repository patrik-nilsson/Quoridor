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
    class Space
    {
        public Rectangle rectangle { get; protected set; }
        protected Point size;
        protected Point playerSize;
        protected Color color;

        public bool wallSet { get; protected set; }

        protected Point position;
        protected Texture2D texture;
        protected bool hasPlayer;
        protected Player player;
        public Space(Point position, Texture2D texture)
        {
            this.position = position;
            this.texture = texture;
            size = new Point(32, 32);
            rectangle = new Rectangle(position, size);
            playerSize = new Point(28, 28);
            color = Color.Gray;
        }

        public void SetPlayer(Player player)
        {
            hasPlayer = true;
            this.player = player;
        }

        public bool IsFree()
        {
            if (hasPlayer)
            {
                return false;
            }
            return true;
        }
        public virtual bool SetWall(int x, int y)
        {
            return false;
        }
        public virtual bool ExtendWall()
        {
            return false;
        }

        public virtual void Update()
        {
            hasPlayer = false;
            player = null;
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rectangle, color);
            if (hasPlayer)
            {
                sb.Draw(texture, new Rectangle(new Point(position.X + 2, position.Y + 2), playerSize), player.color);
            }
        }
    }
}
