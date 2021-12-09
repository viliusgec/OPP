using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Flyweight
{
    public class FlyweightFactory
    {
        private Dictionary<int, PlayerSkin> players = new Dictionary<int, PlayerSkin>();
        
        public PlayerSkin PlayerSkin(int key)
        {
            PlayerSkin player = null;
            if (players.ContainsKey(key))
            {
                player = players[key];
            }
            else
            {
                switch (key)
                {
                    case 1:
                        player = new PlayerWhite();
                        break;
                    case 2:
                        player = new PlayerBlack();
                        break;
                    case 3:
                        player = new PlayerDiamond();
                        break;
                    case 4:
                        player = new EnemyWhite();
                        break;
                    case 5:
                        player = new EnemyBlack();
                        break;
                    case 6:
                        player = new EnemyDiamond();
                        break;
                    default:
                        break;
                }
                players.Add(key, player);
            }

            return player;
        }
    }
}
