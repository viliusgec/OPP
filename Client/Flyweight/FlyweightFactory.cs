﻿using System.Collections.Generic;

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
                    default:
                        break;
                }
                players.Add(key, player);
            }

            return player;
        }
    }
}
