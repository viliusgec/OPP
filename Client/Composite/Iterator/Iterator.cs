using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Composite.Iterator
{
    interface Iterator
    {
        Room first();
        Room currentItem();
        bool hasNext(); 
        Room next();
    }
}
