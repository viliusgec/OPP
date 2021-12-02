using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Bridge
{
    class SandBlockSkin
    {
        public static string SetSkin(string health)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            switch (int.Parse(health))
            {
                case >= 125:
                    return currentDir + @"\Resources\sand1.png";
                case >= 100:
                    return currentDir + @"\Resources\sand2.png";
                case >= 75:
                    return currentDir + @"\Resources\sand3.png";
                case >= 50:
                    return currentDir + @"\Resources\sand4.png";
                case >= 0:
                    return currentDir + @"\Resources\sand5.png";
                default:
                    return currentDir + @"\Resources\sand1.png";
            }
        }
    }
}
