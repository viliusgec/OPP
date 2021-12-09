using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Visitor
{
    public interface IComponent
    {
        void Accept(IVisitor visitor, Label label, int currency, int item_currency);
    }
}
