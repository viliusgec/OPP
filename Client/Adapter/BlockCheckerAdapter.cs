﻿using Microsoft.AspNetCore.SignalR.Client;
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
        FormsEditor editor;
        Map.MapBase map;
        HubConnection connection;
        private BlockCheckerAdaptees check;

        public BlockCheckerAdapter(int side, int x, int y, FormsEditor editor, Map.MapBase map, HubConnection connection)
        {
            this.side = side;
            this.x = x;
            this.y = y;
            this.editor = editor;
            this.map = map;
            this.connection = connection;
            check = new BlockCheckerAdaptees();
        }
        public override bool check_if_block_exists()
        {
            bool exist = false;

            exist = check.check_if_block_exists_specific(side, x, y, editor, map, connection);

            return exist;
        }
    }
}
