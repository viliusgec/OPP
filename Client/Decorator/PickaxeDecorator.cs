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
                    return new RedPickaxe();
                case "Blue":
                    return new BluePickaxe();
                case "Black":
                    return new BlackPickaxe();
                default:
                    return new DefaultPickaxe();
            }
        }
    }
}
