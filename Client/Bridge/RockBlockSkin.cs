using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Bridge
{
    class RockBlockSkin
    {
        public static string SetSkin(string health)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            return int.Parse(health) switch
            {
                >= 125 => currentDir + @"\Resources\rock.png",
                >= 100 => currentDir + @"\Resources\rock1.png",
                >= 75 => currentDir + @"\Resources\rock2.png",
                >= 50 => currentDir + @"\Resources\rock3.png",
                >= 0 => currentDir + @"\Resources\rock4.png",
                _ => currentDir + @"\Resources\rock.png",
            };
        }
    }
}
