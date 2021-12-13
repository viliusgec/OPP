namespace Client.Visitor
{
    internal class ConcreteVisitor : IVisitor
    {
        public void VisitConcreteComponentA(ScoreComponent element)
        {
            element.BuyWithScore();
        }

        public void VisitConcreteComponentB(MoneyComponent element)
        {
            element.BuyWithMoney();
        }
    }
}
