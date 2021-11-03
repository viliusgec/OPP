using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Bridge
{
   public class BlockSkin
    {
        public static string SetImage(string health, string blockType)
        {
            switch(blockType)
            {
                case "Dirt":
                    return DirtBlockSkin.SetSkin(health);
                case "Sand":
                    return SandBlockSkin.SetSkin(health);
                case "Rock":
                    return RockBlockSkin.SetSkin(health);
                default:
                    return "";
            }
        }
            
    }
}
