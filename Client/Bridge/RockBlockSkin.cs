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
            switch (health)
            { 
                case "100":
                    return currentDir + @"\Resources\rock1.png";
                case "75":
                    return currentDir + @"\Resources\rock2.png";
                case "50":
                    return currentDir + @"\Resources\rock3.png";
                case "25":
                    return currentDir + @"\Resources\rock4.png";
                default:
                    return currentDir + @"\Resources\rock.png";
            }
        }
    }
}
