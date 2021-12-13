using Client.Decorator;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client.Adapter
{
    public class BlockCheckerAdapter : BlockChecker
    {
        private readonly int side;
        private readonly int x;
        private readonly int y;
        private readonly FormsEditor editor;
        private readonly Map.MapBase map;
        private readonly HubConnection connection;
        private readonly Character player;
        private readonly BlockCheckerAdaptees check;
        private readonly PictureBoxBuilder.MapBuilder mapBuilder;
        private readonly string room;

        public BlockCheckerAdapter(int side, int x, int y, FormsEditor editor, Map.MapBase map, HubConnection connection, Character player, PictureBoxBuilder.MapBuilder mapBuilder, string room)
        {
            this.side = side;
            this.x = x;
            this.y = y;
            this.editor = editor;
            this.map = map;
            this.connection = connection;
            this.player = player;
            this.mapBuilder = mapBuilder;
            this.room = room;
            check = new BlockCheckerAdaptees();
        }
        public override bool Check_if_block_exists()
        {
            return check.Check_if_block_exists_specific(side, x, y, editor, map, connection, player, mapBuilder, room);
        }
    }
}
