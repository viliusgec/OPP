using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Map
{
    abstract class Block
    {
        public string name { get; set; }
        public Block(string _name)
        {
            name = _name;
        }
    }
}
