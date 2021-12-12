using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Visitor
{
    public interface IVisitor
    {
        public void VisitConcreteComponentA(ScoreComponent element);
        public void VisitConcreteComponentB(MoneyComponent element);
    }
}
