using System;
using System.IO;

namespace Client.Bridge
{
    public class DirtBlockSkin
    {
        public static string SetSkin(string health)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string currentDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            return health switch
            {
                "100" => currentDir + @"\Resources\dirt2.png",
                "75" => currentDir + @"\Resources\dirt3.png",
                "50" => currentDir + @"\Resources\dirt4.png",
                "25" => currentDir + @"\Resources\dirt5.png",
                _ => currentDir + @"\Resources\dirt1.png",
            };
        }
    }
}
