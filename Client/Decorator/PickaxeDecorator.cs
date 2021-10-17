using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Decorator
{
    public class PickaxeDecorator
    {
        public static IPickaxe GetPickaxe(string pickaxe)
        {
            switch(pickaxe)
            {
                case "Red":
                    return RedPickaxe;
                case "Blue":
                    return BluePickaxe();
                case "Black":
                    return BlackPickaxe();
                default:
                    return DefaultPickaxe();
            }
        }
    }
}
