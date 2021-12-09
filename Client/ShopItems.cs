using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class ShopItems
    {
        string Name { get; set; }
        int ID { get; set; }
        public ShopItems(string Name, int ID)
        {
            this.Name = Name;
            this.ID = ID;
        }
    }
}
