using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor
{
    static class Judge
    {
        public static Player[] Players { get; private set; }
        public static void GivePlayers(Player[] players)
        {
            Players = players;
        }

        public static int GetWalls(Player player)
        {
            foreach(Player p in Players)
            {
                if(p != player)
                {
                    return p.walls;
                }
            }
            return 1000;
        }
    }
}
