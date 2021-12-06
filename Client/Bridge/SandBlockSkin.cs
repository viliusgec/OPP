using System;
using System.IO;

namespace Client.Bridge
{
    public class SandBlockSkin
    {
        public static string SetSkin(string health)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            return int.Parse(health) switch
            {
                >= 125 => currentDir + @"\Resources\sand1.png",
                >= 100 => currentDir + @"\Resources\sand2.png",
                >= 75 => currentDir + @"\Resources\sand3.png",
                >= 50 => currentDir + @"\Resources\sand4.png",
                >= 0 => currentDir + @"\Resources\sand5.png",
                _ => currentDir + @"\Resources\sand1.png",
            };
        }
    }
}
