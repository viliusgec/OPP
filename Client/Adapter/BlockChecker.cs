using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// target class
namespace Client.Adapter
{
    public class BlockChecker
    {
        public virtual bool check_if_block_exists()
        {
            return false;
        }
    }
}
