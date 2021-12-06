using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Visitor
{
    class ConcreteVisitor : IVisitor
    {
        public void VisitConcreteComponentA(ConcreteComponentA element)
        {
            element.BuyWithScore();
        }

        public void VisitConcreteComponentB(ConcreteComponentB element)
        {
            element.BuyWithMoney();
        }
    }
}
