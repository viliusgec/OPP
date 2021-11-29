using System;
using System.IO;

namespace Client.Bridge
{
    public class RockBlockSkin
    {
        public static string SetSkin(string health)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            return health switch
            {
                "100" => currentDir + @"\Resources\rock1.png",
                "75" => currentDir + @"\Resources\rock2.png",
                "50" => currentDir + @"\Resources\rock3.png",
                "25" => currentDir + @"\Resources\rock4.png",
                _ => currentDir + @"\Resources\rock.png",
            };
        }
    }
}
