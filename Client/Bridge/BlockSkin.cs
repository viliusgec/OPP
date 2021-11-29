namespace Client.Bridge
{
    public class BlockSkin
    {
        public static string SetImage(string health, string blockType)
        {
            return blockType switch
            {
                "Dirt" => DirtBlockSkin.SetSkin(health),
                "Sand" => SandBlockSkin.SetSkin(health),
                "Rock" => RockBlockSkin.SetSkin(health),
                _ => "",
            };
        }
    }
}
