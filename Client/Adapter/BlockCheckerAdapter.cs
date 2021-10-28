using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Adapter
{
    class BlockCheckerAdapter : BlockChecker
    {
        int side;
        int x;
        int y;
        PictureBox pictureBox1;
        Map.MapBase map;
        HubConnection connection;
        private BlockCheckerAdaptees check;

        public BlockCheckerAdapter(int side, int x, int y, PictureBox pictureBox1, Map.MapBase map, HubConnection connection)
        {
            this.side = side;
            this.x = x;
            this.y = y;
            this.pictureBox1 = pictureBox1;
            this.map = map;
            this.connection = connection;
        }
        public override bool check_if_block_exists()
        {
            check = new BlockCheckerAdaptees();
            bool exist =false;

            exist = check.check_if_block_exists_specific(side, x, y, pictureBox1, map, connection);

            return exist;
        }
    }
}
